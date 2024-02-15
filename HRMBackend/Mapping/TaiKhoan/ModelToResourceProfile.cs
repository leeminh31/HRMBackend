using AutoMapper;
using HRMBackend.Resources.DTO.Authentication.Response;
using HRMBackend.Resources.DTO.TaiKhoan.Response;

namespace HRMBackend.Mapping.TaiKhoan
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Models.TaiKhoan, TaiKhoanResponse>();
            CreateMap<Models.TaiKhoan, AccessTokenResponse>();
        }
    }
}
