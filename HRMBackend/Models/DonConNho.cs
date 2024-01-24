#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_DonConNho")]
    public partial class DonConNho
    {
        [Key]
        [Column("maDonConNho")]
        public int MaDonConNho { get; set; }
        [Column("ngayTaoDon")]
        public DateOnly NgayTaoDon { get; set; }
        [Column("tuNgay")]
        public DateOnly TuNgay { get; set; }
        [Column("denNgay")]
        public DateOnly DenNgay { get; set; }
        [Column("lyDo")]
        public string LyDo { get; set; }
        [Column("nguoiDuyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangThai")]
        public bool TrangThai { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
    }
}
