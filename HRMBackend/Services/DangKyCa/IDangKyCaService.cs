using HRMBackend.Resources.DTO.DangKyCa.Request;
using HRMBackend.Resources.DTO.DangKyCa.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.DangKyCa
{
    public interface IDangKyCaService
    {
        Task<BaseResult<IEnumerable<DangKyCaResponse>>> GetByParamsAsync(SearchDangKyCaRequest request);
        Task<BaseResult<DangKyCaResponse>> CreateAsync(CreateDangKyCaRequest request);
        Task<BaseResult<bool>> ApproveShiftRequestAsync(string maGiaiTrinh, string nguoiDuyet);
        Task<BaseResult<bool>> RejectShiftRequestAsync(string maGiaiTrinh, string nguoiDuyet);
    }
}
