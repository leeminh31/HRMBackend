using HRMBackend.Resources.DTO.CaLamViec.Request;
using HRMBackend.Resources.DTO.CaLamViec.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.CaLamViec
{
    public interface ICaLamViecService
    {
        /// <summary>
        /// Chức năng: Tạo mới một CaLamViec
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<CaLamViecResponse>> CreateAsync(CreateCaLamViecRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<CaLamViecResponse>>> GetByCodeOrNameAsync(SearchCaLamViecRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<CaLamViecResponse>>> PaginationGetByCodeAndNameAsync(PaginationCaLamViecRequest request);

        /// <summary>
        /// Chức năng: Cập nhật CaLamViec bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<CaLamViecResponse>> UpdateAsync(UpdateCaLamViecRequest request);

    }
}
