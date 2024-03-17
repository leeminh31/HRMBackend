#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_DonBu")]
    public partial class DonBu
    {
        [Key]
        [Column("madonbu")]
        public int MaDonBu { get; set; }
        [Column("ngaytaodon")]
        public DateOnly NgayTaoDon { get; set; }
        [Column("ngaylamviec")]
        public DateOnly NgayLamViec { get; set; }
        [Column("sophutxinbu")]
        public int SoPhutXinBu { get; set; }
        [Column("lydo")]
        public string LyDo { get; set; }
        [Column("nguoiduyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangthai")]
        public bool TrangThai { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
    }
}
