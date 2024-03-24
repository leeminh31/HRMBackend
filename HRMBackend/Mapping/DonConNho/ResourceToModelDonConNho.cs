using AutoMapper;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Resources.DTO.DonConNho.Request;

namespace HRMBackend.Mapping.DonConNho
{
    public class ResourceToModelDonConNho : Profile
    {
        public ResourceToModelDonConNho()
        {
            CreateMap<CreateDonConNhoRequest, Models.DonConNho>();
        }
    }
}
