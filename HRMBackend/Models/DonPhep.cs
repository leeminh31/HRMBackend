#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_DonPhep")]
    public partial class DonPhep
    {
        [Key]
        [Column("maDonPhep")]
        public int MaDonPhep { get; set; }
        [Column("ngayTaoDon")]
        public DateOnly NgayTaoDon { get; set; }
        [Column("ngayLamViec")]
        public DateOnly NgayLamViec { get; set; }
        [Column("lyDo")]
        public string LyDo { get; set; }
        [Column("nguoiDuyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangThai")]
        public bool TrangThai { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien { get; set;}
    }
}
