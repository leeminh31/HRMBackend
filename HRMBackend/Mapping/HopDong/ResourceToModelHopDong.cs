using AutoMapper;
using HRMBackend.Resources.DTO.HopDong.Request;

namespace HRMBackend.Mapping.HopDong
{
    public class ResourceToModelHopDong :Profile
    {
        public ResourceToModelHopDong()
        {
            CreateMap<CreateHopDongRequest, Models.HopDong>();
            CreateMap<UpdateHopDongRequest, Models.HopDong>();
        }
    }
}
