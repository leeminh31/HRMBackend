namespace HRMBackend.DataAccess.CaLamViec
{
    public interface ICaLamViecDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.CaLamViec> data)> GetByShiftIDAsync(int? maCaLamViec, string? tenCa);
        Task<(bool isSuccess, Models.CaLamViec data)> CreateAsync(Models.CaLamViec airport);
        Task<(bool isSuccess, Models.CaLamViec data)> UpdateAsync(Models.CaLamViec airport);
        Task<(bool hasValue, Models.CaLamViec data)> GetByShiftNameAsync(string? tenCa);
        Task<bool> DeleteAsync(string id);
    }
}
