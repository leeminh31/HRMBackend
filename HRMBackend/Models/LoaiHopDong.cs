#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_LoaiHopDong")]
    public partial class LoaiHopDong
    {
        [Key]
        [Column("maLoaiHopDong")]
        public int MaLoaiHopDong { get; set; }
        [Column("tenLoai")]
        public string TenLoai { get; set; }
    }
}
