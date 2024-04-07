using AutoMapper;
using HRMBackend.Resources.DTO.DangKyCa.Response;

namespace HRMBackend.Mapping.DangKyCa
{
    public class ModelToResourceDangKyCa : Profile
    {
        public ModelToResourceDangKyCa()
        {
            CreateMap<Models.DangKyCa, DangKyCaResponse>();
        }
    }
}
