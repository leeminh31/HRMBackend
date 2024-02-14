using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.HopDong.Request
{
    public class CreateHopDongRequest
    {
        public string TenHopDong { get; set; }
        public string MaNhanVien { get; set; }
        public DateOnly NgayBatDauHopDong { get; set; }
        public DateOnly NgayKetThucHopDong { get; set; }
        public string loaiHopDong { get; set; }
        public double TiLeHuongLuong { get; set; }
        public double GioLamViec { get; set; }
        public double CongChuan { get; set; }
    }
}
