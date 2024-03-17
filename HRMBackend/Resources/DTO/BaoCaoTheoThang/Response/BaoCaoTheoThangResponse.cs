using HRMBackend.Resources.DTO.DuLieuChamCong.Response;

namespace HRMBackend.Resources.DTO.BaoCaoTheoThang.Response
{
    public class BaoCaoTheoThangResponse : DuLieuChamCongResponse
    {
        public double? TongCong { get; set; }
        public double? ThoiGianLamViecThucTe { get; set; }
    }
}
