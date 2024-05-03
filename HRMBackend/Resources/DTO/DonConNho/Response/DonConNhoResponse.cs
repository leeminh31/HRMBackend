namespace HRMBackend.Resources.DTO.DonConNho.Response
{
    public class DonConNhoResponse
    {
        public int MaDonConNho { get; set; }
        public int LoaiDon {  get; set; }
        public DateTime NgayTaoDon { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }


    }
}
