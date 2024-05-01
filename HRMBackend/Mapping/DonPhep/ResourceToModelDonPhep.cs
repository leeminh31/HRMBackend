using AutoMapper;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Resources.DTO.DonPhep.Request;

namespace HRMBackend.Mapping.DonPhep
{
    public class ResourceToModelDonPhep : Profile
    {
        public ResourceToModelDonPhep()
        {
            CreateMap<CreateDonPhepRequest, Models.DonPhep>().ForMember(x => x.ThoiGianCapNhat, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateDonPhepRequest, Models.DonPhep>();
        }
    }
}
