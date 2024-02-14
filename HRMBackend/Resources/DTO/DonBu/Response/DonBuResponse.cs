namespace HRMBackend.Resources.DTO.DonBu.Response
{
    public class DonBuResponse
    {
        public int MaDonBu { get; set; }
        public DateOnly NgayTaoDon { get; set; }
        public DateOnly NgayLamViec { get; set; }
        public int SoPhutXinBu { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public bool TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
