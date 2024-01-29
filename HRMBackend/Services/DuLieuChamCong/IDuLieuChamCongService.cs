using HRMBackend.Resources.DTO.DuLieuChamCong.Request;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.DuLieuChamCong
{
    public interface IDuLieuChamCongService
    {
        /// <summary>
        /// Chức năng: Tạo mới một DuLieuChamCong
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<DuLieuChamCongResponse>> CreateAsync(CreateDuLieuChamCongRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<DuLieuChamCongResponse>>> GetByCodeOrNameAsync(SearchDuLieuChamCongRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<DuLieuChamCongResponse>>> PaginationGetByCodeAndNameAsync(PaginationDuLieuChamCongRequest request);

        /// <summary>
        /// Chức năng: Cập nhật DuLieuChamCong bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<DuLieuChamCongResponse>> UpdateAsync(UpdateDuLieuChamCongRequest request);

    }
}
