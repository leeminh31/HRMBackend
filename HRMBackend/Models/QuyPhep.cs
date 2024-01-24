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
        [Column("ngayBatDauTinhPhep")]
        public DateOnly NgayBatDauTinhPhep { get; set; }
        [Column("tongQuyTrongNam")]
        public int TongQuyTrongNam { get; set; }
        [Column("thang")]
        public int Thang { get; set; }
        [Column("suDung")]
        public int SuDung { get; set; }
        [Column("conLai")]
        public int ConLai { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
    }
}
