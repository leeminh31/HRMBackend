namespace HRMBackend.Resources.DTO.DonBu.Response
{
    public class DonBuResponse
    {
        public int MaDonBu { get; set; }
        public int LoaiDon {  get; set; }
        public DateTime NgayTaoDon { get; set; }
        public DateTime NgayLamViec { get; set; }
        public int SoPhutXinBu { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }
    }
}
