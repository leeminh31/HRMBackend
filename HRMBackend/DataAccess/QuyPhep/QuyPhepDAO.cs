using Dapper;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;

namespace HRMBackend.DataAccess.QuyPhep
{
    public partial class QuyPhepDAO : BaseDAO, IQuyPhepDAO
    {
        #region Constructor
        public QuyPhepDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.QuyPhep> data)> GetByEmployeeIDAsync(string? maNhanVien)
        {
            var query = GetByEmployeeIDQuery(maNhanVien);
            var queryResult = await Context.QueryAsync<Models.QuyPhep>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
    }
}
