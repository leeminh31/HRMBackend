using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.DuLieuChamCong.Request
{
    public class UpdateDuLieuChamCongRequest
    {
        public int MaChamCong { get; set; }
        public string MaNhanVien { get; set; }
        public DateOnly NgayChamCong { get; set; }
        public int LanChamCong { get; set; }
        public TimeOnly GioChamCong { get; set; }
    }
}
