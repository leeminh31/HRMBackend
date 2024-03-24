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
        [Column("magiaitrinh")]
        public int MaGiaiTrinh { get; set; }
        [Column("ngaytaogiaitrinh")]
        public DateTime NgayTaoGiaiTrinh { get; set; }
        [Column("ngaylamviec")]
        public DateTime NgayLamViec { get; set; }
        [Column("loaigiaitrinh")]
        public string LoaiGiaiTrinh { get; set; }
        [Column("lydo")]
        public string LyDo { get; set; }
        [Column("nguoiduyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangthai")]
        public string TrangThai { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
    }
}
