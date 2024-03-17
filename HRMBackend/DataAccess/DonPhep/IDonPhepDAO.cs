using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonPhep.Response;

namespace HRMBackend.DataAccess.DonPhep
{
    public interface IDonPhepDAO
    {
        Task<(bool isSuccess, IEnumerable<DonPhepResponse> data)> GetByParamsAsync(SearchDanhSachDonRequest request);
    }
}
