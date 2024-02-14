using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.DonTangCa.Request
{
    public class CreateDonTangCaRequest
    {
        public int MaDonTangCa { get; set; }
        public DateOnly NgayTaoDon { get; set; }
        public DateOnly NgayLamViec { get; set; }
        public TimeOnly TangCaTu { get; set; }
        public TimeOnly TangCaDen { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public bool TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
