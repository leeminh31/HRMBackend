using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonTangCa.Response;

namespace HRMBackend.DataAccess.DonTangCa
{
    public interface IDonTangCaDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.DonTangCa> data)> GetByParamsAsync(SearchDanhSachDonRequest request);
    }
}
