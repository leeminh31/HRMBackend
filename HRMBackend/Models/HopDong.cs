#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_HopDong")]
    public partial class HopDong
    {
        [Key]
        [Column("tenHopDong")]
        public string TenHopDong { get; set; }
        [Column("ngayBatDauHopDong")]
        public DateOnly NgayBatDauHopDong { get; set; }
        [Column("ngayKetThucHopDong")]
        public DateOnly NgayKetThucHopDong { get; set; }
        [Column("maLoaiHopDong")]
        public int MaLoaiHopDong { get; set; }
        [Column("tiLeHuongLuong")]
        public double TiLeHuongLuong { get; set; }
        [Column("gioLamViec")]
        public double GioLamViec { get; set; }
        [Column("congChuan")]
        public double CongChuan { get; set; }
    }
}
