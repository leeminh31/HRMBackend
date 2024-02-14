namespace HRMBackend.Resources.DTO.DonPhep.Response
{
    public class DonPhepResponse
    {
        public int MaDonPhep { get; set; }
        public DateOnly NgayTaoDon { get; set; }
        public DateOnly NgayLamViec { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public bool TrangThai { get; set; }
        public string MaNhanVien { get; set; }

    }
}
