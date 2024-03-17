using AutoMapper;
using HRMBackend.Resources.DTO.DonPhep.Response;

namespace HRMBackend.Mapping.DonPhep
{
    public class ModelToResourceDonPhep:Profile
    {
        public ModelToResourceDonPhep()
        {
            CreateMap<Models.DonPhep, DonPhepResponse>()
                .ForMember(x => x.LoaiDon, opt => opt.MapFrom(src => 3));
        }
    }
}
