using AutoMapper;
using HRMBackend.Resources.DTO.DonBu.Response;

namespace HRMBackend.Mapping.DonBu
{
    public class ModelToResourceDonBu:Profile
    {
        public ModelToResourceDonBu()
        {
            CreateMap<Models.DonBu, DonBuResponse>()
                .ForMember(x => x.LoaiDon, opt => opt.MapFrom(src => 1));
        }
    }
}
