using HRMBackend.Resources.DTO.TaiKhoan.Request;
using HRMBackend.Resources.DTO.TaiKhoan.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.TaiKhoan
{
    public interface ITaiKhoanService
    {
        /// <summary>
        /// Chức năng: Tạo mới một TaiKhoan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<TaiKhoanResponse>> CreateAsync(CreateTaiKhoanRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<TaiKhoanResponse>>> GetByCodeOrNameAsync(SearchTaiKhoanRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<PaginationResult<IEnumerable<TaiKhoanResponse>>> PaginationGetByCodeAndNameAsync(PaginationTaiKhoanRequest request);

        /// <summary>
        /// Chức năng: Cập nhật TaiKhoan bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //Task<BaseResult<TaiKhoanResponse>> UpdateAsync(UpdateTaiKhoanRequest request);
        Task<BaseResult<TaiKhoanResponse>> ChangePasswordAsync(ChangePasswordRequest request);

        Task<BaseResult<IEnumerable<string>>> GetEmployeeIdAsync();

    }
}
