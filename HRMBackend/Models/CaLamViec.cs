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
        [Column("maCa")]
        public int MaCa { get; set; }
        [Column("tenCa")]
        public string TenCa { get; set; }
        [Column("gioBatDauCa")]
        public TimeOnly GioBatDauCa { get; set; }
        [Column("gioKetThucCa")]
        public TimeOnly GioKetThucCa { get; set; }
        [Column("gioBatDauNghi")]
        public TimeOnly GioBatDauNghi { get; set; }
        [Column("gioKetThucNghi")]
        public TimeOnly GioKetThucNghi { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
    }
}
