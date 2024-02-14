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
        [Column("maChiTietQuyPhep")]
        public int MaChiTietQuyPhep { get; set; }
        [Column("maQuyPhep")]
        public int MaQuyPhep { get; set; }
        [Column("nam")]
        public int nam { get; set; }
        [Column("thang")]
        public int Thang { get; set; }
        [Column("suDung")]
        public int SuDung { get; set; }
    }
}
