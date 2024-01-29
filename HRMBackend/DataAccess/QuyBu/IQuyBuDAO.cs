using HRMBackend.Resources.DTO.QuyBu.Request;

namespace HRMBackend.DataAccess.QuyBu
{
    public interface IQuyBuDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.QuyBu> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.QuyBu data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo QuyBu
        /// </summary>
        /// <param name="QuyBu"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.QuyBu data)> CreateAsync(Models.QuyBu caLamViec);

        /// <summary>
        /// Chức năng: lấy ra QuyBu dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.QuyBu> data, int totalRecords)> PaginationAsync(PaginationQuyBuRequest request);

        /// <summary>
        /// Chức năng: kiểm tra QuyBu đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.QuyBu> data)> GetByCodeOrNameAsync(SearchQuyBuRequest request);

        /// <summary>
        /// Chức năng: cập nhật QuyBu
        /// </summary>
        /// <param name="QuyBu"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.QuyBu data)> UpdateAsync(Models.QuyBu caLamViec);
    }
}
