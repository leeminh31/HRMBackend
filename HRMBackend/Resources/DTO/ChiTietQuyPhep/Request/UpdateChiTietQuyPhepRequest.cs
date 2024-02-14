using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.ChiTietQuyPhep.Request
{
    public class UpdateChiTietQuyPhepRequest
    {
        public int MaChiTietQuyPhep { get; set; }
        public int MaQuyPhep { get; set; }
        public int nam { get; set; }
        public int Thang { get; set; }
        public int SuDung { get; set; }
    }
}
