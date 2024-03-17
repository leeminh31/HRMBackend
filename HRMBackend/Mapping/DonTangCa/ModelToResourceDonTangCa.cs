using AutoMapper;
using HRMBackend.Resources.DTO.DonTangCa.Response;

namespace HRMBackend.Mapping.DonTangCa
{
    public class ModelToResourceDonTangCa:Profile
    {
        public ModelToResourceDonTangCa()
        {
            CreateMap<Models.DonTangCa, DonTangCaResponse>()
                .ForMember(x => x.LoaiDon, opt => opt.MapFrom(src => 4));
        }
    }
}
