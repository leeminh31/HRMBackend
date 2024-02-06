using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.PhongBan.Response
{
    public class PhongBanResponse
    {
        public int MaPhongBan { get; set; }
        public string TenPhongBan { get; set; }
        public int SoLanChamCong { get; set; }
        public string TruongPhongBan { get; set; }
        public string ThuKyPhongBan { get; set; }

    }
}
