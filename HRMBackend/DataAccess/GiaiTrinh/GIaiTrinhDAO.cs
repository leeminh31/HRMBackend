using Dapper;
using HRMBackend.DataAccess.GiaiTrinh;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.GiaiTrinh.Request;

namespace HRMBackend.DataAccess.GiaiTrinh
{
    public partial class GiaiTrinhDAO : BaseDAO, IGiaiTrinhDAO
    {
        #region Constructor
        public GiaiTrinhDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.GiaiTrinh> data)> GetByParamsAsync(SearchGiaiTrinhRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.GiaiTrinh>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, Models.GiaiTrinh data)> ApproveAllExplanationAsync(string maGiaiTrinh)
        {
            var query = ApproveAllExplantionTypeQuery(maGiaiTrinh);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }

        public async Task<(bool isSuccess, Models.GiaiTrinh data)> RejectAllExplanationAsync(string maGiaiTrinh)
        {
            var query = RejectAllExplanationTypeQuery(maGiaiTrinh);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }
    }
}
