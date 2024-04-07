using HRMBackend.Resources.DTO.QuyPhep.Request;
using HRMBackend.Resources.DTO.QuyPhep.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.QuyPhep
{
    public interface IQuyPhepService
    {
        Task<BaseResult<IEnumerable<QuyPhepResponse>>> GetByYearAsync(SearchQuyPhepRequest request);
    }
}
