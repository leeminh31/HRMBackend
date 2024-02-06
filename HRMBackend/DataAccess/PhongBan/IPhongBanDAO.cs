using HRMBackend.Resources.DTO.PhongBan.Request;

namespace HRMBackend.DataAccess.PhongBan
{
    public interface IPhongBanDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách mã phòng ban và tên phòng ban
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.PhongBan> data)> GetAllPhongBanAsync();
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.PhongBan> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        //Task<(bool hasValue, Models.PhongBan data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo PhongBan
        /// </summary>
        /// <param name="PhongBan"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.PhongBan data)> CreateAsync(Models.PhongBan caLamViec);

        /// <summary>
        /// Chức năng: lấy ra PhongBan dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.PhongBan> data, int totalRecords)> PaginationAsync(PaginationPhongBanRequest request);

        /// <summary>
        /// Chức năng: kiểm tra PhongBan đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.PhongBan> data)> GetByCodeOrNameAsync(SearchPhongBanRequest request);

        /// <summary>
        /// Chức năng: cập nhật PhongBan
        /// </summary>
        /// <param name="PhongBan"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.PhongBan data)> UpdateAsync(Models.PhongBan caLamViec);
    }
}
