using HRMBackend.Resources.DTO.QuyPhep.Request;

namespace HRMBackend.DataAccess.QuyPhep
{
    public interface IQuyPhepDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.QuyPhep> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.QuyPhep data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo QuyPhep
        /// </summary>
        /// <param name="QuyPhep"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.QuyPhep data)> CreateAsync(Models.QuyPhep caLamViec);

        /// <summary>
        /// Chức năng: lấy ra QuyPhep dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.QuyPhep> data, int totalRecords)> PaginationAsync(PaginationQuyPhepRequest request);

        /// <summary>
        /// Chức năng: kiểm tra QuyPhep đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.QuyPhep> data)> GetByCodeOrNameAsync(SearchQuyPhepRequest request);

        /// <summary>
        /// Chức năng: cập nhật QuyPhep
        /// </summary>
        /// <param name="QuyPhep"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.QuyPhep data)> UpdateAsync(Models.QuyPhep caLamViec);
    }
}
