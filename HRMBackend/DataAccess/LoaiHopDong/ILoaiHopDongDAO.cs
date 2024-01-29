using HRMBackend.Resources.DTO.LoaiHopDong.Request;

namespace HRMBackend.DataAccess.LoaiHopDong
{
    public interface ILoaiHopDongDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.LoaiHopDong> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.LoaiHopDong data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo LoaiHopDong
        /// </summary>
        /// <param name="LoaiHopDong"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.LoaiHopDong data)> CreateAsync(Models.LoaiHopDong caLamViec);

        /// <summary>
        /// Chức năng: lấy ra LoaiHopDong dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.LoaiHopDong> data, int totalRecords)> PaginationAsync(PaginationLoaiHopDongRequest request);

        /// <summary>
        /// Chức năng: kiểm tra LoaiHopDong đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.LoaiHopDong> data)> GetByCodeOrNameAsync(SearchLoaiHopDongRequest request);

        /// <summary>
        /// Chức năng: cập nhật LoaiHopDong
        /// </summary>
        /// <param name="LoaiHopDong"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.LoaiHopDong data)> UpdateAsync(Models.LoaiHopDong caLamViec);
    }
}
