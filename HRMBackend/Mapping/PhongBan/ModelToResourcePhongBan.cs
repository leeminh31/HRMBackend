using AutoMapper;
using HRMBackend.Resources.DTO.PhongBan.Response;

namespace HRMBackend.Mapping.PhongBan
{
    public class ModelToResourcePhongBan: Profile
    {
        public ModelToResourcePhongBan() { 
            CreateMap<Models.PhongBan, PhongBanResponse>();
        
        }
    }
}
