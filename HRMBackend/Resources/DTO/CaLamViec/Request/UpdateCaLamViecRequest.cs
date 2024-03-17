using HRMBackend.Extensions.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.CaLamViec.Request
{
    public class UpdateCaLamViecRequest
    {
        public int MaCa { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDauCa { get; set; }
        public TimeSpan GioKetThucCa { get; set; }
        public TimeSpan GioBatDauNghi { get; set; }
        public TimeSpan GioKetThucNghi { get; set; }
    }
}
