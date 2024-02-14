namespace HRMBackend.Resources.DTO.GiaiTrinh.Response
{
    public class GiaiTrinhResponse
    {
        public int MaGiaiTrinh { get; set; }
        public DateOnly NgayTaoGiaiTrinh { get; set; }
        public DateOnly NgayLamViec { get; set; }
        public string LoaiGiaiTrinh { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public bool TrangThai { get; set; }
        public string MaNhanVien { get; set; }

    }
}
