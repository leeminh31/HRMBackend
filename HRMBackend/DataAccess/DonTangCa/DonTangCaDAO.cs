using Dapper;
using HRMBackend.DataAccess.DonTangCa;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using HRMBackend.Resources.DTO.DonTangCa.Response;

namespace HRMBackend.DataAccess.DonTangCa
{
    public partial class DonTangCaDAO : BaseDAO, IDonTangCaDAO
    {
        #region Constructor
        public DonTangCaDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.DonTangCa> data)> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.DonTangCa>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
    }
}
