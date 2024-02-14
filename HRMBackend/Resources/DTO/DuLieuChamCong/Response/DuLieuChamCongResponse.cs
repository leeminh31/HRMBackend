using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.DuLieuChamCong.Response
{
    public class DuLieuChamCongResponse
    {
        public int MaChamCong { get; set; }
        public int MaVanTay { get; set; }
        public string MaNhanVien { get; set; }
        public DateOnly NgayChamCong { get; set; }
        public int LanChamCong { get; set; }
        public TimeOnly GioChamCong { get; set; }

    }
}
