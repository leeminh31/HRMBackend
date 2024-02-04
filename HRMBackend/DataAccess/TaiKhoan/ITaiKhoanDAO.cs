using HRMBackend.Resources.DTO.TaiKhoan.Request;

namespace HRMBackend.DataAccess.TaiKhoan
{
    public interface ITaiKhoanDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.TaiKhoan> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.TaiKhoan data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo TaiKhoan
        /// </summary>
        /// <param name="TaiKhoan"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.TaiKhoan data)> CreateAsync(Models.TaiKhoan caLamViec);

        /// <summary>
        /// Chức năng: lấy ra TaiKhoan dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<(bool isSuccess, IEnumerable<Models.TaiKhoan> data, int totalRecords)> PaginationAsync(PaginationTaiKhoanRequest request);

        /// <summary>
        /// Chức năng: kiểm tra TaiKhoan đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.TaiKhoan> data)> GetByCodeOrNameAsync(SearchTaiKhoanRequest request);

        /// <summary>
        /// Chức năng: cập nhật TaiKhoan
        /// </summary>
        /// <param name="TaiKhoan"></param>
        /// <returns></returns>
        //Task<(bool isSuccess, Models.TaiKhoan data)> UpdateAsync(Models.TaiKhoan caLamViec);
    }
}
