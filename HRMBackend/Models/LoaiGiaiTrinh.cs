#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_LoaiGiaiTrinh")]
    public partial class LoaiGiaiTrinh
    {
        [Key]
        [Column("maLoaiGiaiTrinh")]
        public int MaLoaiGiaiTrinh { get; set; }
        [Column("tenLoai")]
        public string TenLoai { get; set; }
    }
}
