using HRMBackend.Resources.DTO.CaLamViec.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.CaLamViec
{
    public interface ICaLamViecService
    {
        Task<BaseResult<IEnumerable<CaLamViecResponse>>> GetByShiftIdAsync(int? maCaLamViec);
    }
}
