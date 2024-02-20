using HRMBackend.Resources.DTO.NhanVien.Request;
using HRMBackend.Resources.DTO.NhanVien.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.NhanVien
{
    public interface INhanVienService
    {
        /// <summary>
        /// Chức năng: Tạo mới một NhanVien
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<NhanVienResponse>> CreateAsync(CreateNhanVienRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<BaseResult<IEnumerable<NhanVienResponse>>> GetByCodeOrNameAsync(SearchNhanVienRequest request);

        /// <summary>
        /// Chức năng: lấy thông tin nhân viên
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<NhanVienResponse>> GetByIDAsync(string maNhanVien);

        Task<BaseResult<IEnumerable<NhanVienResponse>>> GetByParamsAsync(string? maNhanVien, int? maPhongBan, int? idVanTay, string? chucVu, string? hoTen);

        Task<BaseResult<IEnumerable<string>>> GetAllEmployeeIdByNameAsync(string hoTen);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<PaginationResult<IEnumerable<NhanVienResponse>>> PaginationGetByCodeAndNameAsync(PaginationNhanVienRequest request);

        /// <summary>
        /// Chức năng: Cập nhật NhanVien bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<NhanVienResponse>> UpdateAsync(UpdateNhanVienRequest request);

    }
}
