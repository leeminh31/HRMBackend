using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DanhSachDon.Response;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Resources.DTO.DonBu.Response;
using HRMBackend.Resources.DTO.DonConNho.Request;
using HRMBackend.Resources.DTO.DonConNho.Response;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Resources.DTO.DonPhep.Response;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using HRMBackend.Resources.DTO.DonTangCa.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.DanhSachDon
{
    public interface IDanhSachDonService
    {
        Task<BaseResult<DanhSachDonResponse>> GetByParamsAsync(SearchDanhSachDonRequest request);
        Task<BaseResult<DanhSachDonResponse>> GetByEmployeeIdAsync(string? maNhanVien);
        Task<BaseResult<bool>> RejectAllRequestAsync(ApproveRequestList request);
        Task<BaseResult<bool>> ApproveAllRequestAsync(ApproveRequestList request);
        Task<BaseResult<DonBuResponse>> CreateDonBuAsync(CreateDonBuRequest request);
        Task<BaseResult<DonConNhoResponse>> CreateDonConNhoAsync(CreateDonConNhoRequest request);
        Task<BaseResult<DonPhepResponse>> CreateDonPhepAsync(CreateDonPhepRequest request);
        Task<BaseResult<DonTangCaResponse>> CreateDonTangCaAsync(CreateDonTangCaRequest request);
        Task<BaseResult<DonTangCaResponse>> UpdateDonTangCaAsync(UpdateDonTangCaRequest request);
        Task<BaseResult<DonPhepResponse>> UpdateDonPhepAsync(UpdateDonPhepRequest request);
        Task<BaseResult<DonConNhoResponse>> UpdateDonConNhoAsync(UpdateDonConNhoRequest request);
        Task<BaseResult<DonBuResponse>> UpdateDonBuAsync(UpdateDonBuRequest request);
        Task<BaseResult<int?>> GetTotalMinutesOTAsync(string maNhanVien, int nam);
        Task<BaseResult<double>> GetTotalDayOffByYearAsync(string maNhanVien, int nam);
    }
}
