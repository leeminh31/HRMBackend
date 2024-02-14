using AutoMapper;
using HRMBackend.Resources.DTO.NhanVien.Request;

namespace HRMBackend.Mapping.NhanVien
{
    public class ResourceToModelNhanVien : Profile
    {
        public ResourceToModelNhanVien()
        {
            CreateMap<CreateNhanVienRequest, Models.NhanVien>();
            CreateMap<UpdateNhanVienRequest, Models.NhanVien>();
        }
    }
}
