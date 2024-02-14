using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.NhanVien.Request
{
    public class UpdateNhanVienRequest
    {
        public string? MaNhanVien { get; set; }
        public string? HoTen { get; set; }
        public string? ChucVu { get; set; }
        public string? Mail { get; set; }
        public DateOnly? NgaySinh { get; set; }
        public string? SoCCCD { get; set; }
        public DateOnly? NgayCap { get; set; }
        public string? QueQuan { get; set; }
        public string? NoiOHienTai { get; set; }
        public string? NguoiThanLienHe { get; set; }
        public string? SoDienThoaiNguoiLienHe { get; set; }
        public string? STKNganHang { get; set; }
        public string? NganHang { get; set; }
        public int? MaPhongBan { get; set; }
        public string? SoDienThoai { get; set; }
        public int? IDVanTay { get; set; }
    }
}
