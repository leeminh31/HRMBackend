#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_QuyPhep")]
    public partial class QuyPhep
    {
        [Key]
        [Column("maQuyPhep")]
        public int MaQuyPhep { get; set; }
        [Key]
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
    }
}
