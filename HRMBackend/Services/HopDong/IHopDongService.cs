using HRMBackend.Resources.DTO.HopDong.Request;
using HRMBackend.Resources.DTO.HopDong.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.HopDong
{
    public interface IHopDongService
    {
        /// <summary>
        /// Chức năng: Tạo mới một HopDong
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<HopDongResponse>> CreateAsync(CreateHopDongRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<BaseResult<IEnumerable<HopDongResponse>>> GetByCodeOrNameAsync(SearchHopDongRequest request);

        /// <summary>
        /// Chức năng: lấy thông tin nhân viên
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<HopDongResponse>>> GetByIDAsync(string maHopDong);

        Task<BaseResult<IEnumerable<HopDongResponse>>> GetByParamsAsync(string? tenHopDong, string? tenNhanVien, string? loaiHopDong);

        Task<BaseResult<IEnumerable<HopDongResponse>>> GetAllContractAsync();

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<PaginationResult<IEnumerable<HopDongResponse>>> PaginationGetByCodeAndNameAsync(PaginationHopDongRequest request);

        /// <summary>
        /// Chức năng: Cập nhật HopDong bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<HopDongResponse>> UpdateAsync(UpdateHopDongRequest request);

    }
}
