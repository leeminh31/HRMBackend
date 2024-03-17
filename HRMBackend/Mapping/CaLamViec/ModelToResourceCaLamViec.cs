using AutoMapper;
using HRMBackend.Resources.DTO.CaLamViec.Response;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;

namespace HRMBackend.Mapping.CaLamViec
{
    public class ModelToResourceCaLamViec:Profile
    {
        public ModelToResourceCaLamViec()
        {
            CreateMap<Models.CaLamViec, CaLamViecResponse>();
        }
    }
}
