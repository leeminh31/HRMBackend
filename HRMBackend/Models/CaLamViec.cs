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
        [Column("macalamviec")]
        public int MaCa { get; set; }
        [Column("tenca")]
        public string TenCa { get; set; }
        [Column("giobatdauca")]
        public TimeSpan GioBatDauCa { get; set; }
        [Column("gioketthucca")]
        public TimeSpan GioKetThucCa { get; set; }
        [Column("giobatdaunghi")]
        public TimeSpan GioBatDauNghi { get; set; }
        [Column("gioketthucnghi")]
        public TimeSpan GioKetThucNghi { get; set; }
    }
}
