using Dapper;
using HRMBackend.DataAccess.ChiTietQuyPhep;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.ChiTietQuyPhep.Request;
using HRMBackend.Resources.DTO.ChiTietQuyPhep.Request;

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

        #region Method

        public async Task<(bool hasValue, IEnumerable<Models.ChiTietQuyPhep> data)> GetFilterAsync(string searchKey)
        {
            // Excute
            var query = GetFilterQuery(searchKey);
            var queryResult = await Context.QueryAsync<Models.ChiTietQuyPhep>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.ChiTietQuyPhep data)> GetByIdAsync(int id)
        {
            // Excute
            var query = GetByIdQuery(id);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.ChiTietQuyPhep>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }
        public async Task<(bool isSuccess, Models.ChiTietQuyPhep data)> CreateAsync(Models.ChiTietQuyPhep airport)
        {
            try
            {
                var query = CreateQuery(airport);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    airport.Id = query.param.Get<int>("idOutput");
                    return (true, airport);
                }
            }
            catch (Exception ex)
            {

                return (false, airport);
            }

            return (false, airport);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyPhep> data)> GetByCodeOrNameAsync(SearchChiTietQuyPhepRequest request)
        {
            var query = GetByCodeOrNameQuery(request);
            var queryResult = await Context.QueryAsync<Models.ChiTietQuyPhep>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.ChiTietQuyPhep> data, int totalRecords)> PaginationAsync(PaginationChiTietQuyPhepRequest request)
        {
            // Excute
            var query = PaginationQuery(request);
            var queryResult = await Context.QueryAsync<Models.ChiTietQuyPhep>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult, queryResult.First().TotalRecords);

            return (false, default, 0);
        }

        public async Task<(bool isSuccess, Models.ChiTietQuyPhep data)> UpdateAsync(Models.ChiTietQuyPhep airport)
        {
            var query = UpdateQuery(airport);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, airport);

            return (false, airport);
        }
        #endregion
    }
}
