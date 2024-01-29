using HRMBackend.Resources.DTO.GiaiTrinh.Request;

namespace HRMBackend.DataAccess.GiaiTrinh
{
    public interface IGiaiTrinhDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.GiaiTrinh> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.GiaiTrinh data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo GiaiTrinh
        /// </summary>
        /// <param name="GiaiTrinh"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.GiaiTrinh data)> CreateAsync(Models.GiaiTrinh caLamViec);

        /// <summary>
        /// Chức năng: lấy ra GiaiTrinh dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.GiaiTrinh> data, int totalRecords)> PaginationAsync(PaginationGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: kiểm tra GiaiTrinh đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.GiaiTrinh> data)> GetByCodeOrNameAsync(SearchGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: cập nhật GiaiTrinh
        /// </summary>
        /// <param name="GiaiTrinh"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.GiaiTrinh data)> UpdateAsync(Models.GiaiTrinh caLamViec);
    }
}
