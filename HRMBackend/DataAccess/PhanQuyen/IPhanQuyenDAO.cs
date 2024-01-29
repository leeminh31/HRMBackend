using HRMBackend.Resources.DTO.PhanQuyen.Request;

namespace HRMBackend.DataAccess.PhanQuyen
{
    public interface IPhanQuyenDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.PhanQuyen> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.PhanQuyen data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo PhanQuyen
        /// </summary>
        /// <param name="PhanQuyen"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.PhanQuyen data)> CreateAsync(Models.PhanQuyen caLamViec);

        /// <summary>
        /// Chức năng: lấy ra PhanQuyen dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.PhanQuyen> data, int totalRecords)> PaginationAsync(PaginationPhanQuyenRequest request);

        /// <summary>
        /// Chức năng: kiểm tra PhanQuyen đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.PhanQuyen> data)> GetByCodeOrNameAsync(SearchPhanQuyenRequest request);

        /// <summary>
        /// Chức năng: cập nhật PhanQuyen
        /// </summary>
        /// <param name="PhanQuyen"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.PhanQuyen data)> UpdateAsync(Models.PhanQuyen caLamViec);
    }
}
