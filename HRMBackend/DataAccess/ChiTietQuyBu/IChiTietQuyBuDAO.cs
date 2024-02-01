using HRMBackend.Resources.DTO.ChiTietQuyBu.Request;

namespace HRMBackend.DataAccess.ChiTietQuyBu
{
    public interface IChiTietQuyBuDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.ChiTietQuyBu> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.ChiTietQuyBu data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo ChiTietQuyBu
        /// </summary>
        /// <param name="ChiTietQuyBu"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.ChiTietQuyBu data)> CreateAsync(Models.ChiTietQuyBu caLamViec);

        /// <summary>
        /// Chức năng: lấy ra ChiTietQuyBu dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyBu> data, int totalRecords)> PaginationAsync(PaginationChiTietQuyBuRequest request);

        /// <summary>
        /// Chức năng: kiểm tra ChiTietQuyBu đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyBu> data)> GetByCodeOrNameAsync(SearchChiTietQuyBuRequest request);

        /// <summary>
        /// Chức năng: cập nhật ChiTietQuyBu
        /// </summary>
        /// <param name="ChiTietQuyBu"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.ChiTietQuyBu data)> UpdateAsync(Models.ChiTietQuyBu caLamViec);
    }
}
