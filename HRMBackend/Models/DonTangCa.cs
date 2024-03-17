#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_DonTangCa")]
    public partial class DonTangCa
    {
        [Key]
        [Column("madontangca")]
        public int MaDonTangCa { get; set; }
        [Column("ngaytaodon")]
        public DateTime NgayTaoDon { get; set; }
        [Column("ngaylamviec")]
        public DateTime NgayLamViec { get; set; }
        [Column("tangcatu")]
        public TimeSpan TangCaTu { get; set; }
        [Column("tangcaden")]
        public TimeSpan TangCaDen{ get; set; }
        [Column("lydo")]
        public string LyDo { get; set; }
        [Column("nguoiduyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangthai")]
        public string TrangThai { get; set; }
        [Column("manhanvien")]
        public string MaNhanVien {  get; set; }
    }
}
