using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Resources.DTO.QuyBu.Response
{
    public class QuyBuResponse
    {
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string PhongBan { get; set; }
        public int Nam {  get; set; }
        public IEnumerable<QuyBuThang> QuyBuThangs { get; set; }
        public int PhatSinh { get; set; }
        public int SuDung { get; set;}
        public int ConLai { get; set; }
    }

    public class QuyBuThang
    {
        public int Thang { get; set; }
        public int PhatSinh { get; set; }
        public int SuDung { get; set; }
    }
}
