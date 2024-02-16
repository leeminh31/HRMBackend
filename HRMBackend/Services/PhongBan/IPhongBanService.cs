using HRMBackend.Resources.DTO.PhongBan.Request;
using HRMBackend.Resources.DTO.PhongBan.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.PhongBan
{
    public interface IPhongBanService
    {
        /// <summary>
        /// Chức năng: Tạo mới một PhongBan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<PhongBanResponse>> CreateAsync(CreatePhongBanRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<PhongBanResponse>>> GetByCodeOrNameAsync(SearchPhongBanRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<PhongBanResponse>>> GetByParamsAsync(SearchPhongBanRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<PhongBanResponse>>> PaginationGetByCodeAndNameAsync(PaginationPhongBanRequest request);

        /// <summary>
        /// Chức năng: Cập nhật PhongBan bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //Task<BaseResult<PhongBanResponse>> UpdateAsync(UpdatePhongBanRequest request);

    }
}
