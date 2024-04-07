namespace HRMBackend.DataAccess.QuyPhep
{
    public interface IQuyPhepDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.QuyPhep> data)> GetByEmployeeIDAsync(string? maNhanVien);
    }
}
