using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.GiaiTrinh.Request
{
    public class CreateGiaiTrinhRequest
    {
        public DateTime NgayTaoGiaiTrinh { get; set; }
        public DateTime NgayLamViec { get; set; }
        public string LoaiGiaiTrinh { get; set; }
        public string LyDo { get; set; }
        public string NguoiDuyet { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
    }
}
