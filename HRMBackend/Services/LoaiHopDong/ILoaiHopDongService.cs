using HRMBackend.Resources.DTO.LoaiHopDong.Request;
using HRMBackend.Resources.DTO.LoaiHopDong.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.LoaiHopDong
{
    public interface ILoaiHopDongService
    {
        /// <summary>
        /// Chức năng: Tạo mới một LoaiHopDong
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<LoaiHopDongResponse>> CreateAsync(CreateLoaiHopDongRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<LoaiHopDongResponse>>> GetByCodeOrNameAsync(SearchLoaiHopDongRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<LoaiHopDongResponse>>> PaginationGetByCodeAndNameAsync(PaginationLoaiHopDongRequest request);

        /// <summary>
        /// Chức năng: Cập nhật LoaiHopDong bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<LoaiHopDongResponse>> UpdateAsync(UpdateLoaiHopDongRequest request);

    }
}
