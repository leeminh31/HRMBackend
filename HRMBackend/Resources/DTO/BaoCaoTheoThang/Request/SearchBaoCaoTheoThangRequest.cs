namespace HRMBackend.Resources.DTO.BaoCaoTheoThang.Request
{
    public class SearchBaoCaoTheoThangRequest
    {
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? MaNhanVien { get; set; }
        public string? TenNhanVien { get; set; }
    }
}
