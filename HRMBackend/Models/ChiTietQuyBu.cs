#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_ChiTietQuyBu")]
    public partial class ChiTietQuyBu
    {
        [Key]
        [Column("maChiTietQuyBu")]
        public int MaChiTietQuyBu { get; set; }
        [Column("maQuyBu")]
        public int MaQuyBu { get; set; }
        [Column("thang")]
        public int Thang { get; set; }
        [Column("phatSinh")]
        public int PhatSinh { get; set; }
        [Column("nam")]
        public int Nam { get; set; }
        [Column("suDung")]
        public int SuDung { get; set; }
    }
}
