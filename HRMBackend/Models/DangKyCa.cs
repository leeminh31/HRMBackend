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
        [Column("maDangKyCa")]
        public int MaDangKyCa { get; set; }
        [Column("ngayTao")]
        public DateOnly NgayTao { get; set; }
        [Column("caLamViecHienTai")]
        public string CaLamViecHienTai { get; set; }
        [Column("caLamViecMoi")]
        public string CaLamViecMoi { get; set; }
        [Column("ngayBatDauCaMoi")]
        public DateOnly NgayBatDauCaMoi { get; set; }
        [Column("nguoiDuyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangThai")]
        public bool TrangThai { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
    }
}
