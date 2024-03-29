using HRMBackend.Resources.DTO.GiaiTrinh.Request;

namespace HRMBackend.DataAccess.GiaiTrinh
{
    public interface IGiaiTrinhDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.GiaiTrinh> data)> GetByParamsAsync(SearchGiaiTrinhRequest request);
        Task<(bool isSuccess, Models.GiaiTrinh data)> ApproveAllExplanationAsync(string maGiaiTrinh, string nguoiDuyet);
        Task<(bool isSuccess, Models.GiaiTrinh data)> RejectAllExplanationAsync(string maGiaiTrinh, string nguoiDuyet);
        Task<(bool isSuccess, Models.GiaiTrinh data)> CreateAsync(Models.GiaiTrinh airport);
        Task<(bool isSuccess, Models.GiaiTrinh data)> UpdateAsync(Models.GiaiTrinh airport);
    }
}
