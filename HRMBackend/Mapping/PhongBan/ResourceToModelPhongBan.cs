using AutoMapper;
using HRMBackend.Resources.DTO.PhongBan.Request;
using HRMBackend.Resources.DTO.TaiKhoan.Request;

namespace HRMBackend.Mapping.PhongBan
{
    public class ResourceToModelPhongBan :Profile
    {
        public ResourceToModelPhongBan()
        {
            CreateMap<CreatePhongBanRequest, Models.PhongBan>();
            CreateMap<UpdatePhongBanRequest, Models.PhongBan>();
        }
    }
}
