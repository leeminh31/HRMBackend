using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.DonConNho.Request
{
    public class CreateDonConNhoRequest
    {
        public DateTime NgayTaoDon { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime NgayLamViec { get; set; }
    }
}
