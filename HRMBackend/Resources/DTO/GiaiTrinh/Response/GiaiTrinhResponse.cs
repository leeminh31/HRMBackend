namespace HRMBackend.Resources.DTO.GiaiTrinh.Response
{
    public class GiaiTrinhResponse
    {
        public int MaGiaiTrinh { get; set; }
        public DateTime NgayTaoGiaiTrinh { get; set; }
        public DateTime NgayLamViec { get; set; }
        public string LoaiGiaiTrinh { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }

    }
}
