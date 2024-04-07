namespace HRMBackend.Resources.DTO.DangKyCa.Request
{
    public class SearchDangKyCaRequest
    {
        public string? TenNhanVien { get; set; }
        public string? MaNhanVien { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? CaLamViecMoi { get; set; }
        public string? TrangThai { get; set; }
    }
}
