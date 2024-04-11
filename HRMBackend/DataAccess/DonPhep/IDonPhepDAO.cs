using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Resources.DTO.DonPhep.Response;

namespace HRMBackend.DataAccess.DonPhep
{
    public interface IDonPhepDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.DonPhep> data)> GetByParamsAsync(SearchDanhSachDonRequest request);
        Task<(bool isSuccess, Models.DonPhep data)> ApproveRequestAsync(string maDonPhep);
        Task<(bool isSuccess, Models.DonPhep data)> CreateAsync(Models.DonPhep airport);
        Task<(bool isSuccess, Models.DonPhep data)> UpdateAsync(Models.DonPhep airport);
        Task<(bool isSuccess, IEnumerable<Models.DonPhep> data)> GetDayOffAsync(SearchDonPhepRequest request);
        Task<(bool isSuccess, int data)> GetDayOffByYearAndIDAsync(string maNhanVien, int nam);
        Task<(bool isSuccess, IEnumerable<Models.DonPhep> data)> GetDonPhepByDayAsync(SearchDonByDayRequest request);
    }
}
