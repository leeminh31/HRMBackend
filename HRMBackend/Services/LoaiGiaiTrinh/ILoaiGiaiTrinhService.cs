using HRMBackend.Resources.DTO.LoaiGiaiTrinh.Request;
using HRMBackend.Resources.DTO.LoaiGiaiTrinh.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.LoaiGiaiTrinh
{
    public interface ILoaiGiaiTrinhService
    {
        /// <summary>
        /// Chức năng: Tạo mới một LoaiGiaiTrinh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<LoaiGiaiTrinhResponse>> CreateAsync(CreateLoaiGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<LoaiGiaiTrinhResponse>>> GetByCodeOrNameAsync(SearchLoaiGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<LoaiGiaiTrinhResponse>>> PaginationGetByCodeAndNameAsync(PaginationLoaiGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: Cập nhật LoaiGiaiTrinh bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<LoaiGiaiTrinhResponse>> UpdateAsync(UpdateLoaiGiaiTrinhRequest request);

    }
}
