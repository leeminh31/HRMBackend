namespace HRMBackend.Resources.DTO.BaoCaoTheoThang.Request
{
    public class SearchBaoCaoTheoThangByDay
    {
        public DateTime? NgayLamViec { get; set; }
        public string? MaNhanVien { get; set; }
        public string? TenCa {  get; set; }
    }
}
