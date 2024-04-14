namespace HRMBackend.Resources.DTO.DuLieuChamCong.Request
{
    public class SearchDuLieuChamCongByMonthRequest
    {
        public string? MaNhanVien { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string? TenNhanVien { get; set; }
        public int? IDVanTay { get; set; }
    }
}
