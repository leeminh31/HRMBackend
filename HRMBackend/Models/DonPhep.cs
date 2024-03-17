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
        [Column("madonphep")]
        public int MaDonPhep { get; set; }
        [Column("ngaytaodon")]
        public DateOnly NgayTaoDon { get; set; }
        [Column("ngaylamviec")]
        public DateOnly NgayLamViec { get; set; }
        [Column("lydo")]
        public string LyDo { get; set; }
        [Column("nguoiduyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangthai")]
        public bool TrangThai { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set;}
    }
}
