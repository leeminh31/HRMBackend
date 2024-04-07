namespace HRMBackend.DataAccess.ChiTietQuyBu
{
    public interface IChiTietQuyBuDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyBu> data)> GetByYearAsync(int? nam);
    }
}
