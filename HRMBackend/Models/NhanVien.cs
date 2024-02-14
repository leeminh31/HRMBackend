#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_NhanVien")]
    public partial class NhanVien
    {
        [Key]
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
        [Column("hoten")]
        public string HoTen { get; set; }
        [Column("chucvu")]
        public string ChucVu { get; set; }
        [Column("mail")]
        public string Mail { get;set; }
        [Column("ngaysinh")]
        public DateTime NgaySinh { get;set; }
        [Column("socccd")]
        public string SoCCCD { get; set; }
        [Column("ngaycap")]
        public DateTime NgayCap { get; set; }
        [Column("quequan")]
        public string QueQuan { get; set; }
        [Column("noiohientai")]
        public string NoiOHienTai { get; set; }
        [Column("nguoithanlienhe")]
        public string NguoiThanLienHe {  get; set; }
        [Column("sodienthoainguoilienhe")]
        public string SoDienThoaiNguoiLienHe { get; set; }
        [Column("stknganhang")]
        public string STKNganHang { get; set; }
        [Column("nganhang")]
        public string NganHang { get; set; }
        [Column("maphongban")]
        public int MaPhongBan { get; set; }
        [Column("sodienthoai")]
        public string SoDienThoai { get; set; }
        [Column("idvantay")]
        public int IDVanTay { get; set; }
    }
}
