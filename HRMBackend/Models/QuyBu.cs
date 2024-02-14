#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_QuyBu")]
    public partial class QuyBu
    {
        [Key]
        [Column("maQuyBu")]
        public int MaQuyBu { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
    }
}
