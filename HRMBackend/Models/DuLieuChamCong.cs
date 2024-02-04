#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_DuLieuChamCong")]
    public partial class DuLieuChamCong
    {
        [Key]
        [Column("maChamCong")]
        public int MaChamCong { get; set; }
        [Column("IDVanTay")]
        public int MaVanTay { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
        [Column("ngayChamCong")]
        public DateOnly NgayChamCong { get; set; }
        [Column("lanChamCong")]
        public int LanChamCong { get; set; }
        [Column("gioChamCong")]
        public TimeOnly GioChamCong { get; set; }
    }
}
