namespace HRMBackend.DataAccess.HopDong
{
    public interface IHopDongDAO
    {
        /// <summary>
        /// Chức năng: Lấy danh sách các cảng hàng không đang hoạt động theo từ khoá tìm kiếm
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        Task<(bool hasValue, IEnumerable<Models.HopDong> data)> GetFilterAsync(string searchKey);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool hasValue, Models.HopDong data)> GetByIDAsync(string maHopDong);

        /// <summary>
        /// Chức năng: Lấy thông tin cảng hàng không đang hoạt động theo Id
        /// </summary>
        /// <returns></returns>
        Task<(bool isSuccess, IEnumerable<Models.HopDong> data)> GetByParamsAsync(string? tenHopDong, string? loaiHopDong);

        /// <summary>
        /// Chức năng: tạo HopDong
        /// </summary>
        /// <param name="HopDong"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.HopDong data)> CreateAsync(Models.HopDong caLamViec);

        /// <summary>
        /// Chức năng: lấy ra HopDong dựa vào code và name theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<(bool isSuccess, IEnumerable<Models.HopDong> data, int totalRecords)> PaginationAsync(PaginationHopDongRequest request);

        /// <summary>
        /// Chức năng: kiểm tra HopDong đã tồn tại chưa dựa vào code hoặc name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //Task<(bool isSuccess, IEnumerable<Models.HopDong> data)> GetByCodeOrNameAsync(SearchHopDongRequest request);

        /// <summary>
        /// Chức năng: cập nhật HopDong
        /// </summary>
        /// <param name="HopDong"></param>
        /// <returns></returns>
        Task<(bool isSuccess, Models.HopDong data)> UpdateAsync(Models.HopDong caLamViec);

        Task<(bool hasValue, IEnumerable<Models.HopDong> data)> GetAllContractAsync();
    }
}
