using HRMBackend.Resources.DTO.DonPhep.Request;

namespace HRMBackend.DataAccess.DonPhep
{
    public interface IDonPhepDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.DonPhep> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.DonPhep data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo DonPhep
        /// </summary>
        /// <param name="DonPhep"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DonPhep data)> CreateAsync(Models.DonPhep caLamViec);

        /// <summary>
        /// Chức năng: lấy ra DonPhep dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DonPhep> data, int totalRecords)> PaginationAsync(PaginationDonPhepRequest request);

        /// <summary>
        /// Chức năng: kiểm tra DonPhep đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DonPhep> data)> GetByCodeOrNameAsync(SearchDonPhepRequest request);

        /// <summary>
        /// Chức năng: cập nhật DonPhep
        /// </summary>
        /// <param name="DonPhep"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DonPhep data)> UpdateAsync(Models.DonPhep caLamViec);
    }
}
}
