#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [Column("mataikhoan")]
        public int MaTaiKhoan { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
        [Column("tendangnhap")]
        public string TenDangNhap { get; set; }
        [Column("matkhau")]
        public string MatKhau { get; set; }
        [Column("phanquyen")]
        public string PhanQuyen { get; set; }
    }
}
