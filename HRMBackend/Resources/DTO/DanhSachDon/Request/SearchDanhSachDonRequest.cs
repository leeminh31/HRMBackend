namespace HRMBackend.Resources.DTO.DanhSachDon.Request
{
    public class SearchDanhSachDonRequest
    {
        public string? TenNhanVien { get; set; }
        public int? LoaiDon {  get; set; }
        public int? TrangThai { get; set; }
        public DateTime? NgayLamViec { get; set; }
        public DateTime? NgayTao {  get; set; }
    }
}
