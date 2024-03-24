using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.DonTangCa.Request
{
    public class CreateDonTangCaRequest
    {
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
