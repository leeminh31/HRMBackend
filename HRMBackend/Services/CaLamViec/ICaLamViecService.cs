using HRMBackend.Resources.DTO.CaLamViec.Request;
using HRMBackend.Resources.DTO.CaLamViec.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.CaLamViec
{
    public interface ICaLamViecService
    {
        Task<BaseResult<IEnumerable<CaLamViecResponse>>> GetByShiftIdAsync(int? maCaLamViec, string? tenCa);
        Task<BaseResult<CaLamViecResponse>> CreateAsync(CreateCaLamViecRequest request);
        Task<BaseResult<CaLamViecResponse>> UpdateAsync(UpdateCaLamViecRequest request);
        Task<BaseResult<bool>> DeleteAsync(string id);
    }
}
