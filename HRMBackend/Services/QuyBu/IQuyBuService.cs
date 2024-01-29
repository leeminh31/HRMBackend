using HRMBackend.Resources.DTO.QuyBu.Request;
using HRMBackend.Resources.DTO.QuyBu.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.QuyBu
{
    public interface IQuyBuService
    {
        /// <summary>
        /// Chức năng: Tạo mới một QuyBu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<QuyBuResponse>> CreateAsync(CreateQuyBuRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<QuyBuResponse>>> GetByCodeOrNameAsync(SearchQuyBuRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<QuyBuResponse>>> PaginationGetByCodeAndNameAsync(PaginationQuyBuRequest request);

        /// <summary>
        /// Chức năng: Cập nhật QuyBu bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<QuyBuResponse>> UpdateAsync(UpdateQuyBuRequest request);

    }
}
