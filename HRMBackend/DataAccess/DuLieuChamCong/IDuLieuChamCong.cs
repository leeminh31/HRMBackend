using HRMBackend.Resources.DTO.DuLieuChamCong.Request;

namespace HRMBackend.DataAccess.DuLieuChamCong
{
    public interface IDuLieuChamCongDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.DuLieuChamCong> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.DuLieuChamCong data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo DuLieuChamCong
        /// </summary>
        /// <param name="DuLieuChamCong"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DuLieuChamCong data)> CreateAsync(Models.DuLieuChamCong caLamViec);

        /// <summary>
        /// Chức năng: lấy ra DuLieuChamCong dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DuLieuChamCong> data, int totalRecords)> PaginationAsync(PaginationDuLieuChamCongRequest request);

        /// <summary>
        /// Chức năng: kiểm tra DuLieuChamCong đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DuLieuChamCong> data)> GetByCodeOrNameAsync(SearchDuLieuChamCongRequest request);

        /// <summary>
        /// Chức năng: cập nhật DuLieuChamCong
        /// </summary>
        /// <param name="DuLieuChamCong"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DuLieuChamCong data)> UpdateAsync(Models.DuLieuChamCong caLamViec);
    }
}
