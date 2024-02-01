using HRMBackend.Resources.DTO.ChiTietQuyBu.Request;
using HRMBackend.Resources.DTO.ChiTietQuyBu.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.ChiTietQuyBu
{
    public interface IChiTietQuyBuService
    {
        /// <summary>
        /// Chức năng: Tạo mới một ChiTietQuyBu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<ChiTietQuyBuResponse>> CreateAsync(CreateChiTietQuyBuRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<ChiTietQuyBuResponse>>> GetByCodeOrNameAsync(SearchChiTietQuyBuRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<ChiTietQuyBuResponse>>> PaginationGetByCodeAndNameAsync(PaginationChiTietQuyBuRequest request);

        /// <summary>
        /// Chức năng: Cập nhật ChiTietQuyBu bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<ChiTietQuyBuResponse>> UpdateAsync(UpdateChiTietQuyBuRequest request);

    }
}
