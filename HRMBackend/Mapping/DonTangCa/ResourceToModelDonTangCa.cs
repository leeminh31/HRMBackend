using AutoMapper;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Resources.DTO.DonTangCa.Request;

namespace HRMBackend.Mapping.DonTangCa
{
    public class ResourceToModelDonTangCa : Profile
    {
        public ResourceToModelDonTangCa()
        {
            CreateMap<CreateDonTangCaRequest, Models.DonTangCa>().ForMember(x => x.ThoiGianCapNhat, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateDonTangCaRequest, Models.DonTangCa>();
        }
    }
}
