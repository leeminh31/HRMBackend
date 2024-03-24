namespace HRMBackend.Resources.DTO.GiaiTrinh.Request
{
    public class SearchGiaiTrinhRequest
    {
        public string? TenNhanVien { get; set; }
        public string? LoaiGiaiTrinh { get; set; }
        public string? TrangThai { get; set; }
        public DateTime? NgayLamViecBatDau { get; set; }
        public DateTime? NgayLamViecKetThuc { get; set; }
        public DateTime? NgayTaoBatDau { get; set; }
        public DateTime? NgayTaoKetThuc { get; set; }
    }
}
