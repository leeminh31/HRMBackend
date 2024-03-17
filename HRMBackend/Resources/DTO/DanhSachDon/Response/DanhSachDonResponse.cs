using HRMBackend.Resources.DTO.DonBu.Response;
using HRMBackend.Resources.DTO.DonConNho.Response;
using HRMBackend.Resources.DTO.DonPhep.Response;
using HRMBackend.Resources.DTO.DonTangCa.Response;

namespace HRMBackend.Resources.DTO.DanhSachDon.Response
{
    public class DanhSachDonResponse
    {
        public List<DonBuResponse> listDonBu { get; set; }
        public List<DonConNhoResponse> listDonConNho { get; set; }
        public List<DonPhepResponse> listDonPhep { get; set; }
        public List<DonTangCaResponse> listDonTangCa { get; set; }
    }
}
