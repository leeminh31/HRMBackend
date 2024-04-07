using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.DangKyCa.Request
{
    public class CreateDangKyCaRequest
    {
        public DateTime NgayTao { get; set; }
        public string CaLamViecHienTai { get; set; }
        public string CaLamViecMoi { get; set; }
        public DateTime NgayBatDauCaMoi { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
