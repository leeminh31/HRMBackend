using Dapper;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.PhongBan.Request;

namespace HRMBackend.DataAccess.PhongBan
{
    public partial class PhongBanDAO : BaseDAO, IPhongBanDAO
    {
        #region Constructor
        public PhongBanDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion

        #region Method
        public async Task<(bool hasValue, IEnumerable<Models.PhongBan> data)> GetByParamsAsync(SearchPhongBanRequest request)
        {
            // Excute
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.PhongBan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
        public async Task<(bool hasValue, IEnumerable<Models.PhongBan> data)> GetFilterAsync(string searchKey)
        {
            // Excute
            var query = GetFilterQuery(searchKey);
            var queryResult = await Context.QueryAsync<Models.PhongBan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, IEnumerable<Models.PhongBan> data)> GetByIdAsync()
        {
            var query = GetAllPhongBanQuery();
            var queryResult = await Context.QueryAsync<Models.PhongBan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
        public async Task<(bool isSuccess, Models.PhongBan data)> CreateAsync(Models.PhongBan airport)
        {
            //try
            //{
            //    var query = CreateQuery(airport);
            //    var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            //    // Process result
            //    if (res > 0)
            //    {
            //        airport.Id = query.param.Get<int>("idOutput");
            //        return (true, airport);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    return (false, airport);
            //}

            return (false, airport);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.PhongBan> data)> GetByCodeOrNameAsync(SearchPhongBanRequest request)
        {
            var query = GetByCodeOrNameQuery(request);
            var queryResult = await Context.QueryAsync<Models.PhongBan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.PhongBan> data, int totalRecords)> PaginationAsync(PaginationPhongBanRequest request)
        {
            // Excute
            //var query = PaginationQuery(request);
            //var queryResult = await Context.QueryAsync<Models.PhongBan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            //// Process result
            //if (queryResult.GetEnumerator().MoveNext())
            //    return (true, queryResult, queryResult.First().TotalRecords);

            return (false, default, 0);
        }

        public async Task<(bool isSuccess, Models.PhongBan data)> UpdateAsync(Models.PhongBan airport)
        {
            //var query = UpdateQuery(airport);
            //var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            //// Process result
            //if (result > 0)
            //    return (true, airport);

            return (false, airport);
        }
        #endregion
    }
}
