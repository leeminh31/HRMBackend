using AutoMapper;
using HRMBackend.Resources.DTO.DangKyCa.Request;

namespace HRMBackend.Mapping.DangKyCa
{
    public class ResourceToModelDangKyCa : Profile
    {
        public ResourceToModelDangKyCa()
        {
            CreateMap<CreateDangKyCaRequest, Models.DangKyCa>();
        }
    }
}
