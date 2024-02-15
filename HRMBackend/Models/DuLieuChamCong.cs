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
        [Column("machamcong")]
        public int MaChamCong { get; set; }
        [Column("idvantay")]
        public int MaVanTay { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
        [Column("ngaychamcong")]
        public DateOnly NgayChamCong { get; set; }
        [Column("lanchamcong")]
        public int LanChamCong { get; set; }
        [Column("giochamcong")]
        public TimeOnly GioChamCong { get; set; }
    }
}
