using Dapper;
using HRMBackend.DataAccess.DonConNho;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonConNho.Request;
using HRMBackend.Resources.DTO.DonConNho.Response;

namespace HRMBackend.DataAccess.DonConNho
{
    public partial class DonConNhoDAO : BaseDAO, IDonConNhoDAO
    {
        #region Constructor
        public DonConNhoDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.DonConNho> data)> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.DonConNho>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
    }
}
