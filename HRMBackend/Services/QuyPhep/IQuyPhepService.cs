using HRMBackend.Resources.DTO.QuyPhep.Request;
using HRMBackend.Resources.DTO.QuyPhep.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.QuyPhep
{
    public interface IQuyPhepService
    {
        /// <summary>
        /// Chức năng: Tạo mới một QuyPhep
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<QuyPhepResponse>> CreateAsync(CreateQuyPhepRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<QuyPhepResponse>>> GetByCodeOrNameAsync(SearchQuyPhepRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<QuyPhepResponse>>> PaginationGetByCodeAndNameAsync(PaginationQuyPhepRequest request);

        /// <summary>
        /// Chức năng: Cập nhật QuyPhep bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<QuyPhepResponse>> UpdateAsync(UpdateQuyPhepRequest request);

    }
}
