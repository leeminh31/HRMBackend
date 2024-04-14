using AutoMapper;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using HRMBackend.Services.NhanVien;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.DTO.NhanVien.Response;
using HRMBackend.Resources.DTO.NhanVien.Request;
using HRMBackend.Extensions;
using HRMBackend.Resources.DTO.HopDong.Request;
using HRMBackend.Resources.DTO.TaiKhoan.Request;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.DataAccess.TaiKhoan;
using HRMBackend.Resources.DTO.TaiKhoan.Response;

namespace HRMBackend.Services.NhanVien
{
    public class NhanVienService : BaseService, INhanVienService
    {
        #region Property
        private readonly INhanVienDAO _nhanVienDAO;
        private readonly ITaiKhoanDAO _taiKhoanDAO;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public NhanVienService(INhanVienDAO nhanVienDAO,
            ITaiKhoanDAO taiKhoanDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._nhanVienDAO = nhanVienDAO;
            this._taiKhoanDAO = taiKhoanDAO;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<BaseResult<NhanVienResponse>> CreateAsync(CreateNhanVienRequest request)
        {
            // Mapping Resource to NhanVien
            var airport = Mapper.Map<CreateNhanVienRequest, Models.NhanVien>(request);
            //SearchNhanVienRequest searchRequest = new SearchNhanVienRequest() { IDVanTay = request.IDVanTay, ChucVu = null, HoTen= null, MaNhanVien = null, MaPhongBan = null };
            //Tìm mã nhân viên đã tồn tại hay chưa?
            var records = await _nhanVienDAO.GetByIDAsync(request.MaNhanVien);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._553, data: Mapper.Map<NhanVienResponse>(records.data));
            }

            var records2 = await _nhanVienDAO.GetByParamsAsync(null,null,request.IDVanTay,null, null);
            if (records2.isSuccess)
            {
                return GetBaseResult(CodeMessage._554, data: Mapper.Map<NhanVienResponse>(records2.data.First()));
            }

            var result = await _nhanVienDAO.CreateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<NhanVienResponse>(result.data));
            else
                return GetBaseResult<NhanVienResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<TaiKhoanResponse>> CreateListAsync(string id)
        {
            //SearchNhanVienRequest searchRequest = new SearchNhanVienRequest() { IDVanTay = request.IDVanTay, ChucVu = null, HoTen= null, MaNhanVien = null, MaPhongBan = null };
            //Tìm mã nhân viên đã tồn tại hay chưa?
            var listNhanVien = await _nhanVienDAO.GetByParamsAsync(null, null, null, null, null);
            string[] employeeId = id.Split(",");

            var listEmployeeAccount = new List<Models.TaiKhoan>();
            foreach (var employee in employeeId)
            {
                CreateTaiKhoanRequest taiKhoan = new CreateTaiKhoanRequest();
                taiKhoan.PhanQuyen = "USER";
                taiKhoan.MaNhanVien = listNhanVien.data.FirstOrDefault(nv => nv.MaNhanVien == employee).MaNhanVien;
                taiKhoan.MatKhau = RemoveWhitespaceDiacriticsAndToLower(listNhanVien.data.FirstOrDefault(nv => nv.MaNhanVien == employee).HoTen);
                taiKhoan.TenDangNhap = employee;
                var hopDongModel1 = Mapper.Map<CreateTaiKhoanRequest, Models.TaiKhoan>(taiKhoan);
                listEmployeeAccount.Add(hopDongModel1);
            }

            var result = await _taiKhoanDAO.CreateListQueryAsync(listEmployeeAccount.AsEnumerable());
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<TaiKhoanResponse>(result.data));
            else
                return GetBaseResult<TaiKhoanResponse>(CodeMessage._209, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<NhanVienResponse>>> GetByParamsAsync(string? maNhanVien, int? maPhongBan, int? idVanTay, string? chucVu, string? hoTen)
        {
            var records = await _nhanVienDAO.GetByParamsAsync(maNhanVien, maPhongBan, idVanTay, chucVu, hoTen);
            if (records.isSuccess)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<NhanVienResponse>>(records.data));
            }
            return GetBaseResult<IEnumerable<NhanVienResponse>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<IEnumerable<string>>> GetAllEmployeeIdByNameAsync(string hoTen)
        {
            var records = await _nhanVienDAO.GetAllEmployeeIdByNameAsync(hoTen);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<IEnumerable<string>>(records.data));
            }
            return GetBaseResult<IEnumerable<string>>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<NhanVienResponse>> GetByIDAsync(string maNhanVien)
        {
            var records = await _nhanVienDAO.GetByIDAsync(maNhanVien);
            if (records.hasValue)
            {
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<NhanVienResponse>(records.data));
            }
            return GetBaseResult<NhanVienResponse>(CodeMessage._545, status: StatusEnum.Failed);
        }

        public async Task<BaseResult<NhanVienResponse>> UpdateAsync(UpdateNhanVienRequest request)
        {
            // Mapping Resource to NhanVien
            var airport = Mapper.Map<UpdateNhanVienRequest, Models.NhanVien>(request);

            var records2 = await _nhanVienDAO.GetByIdVanTayAsync(request.IDVanTay, request.MaNhanVien);
            if (records2.hasValue)
            {
                return GetBaseResult(CodeMessage._554, data: Mapper.Map<NhanVienResponse>(records2.data));
            }

            var result = await _nhanVienDAO.UpdateAsync(airport);
            await _unitOfWork.SaveChangesAsync();

            if (result.isSuccess)
                return GetBaseResult(CodeMessage._200, data: Mapper.Map<NhanVienResponse>(result.data));
            else
                return GetBaseResult<NhanVienResponse>(CodeMessage._236, status: StatusEnum.Failed);
        }

        public static string RemoveWhitespaceDiacriticsAndToLower(string text)
        {
            // Loại bỏ dấu từ chuỗi và chuyển đổi ký tự viết hoa thành viết thường
            string decomposed = text.Normalize(NormalizationForm.FormD);
            Regex regexDiacritics = new Regex(@"\p{M}");
            string withoutDiacritics = regexDiacritics.Replace(decomposed, string.Empty).Normalize(NormalizationForm.FormC);
            string lowerCase = withoutDiacritics.ToLower();

            // Loại bỏ khoảng trắng từ chuỗi đã chuyển đổi
            return Regex.Replace(lowerCase, @"\s+", string.Empty);
        }
    }
}
