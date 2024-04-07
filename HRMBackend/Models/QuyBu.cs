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
        [Column("maquybu")]
        public int MaQuyBu { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
    }
}
