using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.PhongBan.Request
{
    public class UpdatePhongBanRequest
    {
        public int MaPhongBan { get; set; }
        public string TenPhongBan { get; set; }
        public int SoLanChamCong { get; set; }
        public string? TruongPhongBan { get; set; }
        public string? ThuKyPhongBan { get; set; }
    }
}
