using HRMBackend.Resources.DTO.DuLieuChamCong.Response;

namespace HRMBackend.Resources.DTO.BaoCaoTheoThang.Response
{
    public class BaoCaoTheoThangAllResponse
    {
        public string MaNhanVien { get; set; }
        public double TongCong { get; set; }
        public string HoTen { get; set; }
        public string Phong { get; set; }
        public int IdVanTay {  get; set; }
        public List<DuLieuChamCongByDayResponse> duLieuChamCongResponses { get; set; }
    }
}
