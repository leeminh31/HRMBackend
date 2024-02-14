using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.DonBu.Request
{
    public class CreateDonBuRequest
    {
        public int MaDonBu { get; set; }
        public DateOnly NgayTaoDon { get; set; }
        public DateOnly NgayLamViec { get; set; }
        public int SoPhutXinBu { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public bool TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
