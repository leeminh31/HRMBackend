using HRMBackend.Resources.DTO.NhanVien.Request;

namespace HRMBackend.DataAccess.NhanVien
{
    public interface INhanVienDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.NhanVien> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.NhanVien data)> GetByIDAsync(string maNhanVien);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.NhanVien> data)> GetByParamsAsync(string? maNhanVien, int? maPhongBan, int? idVanTay, string? chucVu, string? hoTen);

        /// <summary>
        /// Chức năng: tạo NhanVien
        /// </summary>
        /// <param name="NhanVien"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.NhanVien data)> CreateAsync(Models.NhanVien caLamViec);

        /// <summary>
        /// Chức năng: lấy ra NhanVien dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<(bool isSuccess, IEnumerable<Models.NhanVien> data, int totalRecords)> PaginationAsync(PaginationNhanVienRequest request);

        /// <summary>
        /// Chức năng: kiểm tra NhanVien đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<(bool isSuccess, IEnumerable<Models.NhanVien> data)> GetByCodeOrNameAsync(SearchNhanVienRequest request);

        /// <summary>
        /// Chức năng: cập nhật NhanVien
        /// </summary>
        /// <param name="NhanVien"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.NhanVien data)> UpdateAsync(Models.NhanVien caLamViec);

        Task<(bool hasValue, IEnumerable<string> data)> GetAllEmployeeIdByNameAsync(string? hoTen);

        Task<(bool hasValue, Models.NhanVien data)> GetByIdVanTayAsync(int? idVanTay, string? maNhanVien);

        Task<(bool isSuccess, IEnumerable<Models.NhanVien> data)> UpdateOrInsertListRecordsAsync(IEnumerable<Models.NhanVien> request, IEnumerable<Models.HopDong> request2);
    }
}
