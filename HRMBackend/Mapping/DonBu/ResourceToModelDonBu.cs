using AutoMapper;
using HRMBackend.Resources.DTO.CaLamViec.Request;
using HRMBackend.Resources.DTO.DonBu.Request;

namespace HRMBackend.Mapping.DonBu
{
    public class ResourceToModelDonBu : Profile
    {
        public ResourceToModelDonBu()
        {
            CreateMap<CreateDonBuRequest, Models.DonBu>();
            CreateMap<UpdateDonBuRequest, Models.DonBu>();
        }
    }
}
