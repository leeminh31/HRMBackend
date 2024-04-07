using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.DonBu.Request
{
    public class UpdateDonBuRequest
    {
        public int MaDonBu { get; set; }
        public DateTime NgayTaoDon { get; set; }
        public DateTime NgayLamViec { get; set; }
        public int SoPhutXinBu { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
