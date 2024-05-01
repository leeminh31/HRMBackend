using Dapper;
using HRMBackend.DataAccess.QuyPhep;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;

namespace HRMBackend.DataAccess.QuyBu
{
    public partial class QuyBuDAO : BaseDAO, IQuyBuDAO
    {
        #region Constructor
        public QuyBuDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.QuyBu> data)> GetByEmployeeIDAsync(string? maNhanVien)
        {
            var query = GetByEmployeeIDQuery(maNhanVien);
            var queryResult = await Context.QueryAsync<Models.QuyBu>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyBu> data)> GetByYearForEmployeeAsync(string maNhanVien, int nam)
        {
            var query = GetByYearForEmployeeQuery(maNhanVien, nam);
            var queryResult = await Context.QueryAsync<Models.ChiTietQuyBu>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
    }
}
