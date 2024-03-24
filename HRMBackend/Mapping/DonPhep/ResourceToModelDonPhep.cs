using AutoMapper;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Resources.DTO.DonPhep.Request;

namespace HRMBackend.Mapping.DonPhep
{
    public class ResourceToModelDonPhep : Profile
    {
        public ResourceToModelDonPhep()
        {
            CreateMap<CreateDonPhepRequest, Models.DonPhep>();
        }
    }
}
