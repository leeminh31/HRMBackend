using AutoMapper;
using ExcelDataReader;
using HRMBackend.DataAccess.DuLieuChamCong;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;
using HRMBackend.Resources.DTO.NhanVien.Request;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.DuLieuChamCong;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using HRMBackend.Models;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.Resources.DTO.BaoCaoTheoThang.Response;

namespace HRMBackend.Services.DuLieuChamCong
{
    public class DuLieuChamCongService : BaseService, IDuLieuChamCongService
    {
        #region Property
        private readonly IDuLieuChamCongDAO _duLieuChamCongDAO;
        private readonly ICaLamViecDAO _caLamViecDAO;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IPhongBanDAO _phongBanDAO;
        #endregion

        #region Constructor
        public DuLieuChamCongService(IDuLieuChamCongDAO duLieuChamCongDAO,
            INhanVienDAO nhanVienDAO,
            ICaLamViecDAO caLamViecDAO,
            IPhongBanDAO phongBanDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._duLieuChamCongDAO = duLieuChamCongDAO;
            this._nhanVienDAO = nhanVienDAO;
            this._caLamViecDAO = caLamViecDAO;
            this._unitOfWork = unitOfWork;
            this._nhanVienDAO = nhanVienDAO;
            this._phongBanDAO = phongBanDAO;
        }
        #endregion

        //public async Task<BaseResult<DuLieuChamCongResponse>> CreateAsync(CreateDuLieuChamCongRequest request)
        //{
        //    // Mapping Resource to DuLieuChamCong
        //    var airport = Mapper.Map<CreateDuLieuChamCongRequest, Models.DuLieuChamCong>(request);
        //    //SearchDuLieuChamCongRequest searchRequest = new SearchDuLieuChamCongRequest() { Code = request.Code, Name = request.Name };
        //    //Tìm mã code hoặc name đã tồn tại chưa?
        //    var records = await _duLieuChamCongDAO.GetByIDAsync(request.TenDuLieuChamCong);
        //    if (records.hasValue)
        //    {
        //        return GetBaseResult(CodeMessage._552, data: Mapper.Map<DuLieuChamCongResponse>(records.data));
        //    }

        //    var result = await _duLieuChamCongDAO.CreateAsync(airport);
        //    await _unitOfWork.SaveChangesAsync();

        //    if (result.isSuccess)
        //        return GetBaseResult(CodeMessage._200, data: Mapper.Map<DuLieuChamCongResponse>(result.data));
        //    else
        //        return GetBaseResult<DuLieuChamCongResponse>(CodeMessage._209, status: StatusEnum.Failed);
        //}

        public async Task<BaseResult<IEnumerable<DuLieuChamCongResponse>>> GetByParamsAsync(SearchDuLieuChamCongRequest request)
        {
            
            var records = await _duLieuChamCongDAO.GetByParamsAsync(request.MaNhanVien, request.NgayBatDau, request.NgayKetThuc);
            
            if(!records.isSuccess)
            {
                return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }

            var employeeData = await _nhanVienDAO.GetByParamsAsync(null, null, request.IDVanTay, null, request.TenNhanVien);

            if( !employeeData.isSuccess )
            {
                return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }
            var filterRecords =  records.data?.Where(c => employeeData.data.Select(i => i.MaNhanVien).Contains(c.MaNhanVien));

            if (filterRecords!.GetEnumerator().MoveNext())
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DuLieuChamCongResponse>>(filterRecords));
            }
            return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<BaoCaoTheoThangAllResponse>>> GetByEmployeePerMonthAsync(SearchDuLieuChamCongRequest request)
        {
            var records = await _duLieuChamCongDAO.GetByParamsAsync(null, request.NgayBatDau, request.NgayKetThuc);

            var listEmployee = await _nhanVienDAO.GetByParamsAsync(request.MaNhanVien, null, null, null, request.TenNhanVien);

            if (!listEmployee.isSuccess)
            {
                return GetBaseResult<IEnumerable<BaoCaoTheoThangAllResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }

            var listEmployeeId = listEmployee.data.Select(e => e.MaNhanVien);


            if (request.NgayBatDau == null || request.NgayKetThuc == null) {
                return GetBaseResult<IEnumerable<BaoCaoTheoThangAllResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }

            if (!records.isSuccess)
            {
                return GetBaseResult<IEnumerable<BaoCaoTheoThangAllResponse>>(CodeMessage._545, status: StatusEnum.Failed);
            }

            var daysInMonth = (int) (request.NgayKetThuc! - request.NgayBatDau!)?.TotalDays +1;

            var recordsToList = records.data.ToList();

            var recordsByEmployee = recordsToList.DistinctBy(item => item.MaNhanVien).ToList();

            var baoCaoTheoThangAll = new List<BaoCaoTheoThangAllResponse>();

            foreach (var employee in recordsByEmployee)
            {
                if (!listEmployeeId.Contains(employee.MaNhanVien))
                    continue;

                var employeeByMonth = recordsToList.Where(e => e.MaNhanVien == employee.MaNhanVien);

                var employeeInfo = await _nhanVienDAO.GetByIDAsync(employee.MaNhanVien);

                var shiftId = employeeInfo.data?.MaCa;

                if (shiftId == 0)
                    continue;

                var shiftName = await _caLamViecDAO.GetByShiftIDAsync(shiftId, null);

                var giobatdaulam = shiftName.data.First().GioBatDauCa;

                var gioketthuclam = shiftName.data.First().GioKetThucCa;

                var giobatdaunghi = shiftName.data.First().GioBatDauNghi;

                var gioketthucnghi = shiftName.data.First().GioKetThucNghi;

                var workHourByDay = Math.Round((gioketthuclam - giobatdaulam - gioketthucnghi + giobatdaunghi).TotalMinutes);

                var gionghi = Math.Round((gioketthucnghi - giobatdaunghi).TotalMinutes);
                var totalWorkByMonth =(double) 0;

                var listDLCCByDay = new List<DuLieuChamCongByDayResponse>();
                for ( int i = 1; i <= daysInMonth; i++)
                {
                    var employeeByDay = employeeByMonth.Where(e => e.NgayChamCong == new DateTime((int)request.NgayKetThuc?.Year, (int)request.NgayKetThuc?.Month, i));

                    if (employeeByDay == null)
                        continue;
                    
                    var lastCheck = employeeByDay.MaxBy(t => t.LanChamCong);

                    var firstCheck = employeeByDay.MinBy(t => t.LanChamCong);

                    if (firstCheck == null)
                        continue;

                    var firstCheckTime = firstCheck.GioChamCong;

                    var lastCheckTime = lastCheck.GioChamCong;

                    if (firstCheck?.GioChamCong < giobatdaulam)
                    {
                        firstCheckTime = giobatdaulam;
                    }

                    if (lastCheck?.GioChamCong > gioketthuclam)
                    {
                        lastCheckTime = gioketthuclam;
                    }

                    var totalWorkHours = Math.Round((lastCheckTime - firstCheckTime).TotalMinutes);

                    if (lastCheck?.GioChamCong >= gioketthucnghi)
                        totalWorkHours = totalWorkHours - gionghi;

                    if (lastCheck?.GioChamCong >= giobatdaunghi && lastCheck?.GioChamCong <= gioketthucnghi)
                    {
                        totalWorkHours = Math.Round((giobatdaunghi - giobatdaulam).TotalMinutes);
                    }

                    var totalWork = Math.Round(totalWorkHours / workHourByDay,2);

                    totalWorkByMonth += totalWork;

                    var DLCCByDay = new DuLieuChamCongByDayResponse();
                    DLCCByDay.NgayLamViec = i;
                    DLCCByDay.GioLamViec = totalWorkHours;
                    listDLCCByDay.Add(DLCCByDay);
                }

                var baoCaoTheoThangNhanVien = new BaoCaoTheoThangAllResponse();
                baoCaoTheoThangNhanVien.MaNhanVien = employee.MaNhanVien;
                baoCaoTheoThangNhanVien.TongCong = Math.Round(totalWorkByMonth, 2);
                baoCaoTheoThangNhanVien.duLieuChamCongResponses = listDLCCByDay;

                baoCaoTheoThangAll.Add(baoCaoTheoThangNhanVien);
            }

            if( baoCaoTheoThangAll.Count > 0 )
            {
                return GetBaseResult(CodeMessage._200, data: baoCaoTheoThangAll.AsEnumerable());
            } else 
            return GetBaseResult<IEnumerable<BaoCaoTheoThangAllResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<DuLieuChamCongResponse>>> GetAllContractAsync()
        {
            var records = await _duLieuChamCongDAO.GetAllContractAsync();
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<DuLieuChamCongResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<DuLieuChamCongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<DuLieuChamCongResponse>> GetByIDAsync(string maDuLieuChamCong)
        {
            var records = await _duLieuChamCongDAO.GetByIDAsync(maDuLieuChamCong);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<DuLieuChamCongResponse>(records.data));
            }
            return GetBaseResult<DuLieuChamCongResponse>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<bool>> UploadFileTimeKeepingAsync(IFormFile file)
        {
            var listTimeKeepingRecords = new List<Models.DuLieuChamCong>();

            if (file != null && file.Length > 0)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var extension = Path.GetExtension(file.FileName);
                if (extension != ".xls" && extension != ".xlsx")
                    return GetBaseResult<bool>(CodeMessage._555, status: StatusEnum.Failed);

                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\Upload\\";

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            int isHeaderSkipped = 0;

                            while (reader.Read())
                            {
                                if (isHeaderSkipped < 1)
                                {
                                    isHeaderSkipped++;
                                    continue;
                                }

                                if (String.IsNullOrEmpty(reader.GetValue(1)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(2)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(3)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(4)?.ToString()))
                                {
                                    continue;
                                }

                                if ( !int.TryParse(reader.GetValue(3)?.ToString(), out int a)
                                    || !DateTime.TryParse(reader.GetValue(2)?.ToString(), out DateTime b)
                                    || !DateTime.TryParse(reader.GetValue(4)?.ToString(), out DateTime c))
                                {
                                    continue;
                                }

                                var listEmployeeId = await _nhanVienDAO.GetByIdVanTayAsync(int.Parse(reader.GetValue(1)?.ToString()), null);

                                CreateDuLieuChamCongRequest duLieuChamCong = new CreateDuLieuChamCongRequest();
                                duLieuChamCong.MaNhanVien = listEmployeeId.data.MaNhanVien;
                                duLieuChamCong.LanChamCong = int.Parse(reader.GetValue(3)?.ToString());
                                duLieuChamCong.NgayChamCong = DateTime.Parse(reader.GetValue(2)?.ToString());
                                duLieuChamCong.GioChamCong = DateTime.Parse(reader.GetValue(4)?.ToString()).TimeOfDay;

                                var duLieuChamCongModel = Mapper.Map<CreateDuLieuChamCongRequest, Models.DuLieuChamCong>(duLieuChamCong);
                                listTimeKeepingRecords.Add(duLieuChamCongModel);
                                
                            }
                        } while (reader.NextResult());

                    }
                }

                System.IO.File.Delete(filePath);
            }

            var result = await _duLieuChamCongDAO.UpdateOrInsertListRecordsAsync(listTimeKeepingRecords.AsEnumerable());
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: true);
            else
                return GetBaseResult<bool>(CodeMessage._556, status: StatusEnum.Failed);
        }

        private bool HasSpecialChars(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        private bool IsValidEmail(string email)
        {
            if (!MailAddress.TryCreate(email, out var mailAddress))
                return false;

            // And if you want to be more strict:
            var hostParts = mailAddress.Host.Split('.');
            if (hostParts.Length == 1)
                return false; // No dot.
            if (hostParts.Any(p => p == string.Empty))
                return false; // Double dot.
            if (hostParts[^1].Length < 2)
                return false; // TLD only one letter.

            if (mailAddress.User.Contains(' '))
                return false;
            if (mailAddress.User.Split('.').Any(p => p == string.Empty))
                return false; // Double dot or dot at end of user part.

            return true;
        }
    }
}
