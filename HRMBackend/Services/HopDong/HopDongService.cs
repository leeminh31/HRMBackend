using AutoMapper;
using HRMBackend.DataAccess.HopDong;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.HopDong.Request;
using HRMBackend.Resources.DTO.HopDong.Response;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.HopDong;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ExcelDataReader;
using HRMBackend.Resources.DTO.NhanVien.Request;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.Extensions;
using System.Text;
using System.Net.Mail;
using Microsoft.AspNetCore.StaticFiles;
using MySqlX.XDevAPI.Common;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;

namespace HRMBackend.Services.HopDong
{
    public class HopDongService : BaseService, IHopDongService
    {
        #region Property
        private readonly IHopDongDAO _hopDongDAO;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly IPhongBanDAO _phongBanDAO;
        #endregion

        #region Constructor
        public HopDongService(IHopDongDAO hopDongDAO,
            INhanVienDAO nhanVienDAO,
            IPhongBanDAO phongBanDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._hopDongDAO = hopDongDAO;
            this._unitOfWork = unitOfWork;
            this._nhanVienDAO = nhanVienDAO;
            this._phongBanDAO = phongBanDAO;
        }
        #endregion

        public async Task<BaseResult<HopDongResponse>> CreateAsync(CreateHopDongRequest request)
        {
            // Mapping Resource to HopDong
            var airport = Mapper.Map<CreateHopDongRequest, Models.HopDong>(request);
            //SearchHopDongRequest searchRequest = new SearchHopDongRequest() { Code = request.Code, Name = request.Name };
            //Tìm mã code hoặc name đã tồn tại chưa?
            var records = await _hopDongDAO.GetByIDAsync(request.TenHopDong);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._552, data: Mapper.Map<HopDongResponse>(records.data));
            }

            var result = await _hopDongDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<HopDongResponse>(result.data));
            else
                return GetBaseResult<HopDongResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<HopDongResponse>>> GetByParamsAsync(string? tenHopDong, string? loaiHopDong)
        {
            var records = await _hopDongDAO.GetByParamsAsync(tenHopDong, loaiHopDong);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<HopDongResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<HopDongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<HopDongResponse>>> GetAllContractAsync()
        {
            var records = await _hopDongDAO.GetAllContractAsync();
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<HopDongResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<HopDongResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<HopDongResponse>> GetByIDAsync(string maHopDong)
        {
            var records = await _hopDongDAO.GetByIDAsync(maHopDong);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<HopDongResponse>(records.data));
            }
            return GetBaseResult<HopDongResponse>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<HopDongResponse>> UpdateAsync(UpdateHopDongRequest request)
        {
            // Mapping Resource to HopDong
            var airport = Mapper.Map<UpdateHopDongRequest, Models.HopDong>(request);

            var result = await _hopDongDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<HopDongResponse>(result.data));
            else
                return GetBaseResult<HopDongResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<bool>> UploadFileAsync(IFormFile file)
        {
            // Mapping Resource to HopDong
            //var airport = Mapper.Map<UpdateHopDongRequest, Models.HopDong>(request);

            var listEmployeeRecords = new List<Models.NhanVien>();
            var listContractRecords = new List<Models.HopDong>();

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
                    var listEmployeeIdFromContract = await _hopDongDAO.GetByParamsAsync(null,"Thử việc");
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            int isHeaderSkipped = 0;

                            while (reader.Read())
                            {
                                if (isHeaderSkipped < 2)
                                {
                                    isHeaderSkipped++;
                                    continue;
                                }

                                if (String.IsNullOrEmpty(reader.GetValue(1)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(2)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(3)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(4)?.ToString())
                                    || String.IsNullOrEmpty(reader.GetValue(5)?.ToString())
                                    || (String.IsNullOrEmpty(reader.GetValue(12)?.ToString()))) {

                                    continue;
                                }

                                if (HasSpecialChars(reader.GetValue(1)?.ToString())
                                    || HasSpecialChars(reader.GetValue(2)?.ToString())
                                    || HasSpecialChars(reader.GetValue(3)?.ToString())
                                    || HasSpecialChars(reader.GetValue(5)?.ToString())
                                    || HasSpecialChars(reader.GetValue(26)?.ToString())
                                    || HasSpecialChars(reader.GetValue(27)?.ToString()))
                                {
                                    continue;
                                }

                                if (!reader.GetValue(2).ToString().All(char.IsDigit) 
                                    || (reader.GetValue(7) != null && !reader.GetValue(7).ToString().All(char.IsDigit))
                                    || (reader.GetValue(11) != null && !reader.GetValue(11).ToString().All(char.IsDigit))
                                    || (reader.GetValue(25) != null && !reader.GetValue(25).ToString().All(char.IsDigit))
                                    || (reader.GetValue(28) != null && !reader.GetValue(28).ToString().All(char.IsDigit)))
                                {
                                    continue;
                                }

                                if (reader.GetValue(28).ToString().Length !=10 
                                    || reader.GetValue(11).ToString().Length != 10
                                    || reader.GetValue(7).ToString().Length != 12)
                                {
                                    continue;
                                }
                                
                                var departmentIdResult = await _phongBanDAO.GetByTenPhongBanAsync(reader.GetValue(4).ToString(), null);
                                if(departmentIdResult.hasValue == false )
                                {
                                    continue;
                                }
                                var departmentId = departmentIdResult.data.MaPhongBan;
                                
                                if (IsValidEmail(reader.GetValue(12).ToString()))
                                {
                                    CreateNhanVienRequest nhanVien = new CreateNhanVienRequest();
                                    nhanVien.MaNhanVien = reader.GetValue(1).ToString();
                                    nhanVien.IDVanTay = int.Parse(reader.GetValue(2).ToString());
                                    nhanVien.HoTen = reader.GetValue(3).ToString();
                                    nhanVien.MaPhongBan = departmentId;
                                    nhanVien.ChucVu = reader.GetValue(5).ToString();
                                    nhanVien.NgaySinh = DateTime.Parse(reader.GetValue(6).ToString());
                                    nhanVien.SoCCCD = reader.GetValue(7).ToString();
                                    nhanVien.NgayCap = DateTime.Parse(reader.GetValue(8).ToString());
                                    nhanVien.QueQuan = reader.GetValue(9).ToString();
                                    nhanVien.NoiOHienTai = reader.GetValue(10).ToString();
                                    nhanVien.SoDienThoai = reader.GetValue(11).ToString();
                                    nhanVien.Mail = reader.GetValue(12).ToString();
                                    nhanVien.STKNganHang = reader.GetValue(25).ToString();
                                    nhanVien.NganHang = reader.GetValue(26).ToString();
                                    nhanVien.NguoiThanLienHe = reader.GetValue(27).ToString();
                                    nhanVien.SoDienThoaiNguoiLienHe = reader.GetValue(28).ToString();
                                    var nhanVienModel = Mapper.Map<CreateNhanVienRequest, Models.NhanVien>(nhanVien);
                                    listEmployeeRecords.Add(nhanVienModel);
                                }

                                //if (!listEmployeeId.data.Contains(reader.GetValue(1).ToString()) && String.IsNullOrEmpty(reader.GetValue(18)?.ToString()))
                                //{
                                //    continue;
                                //}

                                if( listEmployeeIdFromContract.data?.FirstOrDefault(c => c.MaNhanVien == reader.GetValue(1).ToString()) == null && String.IsNullOrEmpty(reader.GetValue(18)?.ToString())) {
                                    continue;
                                }

                                if (!String.IsNullOrEmpty(reader.GetValue(18)?.ToString()) 
                                    && !String.IsNullOrEmpty(reader.GetValue(16)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(17)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(14)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(13)?.ToString()))
                                {
                                    var dateStart = DateTime.Parse(reader.GetValue(16).ToString());
                                    var dateEnd = DateTime.Parse(reader.GetValue(17).ToString());
                                    if (dateStart.AddDays(90) < dateEnd)
                                    {
                                        dateEnd = dateStart.AddDays(90);
                                    }

                                    CreateHopDongRequest hopDongThuViec = new CreateHopDongRequest();
                                    hopDongThuViec.TenHopDong = reader.GetValue(18).ToString();
                                    hopDongThuViec.MaNhanVien = reader.GetValue(1).ToString();
                                    hopDongThuViec.NgayBatDauHopDong = dateStart;
                                    hopDongThuViec.NgayKetThucHopDong = dateEnd;
                                    hopDongThuViec.loaiHopDong = "Thử việc";
                                    hopDongThuViec.TiLeHuongLuong = double.Parse(reader.GetValue(14).ToString());
                                    hopDongThuViec.GioLamViec = double.Parse(reader.GetValue(13).ToString());
                                    var hopDongModel1 = Mapper.Map<CreateHopDongRequest, Models.HopDong>(hopDongThuViec);
                                    listContractRecords.Add(hopDongModel1);
                                }

                                if (!String.IsNullOrEmpty(reader.GetValue(21)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(19)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(20)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(15)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(13)?.ToString()))
                                {
                                    var dateStart = DateTime.Parse(reader.GetValue(19).ToString());
                                    var dateEnd = DateTime.Parse(reader.GetValue(20).ToString());
                                    if (dateStart.AddDays(365) < dateEnd)
                                    {
                                        dateEnd = dateStart.AddDays(365);
                                    }

                                    CreateHopDongRequest hopDongLD1 = new CreateHopDongRequest();
                                    hopDongLD1.TenHopDong = reader.GetValue(21).ToString();
                                    hopDongLD1.MaNhanVien = reader.GetValue(1).ToString();
                                    hopDongLD1.NgayBatDauHopDong = dateStart;
                                    hopDongLD1.NgayKetThucHopDong = dateEnd;
                                    hopDongLD1.loaiHopDong = "Chính thức";
                                    hopDongLD1.TiLeHuongLuong = double.Parse(reader.GetValue(15).ToString());
                                    hopDongLD1.GioLamViec = double.Parse(reader.GetValue(13).ToString());
                                    var hopDongModel2 = Mapper.Map<CreateHopDongRequest, Models.HopDong>(hopDongLD1);
                                    listContractRecords.Add(hopDongModel2);
                                }

                                if (!String.IsNullOrEmpty(reader.GetValue(24)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(22)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(23)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(15)?.ToString())
                                    && !String.IsNullOrEmpty(reader.GetValue(13)?.ToString()))
                                {
                                    CreateHopDongRequest hopDongLD2 = new CreateHopDongRequest();
                                    hopDongLD2.TenHopDong = reader.GetValue(24).ToString();
                                    hopDongLD2.MaNhanVien = reader.GetValue(1).ToString();
                                    hopDongLD2.NgayBatDauHopDong = DateTime.Parse(reader.GetValue(22).ToString());
                                    hopDongLD2.NgayKetThucHopDong = DateTime.Parse(reader.GetValue(23).ToString());
                                    hopDongLD2.loaiHopDong = "Chính thức";
                                    hopDongLD2.TiLeHuongLuong = double.Parse(reader.GetValue(15).ToString());
                                    hopDongLD2.GioLamViec = double.Parse(reader.GetValue(13).ToString());
                                    var hopDongModel3 = Mapper.Map<CreateHopDongRequest, Models.HopDong>(hopDongLD2);
                                    listContractRecords.Add(hopDongModel3);
                                }
                            }
                        } while (reader.NextResult());
                        
                    }
                }

                System.IO.File.Delete(filePath);
            }

            var result = await _nhanVienDAO.UpdateOrInsertListRecordsAsync(listEmployeeRecords.AsEnumerable(), listContractRecords.AsEnumerable());
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
