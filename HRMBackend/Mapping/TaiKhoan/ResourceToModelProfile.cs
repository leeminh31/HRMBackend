using AutoMapper;
using HRMBackend.Extensions;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.TaiKhoan.Request;

namespace HRMBackend.Mapping.TaiKhoan
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<CreateTaiKhoanRequest, Models.TaiKhoan>()
                .ForMember(x => x.MatKhau, opt => opt.MapFrom(src => src.MatKhau.HashingPassword(Constant.IterationCount)));
        }
    }
}
