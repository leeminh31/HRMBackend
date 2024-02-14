#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_GiaiTrinh")]
    public partial class GiaiTrinh
    {
        [Key]
        [Column("maGiaiTrinh")]
        public int MaGiaiTrinh { get; set; }
        [Column("ngayTaoGiaiTrinh")]
        public DateOnly NgayTaoGiaiTrinh { get; set; }
        [Column("ngayLamViec")]
        public DateOnly NgayLamViec { get; set; }
        [Column("loaiGiaiTrinh")]
        public string LoaiGiaiTrinh { get; set; }
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
