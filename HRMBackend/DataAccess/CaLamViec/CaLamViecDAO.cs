using Dapper;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;

namespace HRMBackend.DataAccess.CaLamViec
{
    public partial class CaLamViecDAO : BaseDAO, ICaLamViecDAO
    {
        #region Constructor
        public CaLamViecDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.CaLamViec> data)> GetByShiftIDAsync(int? maCaLamViec)
        {
            var query = GetByShiftIDQuery(maCaLamViec);
            var queryResult = await Context.QueryAsync<Models.CaLamViec>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
    }
}

