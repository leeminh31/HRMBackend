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
        [Column("maTaiKhoan")]
        public int MaTaiKhoan { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
        [Column("tenDangNhap")]
        public string TenDangNhap { get; set; }
        [Column("matKhau")]
        public string MatKhau { get; set; }
        [Column("maPhanQuyen")]
        public int MaPhanQuyen { get; set; }
    }
}
