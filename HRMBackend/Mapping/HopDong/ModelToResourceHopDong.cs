using AutoMapper;
using HRMBackend.Resources.DTO.Authentication.Response;
using HRMBackend.Resources.DTO.HopDong.Response;
using HRMBackend.Resources.DTO.NhanVien.Response;

namespace HRMBackend.Mapping.HopDong
{
    public class ModelToResourceHopDong :Profile
    {
        public ModelToResourceHopDong()
        {
            CreateMap<Models.HopDong, HopDongResponse>()
            .ForMember(x => x.NgayBatDauHopDong, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.NgayBatDauHopDong).ToString("dd/MM/yyyy")))
            .ForMember(x => x.NgayKetThucHopDong, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.NgayKetThucHopDong).ToString("dd/MM/yyyy")));
        }
    }
}
