using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.DonTangCa.Request
{
    public class UpdateDonTangCaRequest
    {
        public int MaDonTangCa { get; set; }
        public DateTime NgayTaoDon { get; set; }
        public DateTime NgayLamViec { get; set; }
        public TimeSpan TangCaTu { get; set; }
        public TimeSpan TangCaDen { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
