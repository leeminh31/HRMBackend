#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_ChiTietQuyPhep")]
    public partial class ChiTietQuyPhep
    {
        [Key]
        [Column("machitietquyphep")]
        public int MaChiTietQuyPhep { get; set; }
        [Column("maquyphep")]
        public int MaQuyPhep { get; set; }
        [Column("nam")]
        public int Nam { get; set; }
        [Column("thang")]
        public int Thang { get; set; }
        [Column("sudung")]
        public int SuDung { get; set; }
    }
}
