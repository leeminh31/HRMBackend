using HRMBackend.Resources.DTO.DonConNho.Request;
using HRMBackend.Resources.DTO.DonConNho.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.DonConNho
{
    public interface IDonConNhoService
    {
        /// <summary>
        /// Chức năng: Tạo mới một DonConNho
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<DonConNhoResponse>> CreateAsync(CreateDonConNhoRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<DonConNhoResponse>>> GetByCodeOrNameAsync(SearchDonConNhoRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<DonConNhoResponse>>> PaginationGetByCodeAndNameAsync(PaginationDonConNhoRequest request);

        /// <summary>
        /// Chức năng: Cập nhật DonConNho bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<DonConNhoResponse>> UpdateAsync(UpdateDonConNhoRequest request);

    }
}
