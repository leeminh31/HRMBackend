using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.HopDong.Response
{
    public class HopDongResponse
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
