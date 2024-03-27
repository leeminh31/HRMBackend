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
        public async Task<(bool isSuccess, Models.GiaiTrinh data)> CreateAsync(Models.GiaiTrinh airport)
        {
            try
            {
                var query = CreateQuery(airport);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    airport.MaGiaiTrinh = query.param.Get<int>("magiaitrinh");
                    return (true, airport);
                }
            }
            catch (Exception ex)
            {
                return (false, airport);
            }

            return (false, airport);
        }
        public async Task<(bool isSuccess, IEnumerable<Models.GiaiTrinh> data)> GetByParamsAsync(SearchGiaiTrinhRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.GiaiTrinh>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, Models.GiaiTrinh data)> ApproveAllExplanationAsync(string maGiaiTrinh, string nguoiDuyet)
        {
            var query = ApproveAllExplantionTypeQuery(maGiaiTrinh, nguoiDuyet);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }

        public async Task<(bool isSuccess, Models.GiaiTrinh data)> RejectAllExplanationAsync(string maGiaiTrinh, string nguoiDuyet)
        {
            var query = RejectAllExplanationTypeQuery(maGiaiTrinh, nguoiDuyet);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }
    }
}
