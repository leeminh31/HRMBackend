namespace HRMBackend.DataAccess.QuyBu
{
    public interface IQuyBuDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.QuyBu> data)> GetByEmployeeIDAsync(string? maNhanVien);
        Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyBu> data)> GetByYearForEmployeeAsync(string maNhanVien, int nam);
    }
}
