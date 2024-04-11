using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonConNho.Response;

namespace HRMBackend.DataAccess.DonConNho
{
    public interface IDonConNhoDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.DonConNho> data)> GetByParamsAsync(SearchDanhSachDonRequest request);
        Task<(bool isSuccess, Models.DonConNho data)> ApproveRequestAsync(string maDonConNho);
        Task<(bool isSuccess, Models.DonConNho data)> CreateAsync(Models.DonConNho airport);
        Task<(bool isSuccess, Models.DonConNho data)> UpdateAsync(Models.DonConNho airport);
        Task<(bool isSuccess, IEnumerable<Models.DonConNho> data)> GetDonConNhoByDayAsync(SearchDonByDayRequest request);
        Task<(bool isSuccess, IEnumerable<Models.DonConNho> data)> GetDonConNhoByMonthAsync(SearchDanhSachDonRequest request);
    }
}
