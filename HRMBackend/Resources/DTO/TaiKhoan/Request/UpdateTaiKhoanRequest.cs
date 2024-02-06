using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.TaiKhoan.Request
{
    public class UpdateTaiKhoanRequest
    {
        public int MaTaiKhoan { get; set; }
        public string MaNhanVien { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string PhanQuyen { get; set; }
    }
}
