#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_CaLamViec")]
    public partial class CaLamViec
    {
        [Key]
        [Column("maca")]
        public int MaCa { get; set; }
        [Column("tenca")]
        public string TenCa { get; set; }
        [Column("giobatdauca")]
        public TimeOnly GioBatDauCa { get; set; }
        [Column("gioketthucca")]
        public TimeOnly GioKetThucCa { get; set; }
        [Column("giobatdaunghi")]
        public TimeOnly GioBatDauNghi { get; set; }
        [Column("gioketthucnghi")]
        public TimeOnly GioKetThucNghi { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
    }
}
