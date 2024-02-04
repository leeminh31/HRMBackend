using AutoMapper;
using HRMBackend.Resources.DTO.NhanVien.Response;

namespace HRMBackend.Mapping.NhanVien
{
    public class ModelToResourceNhanVien : Profile
    {
        public ModelToResourceNhanVien()
        {
            CreateMap<Models.NhanVien, NhanVienResponse>();
        }
    }
}
