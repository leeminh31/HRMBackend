using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Resources.DTO.DonPhep.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.DonPhep
{
    public interface IDonPhepService
    {
        /// <summary>
        /// Chức năng: Tạo mới một DonPhep
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<DonPhepResponse>> CreateAsync(CreateDonPhepRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<DonPhepResponse>>> GetByCodeOrNameAsync(SearchDonPhepRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<DonPhepResponse>>> PaginationGetByCodeAndNameAsync(PaginationDonPhepRequest request);

        /// <summary>
        /// Chức năng: Cập nhật DonPhep bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<DonPhepResponse>> UpdateAsync(UpdateDonPhepRequest request);

    }
}
