using AutoMapper;
using HRMBackend.Resources.DTO.NhanVien.Response;

namespace HRMBackend.Mapping.NhanVien
{
    public class ModelToResourceNhanVien : Profile
    {
        public ModelToResourceNhanVien()
        {
            CreateMap<Models.NhanVien, NhanVienResponse>()
            .ForMember(x => x.NgaySinh, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.NgaySinh).ToString("dd/MM/yyyy")))
            .ForMember(x => x.NgayCap, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.NgayCap).ToString("dd/MM/yyyy")));
        }
    }
}
