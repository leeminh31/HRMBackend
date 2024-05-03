namespace HRMBackend.Resources.DTO.DonTangCa.Response
{
    public class DonTangCaResponse
    {
        public int MaDonTangCa { get; set; }
        public int LoaiDon {  get; set; }
        public DateTime NgayTaoDon { get; set; }
        public DateTime NgayLamViec { get; set; }
        public TimeSpan TangCaTu { get; set; }
        public TimeSpan TangCaDen { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }


    }
}
