using HRMBackend.Resources.DTO.GiaiTrinh.Request;
using HRMBackend.Resources.DTO.GiaiTrinh.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.GiaiTrinh
{
    public interface IGiaiTrinhService
    {
        /// <summary>
        /// Chức năng: Tạo mới một GiaiTrinh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<GiaiTrinhResponse>> CreateAsync(CreateGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<GiaiTrinhResponse>>> GetByCodeOrNameAsync(SearchGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<GiaiTrinhResponse>>> PaginationGetByCodeAndNameAsync(PaginationGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: Cập nhật GiaiTrinh bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<GiaiTrinhResponse>> UpdateAsync(UpdateGiaiTrinhRequest request);

    }
}
