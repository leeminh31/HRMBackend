namespace HRMBackend.DataAccess.CaLamViec
{
    public interface ICaLamViecDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.CaLamViec> data)> GetByShiftIDAsync(int? maCaLamViec);
    }
}
