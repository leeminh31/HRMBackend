using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.DuLieuChamCong.Request
{
    public class CreateDuLieuChamCongRequest
    {
        public string MaNhanVien { get; set; }
        public DateTime NgayChamCong { get; set; }
        public int LanChamCong { get; set; }
        public TimeSpan GioChamCong { get; set; }
    }
}
