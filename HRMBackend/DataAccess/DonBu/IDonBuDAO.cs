using HRMBackend.Resources.DTO.DonBu.Request;

namespace HRMBackend.DataAccess.DonBu
{
    public interface IDonBuDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.DonBu> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.DonBu data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo DonBu
        /// </summary>
        /// <param name="DonBu"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DonBu data)> CreateAsync(Models.DonBu caLamViec);

        /// <summary>
        /// Chức năng: lấy ra DonBu dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DonBu> data, int totalRecords)> PaginationAsync(PaginationDonBuRequest request);

        /// <summary>
        /// Chức năng: kiểm tra DonBu đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DonBu> data)> GetByCodeOrNameAsync(SearchDonBuRequest request);

        /// <summary>
        /// Chức năng: cập nhật DonBu
        /// </summary>
        /// <param name="DonBu"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DonBu data)> UpdateAsync(Models.DonBu caLamViec);
    }
}
