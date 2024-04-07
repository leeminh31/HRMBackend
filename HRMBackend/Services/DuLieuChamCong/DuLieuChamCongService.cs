using AutoMapper;
using ExcelDataReader;
using HRMBackend.DataAccess.DuLieuChamCong;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using HRMBackend.DataAccess.CaLamViec;
using HRMBackend.Resources.DTO.BaoCaoTheoThang.Response;
using HRMBackend.DataAccess.DonPhep;
using HRMBackend.DataAccess.DonBu;
using HRMBackend.DataAccess.DonConNho;
using HRMBackend.DataAccess.DonTangCa;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.DataAccess.DangKyCa;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using HRMBackend.Resources.DTO.DangKyCa.Request;
using HRMBackend.DataAccess.HopDong;
using HRMBackend.Models;

namespace HRMBackend.Services.DuLieuChamCong
{
    public class DuLieuChamCongService : BaseService, IDuLieuChamCongService
    {
        #region Property
        private readonly IDuLieuChamCongDAO _duLieuChamCongDAO;
        private readonly ICaLamViecDAO _caLamViecDAO;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IDonPhepDAO _donPhepDAO;
        private readonly IDonBuDAO _donBuDAO;
        private readonly IDonConNhoDAO _donConNhoDAO;
        private readonly IDonTangCaDAO _donTangCaDAO;
        private readonly IDangKyCaDAO _dangKyCaDAO;
        private readonly IPhongBanDAO _phongBanDAO;
        private readonly IHopDongDAO _hopDongDAO;
        #endregion

        #region Constructor
        public DuLieuChamCongService(IDuLieuChamCongDAO duLieuChamCongDAO,
            INhanVienDAO nhanVienDAO,
            ICaLamViecDAO caLamViecDAO,
            IDonBuDAO donBuDAO,
            IDangKyCaDAO dangKyCaDAO,
            IDonConNhoDAO donConNhoDAO,
            IDonPhepDAO donPhepDAO,
            IDonTangCaDAO donTangCaDAO,
            IHopDongDAO hopDongDAO,
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
            this._donBuDAO = donBuDAO;
            this._dangKyCaDAO = dangKyCaDAO;
            this._donConNhoDAO = donConNhoDAO;
            this._hopDongDAO = hopDongDAO;
            this._donTangCaDAO = donTangCaDAO;
            this._donPhepDAO = donPhepDAO;
        }
        #endregion

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

            var searchRequest = new SearchDonPhepRequest();
            searchRequest.MaNhanVien = request?.MaNhanVien;
            searchRequest.NgayBatDauTaoDon = request?.NgayBatDau;
            searchRequest.NgayKetThucTaoDon = request?.NgayKetThuc;

            var donPhep = await _donPhepDAO.GetDayOffAsync(searchRequest);

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

                var dangKyCa = await _dangKyCaDAO.GetByEmployeeIDAsync(employee.MaNhanVien, request.NgayBatDau, request.NgayKetThuc);

                var employeeByMonth = recordsToList.Where(e => e.MaNhanVien == employee.MaNhanVien);

                var employeeInfo = await _nhanVienDAO.GetByIDAsync(employee.MaNhanVien);

                //var shiftId = employeeInfo.data?.MaCa;

                //if (shiftId == 0)
                //    continue;

                //var shiftName = await _caLamViecDAO.GetByShiftIDAsync(shiftId, null);

                //var giobatdaulam = shiftName.data.First().GioBatDauCa;

                //var gioketthuclam = shiftName.data.First().GioKetThucCa;

                //var giobatdaunghi = shiftName.data.First().GioBatDauNghi;

                //var gioketthucnghi = shiftName.data.First().GioKetThucNghi;

                //var workHourByDay = Math.Round((gioketthuclam - giobatdaulam - gioketthucnghi + giobatdaunghi).TotalMinutes);

                //var gionghi = Math.Round((gioketthucnghi - giobatdaunghi).TotalMinutes);

                var totalWorkByMonth =(double) 0;

                var listDLCCByDay = new List<DuLieuChamCongByDayResponse>();
                for ( int i = 1; i <= daysInMonth; i++)
                {
                    var employeeByDay = employeeByMonth.Where(e => e.NgayChamCong == new DateTime((int)request.NgayKetThuc?.Year, (int)request.NgayKetThuc?.Month, i));

                    var DLCCByDay = new DuLieuChamCongByDayResponse();
                    DLCCByDay.NgayLamViec = i;
                    //DLCCByDay.GioLamViec = totalWorkHours;
                    //DLCCByDay.GioLamViecTheoCa = workHourByDay;

                    if (employeeByDay != null)
                    {
                        var lastCheck = employeeByDay.MaxBy(t => t.LanChamCong);

                        var firstCheck = employeeByDay.MinBy(t => t.LanChamCong);

                        var firstCheckTime = firstCheck.GioChamCong;

                        var lastCheckTime = lastCheck.GioChamCong;

                        //if (firstCheck?.GioChamCong < giobatdaulam)
                        //{
                        //    firstCheckTime = giobatdaulam;
                        //}

                        //if (lastCheck?.GioChamCong > gioketthuclam)
                        //{
                        //    lastCheckTime = gioketthuclam;
                        //}

                        //var totalWorkHours = Math.Round((lastCheckTime - firstCheckTime).TotalMinutes);

                        //if (lastCheck?.GioChamCong >= gioketthucnghi)
                        //    totalWorkHours = totalWorkHours - gionghi;

                        //if (lastCheck?.GioChamCong >= giobatdaunghi && lastCheck?.GioChamCong <= gioketthucnghi)
                        //{
                        //    totalWorkHours = Math.Round((giobatdaunghi - giobatdaulam).TotalMinutes);
                        //}

                        //var totalWork = Math.Round(totalWorkHours / workHourByDay, 2);

                        bool off = false;

                        if (donPhep.isSuccess)
                        {
                            off = donPhep.data.Any(dp => dp.NgayLamViec.Day == i && dp.MaNhanVien == employee.MaNhanVien);
                        }

                        DLCCByDay.NghiPhep = off;


                        //totalWorkByMonth += totalWork;



                        if (dangKyCa.isSuccess)
                        {
                            foreach (var dangKy in dangKyCa.data)
                            {
                                var hopDong = await _hopDongDAO.GetContractByTimeRangeAndIDAsync(employee.MaNhanVien, request.NgayBatDau, request.NgayKetThuc);
                                if (hopDong.hasValue)
                                {
                                    foreach (var item in hopDong.data)
                                    {
                                        if (dangKy.NgayBatDauCaMoi <= item.NgayKetThucHopDong)
                                        {
                                            if (item.NgayBatDauHopDong < request.NgayBatDau && item.NgayKetThucHopDong < request.NgayKetThuc && item.NgayKetThucHopDong >= request.NgayBatDau)
                                            {
                                                if (dangKy.NgayBatDauCaMoi <= request.NgayBatDau && i <= item.NgayKetThucHopDong.Day)
                                                {
                                                    DLCCByDay.TenCa = dangKy.CaLamViecMoi;
                                                }

                                                if (dangKy.NgayBatDauCaMoi > request.NgayBatDau && i >= dangKy.NgayBatDauCaMoi.Day && i <= item.NgayKetThucHopDong.Day)
                                                {
                                                    DLCCByDay.TenCa = dangKy.CaLamViecMoi;
                                                }
                                                else if (i < dangKy.NgayBatDauCaMoi.Day)
                                                {
                                                    DLCCByDay.TenCa = dangKy.CaLamViecHienTai;
                                                }
                                            }

                                            if (item.NgayBatDauHopDong > request.NgayBatDau && item.NgayKetThucHopDong < request.NgayKetThuc)
                                            {
                                                if (dangKy.NgayBatDauCaMoi < item.NgayBatDauHopDong)
                                                {
                                                    if (i >= item.NgayBatDauHopDong.Day && i <= item.NgayKetThucHopDong.Day)
                                                    {
                                                        DLCCByDay.TenCa = dangKy.CaLamViecMoi;
                                                    }
                                                }

                                                if (dangKy.NgayBatDauCaMoi >= item.NgayBatDauHopDong)
                                                {
                                                    if (i <= item.NgayKetThucHopDong.Day && i >= dangKy.NgayBatDauCaMoi.Day)
                                                    {
                                                        DLCCByDay.TenCa = dangKy.CaLamViecMoi;
                                                    }

                                                    if (i < dangKy.NgayBatDauCaMoi.Day && i >= item.NgayBatDauHopDong.Day)
                                                    {
                                                        DLCCByDay.TenCa = dangKy.CaLamViecHienTai;
                                                    }
                                                }
                                            }

                                            if (item.NgayBatDauHopDong <= request.NgayBatDau && item.NgayKetThucHopDong >= request.NgayKetThuc)
                                            {
                                                if (dangKy.NgayBatDauCaMoi <= request.NgayBatDau)
                                                {
                                                    DLCCByDay.TenCa = dangKy.CaLamViecMoi;
                                                }
                                                else
                                                {
                                                    if (i >= dangKy.NgayBatDauCaMoi.Day)
                                                    {
                                                        DLCCByDay.TenCa = dangKy.CaLamViecMoi;
                                                    }
                                                    else
                                                    {
                                                        DLCCByDay.TenCa = dangKy.CaLamViecHienTai;
                                                    }
                                                }
                                            }

                                            if (item.NgayBatDauHopDong <= request.NgayKetThuc && item.NgayBatDauHopDong > request.NgayBatDau && item.NgayKetThucHopDong > request.NgayKetThuc)
                                            {
                                                if (dangKy.NgayBatDauCaMoi >= item.NgayBatDauHopDong)
                                                {
                                                    if (i >= dangKy.NgayBatDauCaMoi.Day && i <= request.NgayKetThuc.Value.Day)
                                                    {
                                                        DLCCByDay.TenCa = dangKy.CaLamViecMoi;
                                                    }

                                                    if (i >= item.NgayBatDauHopDong.Day && i < dangKy.NgayBatDauCaMoi.Day)
                                                    {
                                                        DLCCByDay.TenCa = dangKy.CaLamViecHienTai;
                                                    }
                                                }
                                                else
                                                {
                                                    if (i >= item.NgayBatDauHopDong.Day && i <= request.NgayKetThuc.Value.Day)
                                                    {
                                                        DLCCByDay.TenCa = dangKy.CaLamViecMoi;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DLCCByDay.TenCa = dangKy.CaLamViecMoi;
                                }
                            }
                        } else
                        {
                            DLCCByDay.TenCa = "8A";
                        }
                    }

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
    }
}
