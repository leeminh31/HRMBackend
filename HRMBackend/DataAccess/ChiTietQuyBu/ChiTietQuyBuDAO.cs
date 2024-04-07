using Dapper;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;

namespace HRMBackend.DataAccess.ChiTietQuyBu
{
    public partial class ChiTietQuyBuDAO : BaseDAO, IChiTietQuyBuDAO
    {
        #region Constructor
        public ChiTietQuyBuDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyBu> data)> GetByYearAsync(int? nam)
        {
            var query = GetByYearQuery(nam);
            var queryResult = await Context.QueryAsync<Models.ChiTietQuyBu>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
    }
}
