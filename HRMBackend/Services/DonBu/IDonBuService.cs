using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Resources.DTO.DonBu.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.DonBu
{
    public interface IDonBuService
    {
        /// <summary>
        /// Chức năng: Tạo mới một DonBu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<DonBuResponse>> CreateAsync(CreateDonBuRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<DonBuResponse>>> GetByCodeOrNameAsync(SearchDonBuRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<DonBuResponse>>> PaginationGetByCodeAndNameAsync(PaginationDonBuRequest request);

        /// <summary>
        /// Chức năng: Cập nhật DonBu bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<DonBuResponse>> UpdateAsync(UpdateDonBuRequest request);

    }
}
}
