using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.QuyPhep.Response
{
    public class QuyPhepResponse
    {
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string PhongBan { get; set; }
        public int Nam { get; set; }
        public int TongPhep { get; set; }
        public int Thang1 { get; set; }
        public int Thang2 { get; set; }
        public int Thang3 { get; set; }
        public int Thang4 { get; set; }
        public int Thang5 { get; set; }
        public int Thang6 { get; set; }
        public int Thang7 { get; set; }
        public int Thang8 { get; set; }
        public int Thang9 { get; set; }
        public int Thang10 { get; set; }
        public int Thang11 { get; set; }
        public int Thang12 { get; set; }
        public int DaDung { get; set; }
        public int ConLai { get; set; }
    }
}
