using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.DangKyCa.Request
{
    public class UpdateDangKyCaRequest
    {
        public int MaDangKyCa { get; set; }
        public DateOnly NgayTao { get; set; }
        public string CaLamViecHienTai { get; set; }
        public string CaLamViecMoi { get; set; }
        public DateOnly NgayBatDauCaMoi { get; set; }
        public string NguoiDuyet { get; set; }
        public bool TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
