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
        [Column("maNhanVien")]
        public string MaNhanVien { get; set; }
        [Column("hoTen")]
        public string HoTen { get; set; }
        [Column("chucVu")]
        public string ChucVu { get; set; }
        [Column("mail")]
        public string Mail { get;set; }
        [Column("ngaySinh")]
        public DateOnly NgaySinh { get;set; }
        [Column("soCCCD")]
        public string SoCCCD { get; set; }
        [Column("ngayCap")]
        public DateOnly NgayCap { get; set; }
        [Column("queQuan")]
        public string QueQuan { get; set; }
        [Column("noiOHienTai")]
        public string NoiOHienTai { get; set; }
        [Column("nguoiThanLienHe")]
        public string NguoiThanLienHe {  get; set; }
        [Column("soDienThoaiNguoiLienHe")]
        public string SoDienThoaiNguoiLienHe { get; set; }
        [Column("STKNganHang")]
        public string STKNganHang { get; set; }
        [Column("nganHang")]
        public string NganHang { get; set; }
        [Column("maPhongBan")]
        public int MaPhongBan { get; set; }
        [Column("soDienThoai")]
        public string SoDienThoai { get; set; }
        [Column("IDVanTay")]
        public int IDVanTay { get; set; }
    }
}
