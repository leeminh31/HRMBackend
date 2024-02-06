using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.QuyPhep.Request
{
    public class UpdateQuyPhepRequest
    {
        public int MaQuyPhep { get; set; }
        public string MaNhanVien { get; set; }
    }
}
