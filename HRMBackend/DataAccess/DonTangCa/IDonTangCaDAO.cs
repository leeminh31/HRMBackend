using HRMBackend.Resources.DTO.DonTangCa.Request;

namespace HRMBackend.DataAccess.DonTangCa
{
    public interface IDonTangCaDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.DonTangCa> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.DonTangCa data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo DonTangCa
        /// </summary>
        /// <param name="DonTangCa"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DonTangCa data)> CreateAsync(Models.DonTangCa caLamViec);

        /// <summary>
        /// Chức năng: lấy ra DonTangCa dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DonTangCa> data, int totalRecords)> PaginationAsync(PaginationDonTangCaRequest request);

        /// <summary>
        /// Chức năng: kiểm tra DonTangCa đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.DonTangCa> data)> GetByCodeOrNameAsync(SearchDonTangCaRequest request);

        /// <summary>
        /// Chức năng: cập nhật DonTangCa
        /// </summary>
        /// <param name="DonTangCa"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.DonTangCa data)> UpdateAsync(Models.DonTangCa caLamViec);
    }
}
