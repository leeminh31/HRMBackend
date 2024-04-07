using HRMBackend.Resources.DTO.BaoCaoTheoThang.Request;
using HRMBackend.Resources.DTO.BaoCaoTheoThang.Response;
using HRMBackend.Resources.DTO.DuLieuChamCong.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.PhanCaNhanVien
{
    public interface IBaoCaoTheoThangService
    {
        Task<BaseResult<IEnumerable<DuLieuChamCongResponse>>> GetByParamsAsync(SearchBaoCaoTheoThangRequest request);

        Task<BaseResult<List<BaoCaoTheoThangResponse>>> GetTotalHourkWorkByDayAsync(string? maNhanVien, DateTime? ngayLamViec);
        Task<BaseResult<IEnumerable<Models.NhanVien>>> AssignShiftsToEmployeeAsync();
    }
}
