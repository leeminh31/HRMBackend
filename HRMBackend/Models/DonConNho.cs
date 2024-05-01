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
        [Column("madonconnho")]
        public int MaDonConNho { get; set; }
        [Column("ngaytaodon")]
        public DateTime NgayTaoDon { get; set; }
        [Column("tungay")]
        public DateTime TuNgay { get; set; }
        [Column("denngay")]
        public DateTime DenNgay { get; set; }
        [Column("lydo")]
        public string LyDo { get; set; }
        [Column("nguoiduyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangthai")]
        public string TrangThai { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
        [Column("thoigiancapnhat")]
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
