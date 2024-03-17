namespace HRMBackend.Resources.DTO.DanhSachDon.Request
{
    public class SearchDanhSachDonRequest
    {
        public string? TenNhanVien { get; set; }
        public int? LoaiDon {  get; set; }
        public int? TrangThai { get; set; }
        public DateTime? NgayLamViecBatDau { get; set; }
        public DateTime? NgayLamViecKetThuc { get; set; }
        public DateTime? NgayTaoBatDau {  get; set; }
        public DateTime? NgayTaoKetThuc {  get; set; }
    }
}
