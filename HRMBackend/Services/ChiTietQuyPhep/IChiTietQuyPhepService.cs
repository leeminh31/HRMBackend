using HRMBackend.Resources.DTO.ChiTietQuyPhep.Request;
using HRMBackend.Resources.DTO.ChiTietQuyPhep.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.ChiTietQuyPhep
{
    public interface IChiTietQuyPhepService
    {
        /// <summary>
        /// Chức năng: Tạo mới một ChiTietQuyPhep
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<ChiTietQuyPhepResponse>> CreateAsync(CreateChiTietQuyPhepRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<ChiTietQuyPhepResponse>>> GetByCodeOrNameAsync(SearchChiTietQuyPhepRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<ChiTietQuyPhepResponse>>> PaginationGetByCodeAndNameAsync(PaginationChiTietQuyPhepRequest request);

        /// <summary>
        /// Chức năng: Cập nhật ChiTietQuyPhep bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<ChiTietQuyPhepResponse>> UpdateAsync(UpdateChiTietQuyPhepRequest request);

    }
}
