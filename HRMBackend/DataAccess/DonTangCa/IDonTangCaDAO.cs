using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonTangCa.Response;

namespace HRMBackend.DataAccess.DonTangCa
{
    public interface IDonTangCaDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.DonTangCa> data)> GetByParamsAsync(SearchDanhSachDonRequest request);
        Task<(bool isSuccess, Models.DonBu data)> ApproveRequestAsync(string maDonTangCa);
        Task<(bool isSuccess, Models.DonTangCa data)> CreateAsync(Models.DonTangCa airport);
        Task<(bool isSuccess, Models.DonTangCa data)> UpdateAsync(Models.DonTangCa airport);
        Task<(bool isSuccess, int? data)> GetTotalMinutesOTAsync(string maNhanVien, int nam);
    }
}
