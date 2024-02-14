using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.ChiTietQuyBu.Request
{
    public class CreateChiTietQuyBuRequest
    {
        public int MaChiTietQuyBu { get; set; }
        public int MaQuyBu { get; set; }
        public int Thang { get; set; }
        public int PhatSinh { get; set; }
        public int Nam { get; set; }
        public int SuDung { get; set; }
    }
}
