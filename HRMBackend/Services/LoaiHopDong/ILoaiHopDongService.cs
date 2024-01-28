using HRMBackend.Results;

namespace HRMBackend.Services.LoaiHopDong
{
    public interface ILoaiHopDongService
    {
        /// <summary>
        /// Chức năng: Tạo mới một Airport
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<AirportResponse>> CreateAsync(CreateAirportRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<AirportResponse>>> GetByCodeOrNameAsync(SearchAirportRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<AirportResponse>>> PaginationGetByCodeAndNameAsync(PaginationAirportRequest request);

        /// <summary>
        /// Chức năng: Cập nhật Airport bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<AirportResponse>> UpdateAsync(UpdateAirportRequest request);

    }
}
