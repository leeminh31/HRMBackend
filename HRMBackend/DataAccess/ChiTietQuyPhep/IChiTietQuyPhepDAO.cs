using HRMBackend.Resources.DTO.ChiTietQuyPhep.Request;

namespace HRMBackend.DataAccess.ChiTietQuyPhep
{
    public interface IChiTietQuyPhepDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.ChiTietQuyPhep> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.ChiTietQuyPhep data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo ChiTietQuyPhep
        /// </summary>
        /// <param name="ChiTietQuyPhep"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.ChiTietQuyPhep data)> CreateAsync(Models.ChiTietQuyPhep caLamViec);

        /// <summary>
        /// Chức năng: lấy ra ChiTietQuyPhep dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyPhep> data, int totalRecords)> PaginationAsync(PaginationChiTietQuyPhepRequest request);

        /// <summary>
        /// Chức năng: kiểm tra ChiTietQuyPhep đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyPhep> data)> GetByCodeOrNameAsync(SearchChiTietQuyPhepRequest request);

        /// <summary>
        /// Chức năng: cập nhật ChiTietQuyPhep
        /// </summary>
        /// <param name="ChiTietQuyPhep"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.ChiTietQuyPhep data)> UpdateAsync(Models.ChiTietQuyPhep caLamViec);
    }
}
