namespace HRMBackend.Resources.DTO.DonBu.Request
{
    public class SearchDonBuRequest
    {
        public DateTime? NgayBatDauTaoDon { get; set; }
        public DateTime? NgayKetThucTaoDon { get; set; }
        public string? MaNhanVien {  get; set; }
    }
}
