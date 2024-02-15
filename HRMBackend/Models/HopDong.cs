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
        [Column("tenhopdong")]
        public string TenHopDong { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
        [Column("ngaybatdauhopdong")]
        public DateOnly NgayBatDauHopDong { get; set; }
        [Column("ngayketthuchopdong")]
        public DateOnly NgayKetThucHopDong { get; set; }
        [Column("loaihopdong")]
        public string LoaiHopDong { get; set; }
        [Column("tilehuongluong")]
        public double TyLeHuongLuong { get; set; }
        [Column("giolamviec")]
        public double GioLamViec { get; set; }
        [Column("congchuan")]
        public double CongChuan { get; set; }
    }
}
