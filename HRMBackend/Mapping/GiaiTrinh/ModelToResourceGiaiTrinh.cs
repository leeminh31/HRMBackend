using AutoMapper;
using HRMBackend.Resources.DTO.DonBu.Response;
using HRMBackend.Resources.DTO.GiaiTrinh.Response;

namespace HRMBackend.Mapping.GiaiTrinh
{
    public class ModelToResourceGiaiTrinh: Profile
    {
        public ModelToResourceGiaiTrinh()
        {
            CreateMap<Models.GiaiTrinh, GiaiTrinhResponse>();
        }
    }
}
