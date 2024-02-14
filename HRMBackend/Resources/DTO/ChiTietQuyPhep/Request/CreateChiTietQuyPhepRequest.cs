using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.ChiTietQuyPhep.Request
{
    public class CreateChiTietQuyPhepRequest
    {
        public int MaChiTietQuyPhep { get; set; }
        public int MaQuyPhep { get; set; }
        public int nam { get; set; }
        public int Thang { get; set; }
        public int SuDung { get; set; }
    }
}
