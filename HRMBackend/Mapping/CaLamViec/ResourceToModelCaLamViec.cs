using AutoMapper;
using HRMBackend.Resources.DTO.CaLamViec.Request;

namespace HRMBackend.Mapping.CaLamViec
{
    public class ResourceToModelCaLamViec :Profile
    {
        public ResourceToModelCaLamViec()
        {
            CreateMap<CreateCaLamViecRequest, Models.CaLamViec>();
            CreateMap<UpdateCaLamViecRequest, Models.CaLamViec>();
        }
    }
}
