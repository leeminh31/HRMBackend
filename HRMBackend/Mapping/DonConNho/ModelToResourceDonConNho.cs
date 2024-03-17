using AutoMapper;
using HRMBackend.Resources.DTO.DonConNho.Response;

namespace HRMBackend.Mapping.DonConNho
{
    public class ModelToResourceDonConNho:Profile
    {
        public ModelToResourceDonConNho()
        {
            CreateMap<Models.DonConNho, DonConNhoResponse>()
                .ForMember(x => x.LoaiDon, opt => opt.MapFrom(src => 2));
        }
    }
}
