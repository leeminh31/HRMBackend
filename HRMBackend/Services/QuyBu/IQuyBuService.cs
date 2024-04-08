using HRMBackend.Resources.DTO.QuyBu.Request;
using HRMBackend.Resources.DTO.QuyBu.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.QuyBu
{
    public interface IQuyBuService
    {
        Task<BaseResult<IEnumerable<QuyBuResponse>>> GetByYearAsync(SearchQuyBuRequest request);
    }
}
