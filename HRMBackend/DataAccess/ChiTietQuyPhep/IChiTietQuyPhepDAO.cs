namespace HRMBackend.DataAccess.ChiTietQuyPhep
{
    public interface IChiTietQuyPhepDAO
    {
        Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyPhep> data)> GetByYearAsync(int? nam);
    }
}
