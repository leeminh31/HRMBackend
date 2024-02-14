using HRMBackend.Extensions.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.CaLamViec.Request
{
    public class CreateCaLamViecRequest
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
