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
        [Column("machitietquybu")]
        public int MaChiTietQuyBu { get; set; }
        [Column("maquybu")]
        public int MaQuyBu { get; set; }
        [Column("thang")]
        public int Thang { get; set; }
        [Column("phatsinh")]
        public int PhatSinh { get; set; }
        [Column("nam")]
        public int Nam { get; set; }
        [Column("sudung")]
        public int SuDung { get; set; }
    }
}
