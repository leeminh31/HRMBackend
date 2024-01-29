using HRMBackend.Resources.DTO.DangKyCa.Request;
using HRMBackend.Resources.DTO.DangKyCa.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.DangKyCa
{
    public interface IDangKyCaService
    {
        /// <summary>
        /// Chức năng: Tạo mới một DangKyCa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<DangKyCaResponse>> CreateAsync(CreateDangKyCaRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<DangKyCaResponse>>> GetByCodeOrNameAsync(SearchDangKyCaRequest request);

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng theo mã code và name được phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginationResult<IEnumerable<DangKyCaResponse>>> PaginationGetByCodeAndNameAsync(PaginationDangKyCaRequest request);

        /// <summary>
        /// Chức năng: Cập nhật DangKyCa bằng name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseResult<DangKyCaResponse>> UpdateAsync(UpdateDangKyCaRequest request);

    }
}
