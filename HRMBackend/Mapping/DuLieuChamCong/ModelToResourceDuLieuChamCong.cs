using AutoMapper;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;
using HRMBackend.Resources.DTO.HopDong.Response;

namespace HRMBackend.Mapping.DuLieuChamCong
{
    public class ModelToResourceDuLieuChamCong:Profile
    {
        public ModelToResourceDuLieuChamCong()
        {
            CreateMap<Models.DuLieuChamCong, DuLieuChamCongResponse>();
        }

    }
}
