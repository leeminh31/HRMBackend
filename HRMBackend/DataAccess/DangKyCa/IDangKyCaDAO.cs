using HRMBackend.Resources.DTO.DangKyCa.Request;

namespace HRMBackend.DataAccess.DangKyCa
{
    public interface IDangKyCaDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.DangKyCa> data)> GetByParamsAsync(SearchDangKyCaRequest request);
        Task<(bool isSuccess, Models.DangKyCa data)> CreateAsync(Models.DangKyCa airport);
        Task<(bool isSuccess, Models.DangKyCa data)> ApproveShiftRequestAsync(string maDangKyCa, string nguoiDuyet);
        Task<(bool isSuccess, Models.DangKyCa data)> RejectShiftRequestAsync(string maDangKyCa, string nguoiDuyet);
        Task<(bool isSuccess, IEnumerable<Models.DangKyCa> data)> GetLatestShiftToUpdateAsync();
        Task<(bool isSuccess, IEnumerable<Models.DangKyCa> data)> GetByEmployeeIDAsync(string? maNhanVien, DateTime? ngayBatDau, DateTime? ngayKetThuc);
    }
}
