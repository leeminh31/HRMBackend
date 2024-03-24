using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonBu.Response;

namespace HRMBackend.DataAccess.DonBu
{
    public interface IDonBuDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.DonBu> data)> GetByParamsAsync(SearchDanhSachDonRequest request);

        Task<(bool isSuccess, Models.DonBu data)> ApproveRequestAsync(string maDonBu);

        Task<(bool isSuccess, Models.DonBu data)> RejectAllRequestAsync(ApproveRequestList request);

        Task<(bool isSuccess, Models.DonBu data)> ApproveAllRequestAsync(ApproveRequestList request);

        Task<(bool isSuccess, Models.DonBu data)> CreateAsync(Models.DonBu airport);
    }
}
