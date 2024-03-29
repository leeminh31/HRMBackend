using AutoMapper;
using HRMBackend.Resources.DTO.DonConNho.Request;
using HRMBackend.Resources.DTO.GiaiTrinh.Request;

namespace HRMBackend.Mapping.GiaiTrinh
{
    public class ResourceToModelGiaiTrinh :Profile
    {
        public ResourceToModelGiaiTrinh()
        {
            CreateMap<CreateGiaiTrinhRequest, Models.GiaiTrinh>();
            CreateMap<UpdateGiaiTrinhRequest, Models.GiaiTrinh>();
        }
    }
}
