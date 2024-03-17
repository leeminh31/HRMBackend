using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DanhSachDon.Response;

namespace HRMBackend.DataAccess.DanhSachDon
{
    public interface IDanhSachDonDAO
    {
        Task<(bool isSuccess, DanhSachDonResponse data)> GetByParamsAsync(SearchDanhSachDonRequest request);
        Task<bool> DeleteAsync(string id);
    }
}
