using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.TaiKhoan.Request
{
    public class CreateTaiKhoanRequest
    {
        public int MaTaiKhoan { get; set; }
        public string MaNhanVien { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string PhanQuyen { get; set; }
    }
}
