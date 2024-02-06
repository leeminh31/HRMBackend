using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.DonConNho.Request
{
    public class UpdateDonConNhoRequest
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
