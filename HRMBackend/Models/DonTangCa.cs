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
        [Column("maDonTangCa")]
        public int MaDonTangCa { get; set; }
        [Column("ngayTaoDon")]
        public DateOnly NgayTaoDon { get; set; }
        [Column("ngaylamViec")]
        public DateOnly NgayLamViec { get; set; }
        [Column("tangCaTu")]
        public TimeOnly TangCaTu { get; set; }
        [Column("tangCaDen")]
        public TimeOnly TangCaDen{ get; set; }
        [Column("lyDo")]
        public string LyDo { get; set; }
        [Column("nguoiDuyet")]
        public string NguoiDuyet { get; set; }
        [Column("trangThai")]
        public bool TrangThai { get; set; }
        [Column("maNhanVien")]
        public string MaNhanVien {  get; set; }
    }
}
