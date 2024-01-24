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
        [Column("ngayVaoLam")]
        public DateOnly NgayVaoLam { get; set; }
        [Column("thang")]
        public int Thang { get; set; }
        [Column("phatSinh")]
        public int PhatSinh { get; set; }
        [Column("tong")]
        public int Tong { get; set; }
        [Column("suDung")]
        public int SuDung { get; set; }
        [Column("conLai")]
        public int ConLai { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
    }
}
