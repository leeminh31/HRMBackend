namespace HRMBackend.DataAccess.QuyPhep
{
    public interface IQuyPhepDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.CaLamViec> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.CaLamViec data)> GetByIdAsync(int id);

        /// <summary>
        /// Chức năng: tạo CaLamViec
        /// </summary>
        /// <param name="CaLamViec"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.CaLamViec data)> CreateAsync(Models.CaLamViec caLamViec);

        /// <summary>
        /// Chức năng: lấy ra CaLamViec dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.CaLamViec> data, int totalRecords)> PaginationAsync(PaginationAirportRequest request);

        /// <summary>
        /// Chức năng: kiểm tra CaLamViec đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.CaLamViec> data)> GetByCodeOrNameAsync(SearchAirportRequest request);

        /// <summary>
        /// Chức năng: cập nhật CaLamViec
        /// </summary>
        /// <param name="CaLamViec"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.CaLamViec data)> UpdateAsync(Models.CaLamViec caLamViec);
    }
}
