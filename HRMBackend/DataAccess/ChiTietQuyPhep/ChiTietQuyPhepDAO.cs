using Dapper;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;

namespace HRMBackend.DataAccess.ChiTietQuyPhep
{
    public partial class ChiTietQuyPhepDAO : BaseDAO, IChiTietQuyPhepDAO
    {
        #region Constructor
        public ChiTietQuyPhepDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyPhep> data)> GetByYearAsync(int? nam)
        {
            var query = GetByYearQuery(nam);
            var queryResult = await Context.QueryAsync<Models.ChiTietQuyPhep>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
    }
}
