#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_PhongBan")]
    public partial class PhongBan
    {
        [Key]
        [Column("maPhongBan")]
        public int MaPhongBan { get; set; }
        [Column("tenPhongBan")]
        public string TenPhongBan { get; set; }
        [Column("soLanChamCong")]
        public int SoLanChamCong { get; set; }
        [Column("truongPhongBan")]
        public string TruongPhongBan { get; set; }
        [Column("thuKyPhongBan")]
        public string ThuKyPhongBan { get; set; }
    }
}
