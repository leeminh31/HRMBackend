using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.CaLamViec.Response
{
    public class CaLamViecResponse
    {
        public int MaCa { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDauCa { get; set; }
        public TimeSpan GioKetThucCa { get; set; }
        public TimeSpan GioBatDauNghi { get; set; }
        public TimeSpan GioKetThucNghi { get; set; }

    }
}

