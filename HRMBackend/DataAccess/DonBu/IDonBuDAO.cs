using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonBu.Response;

namespace HRMBackend.DataAccess.DonBu
{
    public interface IDonBuDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.DonBu> data)> GetByParamsAsync(SearchDanhSachDonRequest request);
    }
}
