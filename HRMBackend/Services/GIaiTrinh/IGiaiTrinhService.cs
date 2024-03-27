using HRMBackend.Resources.DTO.GiaiTrinh.Request;
using HRMBackend.Resources.DTO.GiaiTrinh.Response;
using HRMBackend.Results;
using Microsoft.AspNetCore.Mvc;

namespace HRMBackend.Services.GIaiTrinh
{
    public interface IGiaiTrinhService
    {
        Task<BaseResult<IEnumerable<GiaiTrinhResponse>>> GetByEmployeeIdAsync(string? maNhanVien);

        Task<BaseResult<IEnumerable<Models.GiaiTrinh>>> GetByParamsAsync(SearchGiaiTrinhRequest request);

        Task<BaseResult<bool>> ApproveAllExplanationAsync(string maGiaiTrinh, string nguoiDuyet);

        Task<BaseResult<bool>> RejectAllExplanationAsync(string maGiaiTrinh, string nguoiDuyet);

        Task<BaseResult<GiaiTrinhResponse>> CreateAsync(CreateGiaiTrinhRequest request);
    }
}
