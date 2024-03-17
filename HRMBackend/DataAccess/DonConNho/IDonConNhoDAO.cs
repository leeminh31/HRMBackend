using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonConNho.Response;

namespace HRMBackend.DataAccess.DonConNho
{
    public interface IDonConNhoDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.DonConNho> data)> GetByParamsAsync(SearchDanhSachDonRequest request);
    }
}
