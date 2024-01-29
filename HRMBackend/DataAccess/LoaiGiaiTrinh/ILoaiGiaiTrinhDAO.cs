using HRMBackend.Resources.DTO.LoaiGiaiTrinh.Request;

namespace HRMBackend.DataAccess.LoaiGiaiTrinh
{
    public interface ILoaiGiaiTrinhDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.LoaiGiaiTrinh> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.LoaiGiaiTrinh data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo LoaiGiaiTrinh
        /// </summary>
        /// <param name="LoaiGiaiTrinh"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.LoaiGiaiTrinh data)> CreateAsync(Models.LoaiGiaiTrinh caLamViec);

        /// <summary>
        /// Chức năng: lấy ra LoaiGiaiTrinh dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.LoaiGiaiTrinh> data, int totalRecords)> PaginationAsync(PaginationLoaiGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: kiểm tra LoaiGiaiTrinh đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.LoaiGiaiTrinh> data)> GetByCodeOrNameAsync(SearchLoaiGiaiTrinhRequest request);

        /// <summary>
        /// Chức năng: cập nhật LoaiGiaiTrinh
        /// </summary>
        /// <param name="LoaiGiaiTrinh"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.LoaiGiaiTrinh data)> UpdateAsync(Models.LoaiGiaiTrinh caLamViec);
    }
}
