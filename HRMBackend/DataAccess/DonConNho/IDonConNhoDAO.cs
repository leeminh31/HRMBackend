using HRMBackend.Resources.DTO.DonConNho.Request;

namespace HRMBackend.DataAccess.DonConNho
{
    public interface IDonConNhoDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.DonConNho> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.DonConNho data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo DonConNho
        /// </summary>
        /// <param name="DonConNho"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DonConNho data)> CreateAsync(Models.DonConNho caLamViec);

        /// <summary>
        /// Chức năng: lấy ra DonConNho dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DonConNho> data, int totalRecords)> PaginationAsync(PaginationDonConNhoRequest request);

        /// <summary>
        /// Chức năng: kiểm tra DonConNho đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DonConNho> data)> GetByCodeOrNameAsync(SearchDonConNhoRequest request);

        /// <summary>
        /// Chức năng: cập nhật DonConNho
        /// </summary>
        /// <param name="DonConNho"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DonConNho data)> UpdateAsync(Models.DonConNho caLamViec);
    }
}
