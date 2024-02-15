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
        [Column("maphongban")]
        public int MaPhongBan { get; set; }
        [Column("tenphongban")]
        public string TenPhongBan { get; set; }
        [Column("solanchamcong")]
        public int SoLanChamCong { get; set; }
        [Column("truongphongban")]
        public string TruongPhongBan { get; set; }
        [Column("thukyphongban")]
        public string ThuKyPhongBan { get; set; }
    }
}
