namespace HRMBackend.Resources.DTO.DuLieuChamCong.Response
{
    public class DuLieuChamCongByDayResponse
    {
        public double GioLamViec { get; set; }
        public int NgayLamViec { get; set; }
        public double GioLamViecTheoCa { get; set; }
        public bool NghiPhep { get; set; }
        public string TenCa { get; set; }
        public bool ConNho { get; set; }
        public bool IsYellow { get; set; }
    }
}
