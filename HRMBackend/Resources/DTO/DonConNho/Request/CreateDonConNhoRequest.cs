using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.DonConNho.Request
{
    public class CreateDonConNhoRequest
    {
        public int MaDonConNho { get; set; }
        public DateOnly NgayTaoDon { get; set; }
        public DateOnly TuNgay { get; set; }
        public DateOnly DenNgay { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public bool TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
