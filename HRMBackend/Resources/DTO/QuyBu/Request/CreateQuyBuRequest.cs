using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.QuyBu.Request
{
    public class CreateQuyBuRequest
    {
        public int MaQuyBu { get; set; }
        public string MaNhanVien { get; set; }
    }
}
