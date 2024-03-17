using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DanhSachDon.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.DanhSachDon
{
    public interface IDanhSachDonService
    {
        Task<BaseResult<DanhSachDonResponse>> GetByParamsAsync(SearchDanhSachDonRequest request);
    }
}
