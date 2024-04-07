#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_DangKyCa")]
    public partial class DangKyCa
    {
        [Key]
        [Column("madangkyca")]
        public int MaDangKyCa { get; set; }
        [Column("ngaytao")]
        public DateTime NgayTao { get; set; }
        [Column("calamviechientai")]
        public string CaLamViecHienTai { get; set; }
        [Column("calamviecmoi")]
        public string CaLamViecMoi { get; set; }
        [Column("ngaybatdaucamoi")]
        public DateTime NgayBatDauCaMoi { get; set; }
        [Column("nguoiduyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangthai")]
        public string TrangThai { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
    }
}
