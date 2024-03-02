using AutoMapper;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;

namespace HRMBackend.Mapping.DuLieuChamCong
{
    public class ResourceToModelDuLieuChamCong:Profile
    {
        public ResourceToModelDuLieuChamCong()
        {
            CreateMap<CreateDuLieuChamCongRequest, Models.DuLieuChamCong>();
            CreateMap<UpdateDuLieuChamCongRequest, Models.DuLieuChamCong>();
        }
    }
}
