namespace HRMBackend.DataAccess.QuyBu
{
    public interface IQuyBuDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.QuyBu> data)> GetByEmployeeIDAsync(string? maNhanVien);
    }
}
