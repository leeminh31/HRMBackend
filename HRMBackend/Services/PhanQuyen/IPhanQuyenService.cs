using HRMBackend.Resources.DTO.PhanQuyen.Request;
using HRMBackend.Resources.DTO.PhanQuyen.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.PhanQuyen
{
    public interface IPhanQuyenService
    {
        /// <summary>
        /// Chức năng: Tạo mới một PhanQuyen
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<PhanQuyenResponse>> CreateAsync(CreatePhanQuyenRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<PhanQuyenResponse>>> GetByCodeOrNameAsync(SearchPhanQuyenRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<PhanQuyenResponse>>> PaginationGetByCodeAndNameAsync(PaginationPhanQuyenRequest request);

        /// <summary>
        /// Chức năng: Cập nhật PhanQuyen bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<PhanQuyenResponse>> UpdateAsync(UpdatePhanQuyenRequest request);

    }
}
