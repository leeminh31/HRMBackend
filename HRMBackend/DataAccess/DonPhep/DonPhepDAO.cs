using Dapper;
using HRMBackend.DataAccess.DonPhep;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Resources.DTO.DonPhep.Response;

namespace HRMBackend.DataAccess.DonPhep
{
    public partial class DonPhepDAO : BaseDAO, IDonPhepDAO
    {
        #region Constructor
        public DonPhepDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.DonPhep> data)> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.DonPhep>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
    }
}
