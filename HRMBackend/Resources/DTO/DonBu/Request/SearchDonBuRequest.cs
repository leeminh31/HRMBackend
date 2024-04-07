namespace HRMBackend.Resources.DTO.DonBu.Request
{
    public class SearchDonBuRequest
    {
        public DateTime ngayBatDauTaoDon { get; set; }
        public DateTime ngayKetThucTaoDon { get; set; }
        public string maNhanVien {  get; set; }
    }
}
