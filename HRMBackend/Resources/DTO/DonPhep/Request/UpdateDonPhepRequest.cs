using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.DonPhep.Request
{
    public class UpdateDonPhepRequest
    {
        public int MaDonPhep { get; set; }
        public DateTime NgayTaoDon { get; set; }
        public DateTime NgayLamViec { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
