using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.CaLamViec.Response
{
    public class CaLamViecResponse
    {
        public int MaCa { get; set; }
        public string TenCa { get; set; }
        public TimeOnly GioBatDauCa { get; set; }
        public TimeOnly GioKetThucCa { get; set; }
        public TimeOnly GioBatDauNghi { get; set; }
        public TimeOnly GioKetThucNghi { get; set; }
        public string MaNhanVien { get; set; }

    }
}

