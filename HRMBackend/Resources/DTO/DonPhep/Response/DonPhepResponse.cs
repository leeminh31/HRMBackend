namespace HRMBackend.Resources.DTO.DonPhep.Response
{
    public class DonPhepResponse
    {
        public int MaDonPhep { get; set; }
        public int LoaiDon {  get; set; }
        public DateTime NgayTaoDon { get; set; }
        public DateTime NgayLamViec { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }

    }
}
