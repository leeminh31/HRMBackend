namespace HRMBackend.Resources.DTO.DonPhep.Request
{
    public class SearchDonPhepRequest
    {
        public DateTime? NgayBatDauTaoDon { get; set; }
        public DateTime? NgayKetThucTaoDon { get; set; }
        public string? MaNhanVien { get; set; }
    }
}
