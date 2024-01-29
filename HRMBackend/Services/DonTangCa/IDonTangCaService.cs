using HRMBackend.Resources.DTO.DonTangCa.Request;
using HRMBackend.Resources.DTO.DonTangCa.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.DonTangCa
{
    public interface IDonTangCaService
    {
        /// <summary>
        /// Chức năng: Tạo mới một DonTangCa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<DonTangCaResponse>> CreateAsync(CreateDonTangCaRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<DonTangCaResponse>>> GetByCodeOrNameAsync(SearchDonTangCaRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<DonTangCaResponse>>> PaginationGetByCodeAndNameAsync(PaginationDonTangCaRequest request);

        /// <summary>
        /// Chức năng: Cập nhật DonTangCa bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<DonTangCaResponse>> UpdateAsync(UpdateDonTangCaRequest request);

    }
}
