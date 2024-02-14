using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.NhanVien.Request
{
    public class SearchNhanVienRequest
    {
        public string? MaNhanVien { get; set; }
        public string? HoTen { get; set; }
        public string? ChucVu { get; set; }
        public int? MaPhongBan { get; set; }
        public int? IDVanTay { get; set; }
    }
}
