using Dapper;
using HRMBackend.DataAccess.QuyBu;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;

namespace HRMBackend.DataAccess.QuyBu
{
    public partial class QuyBuDAO : BaseDAO, IQuyBuDAO
    {
        #region Constructor
        public QuyBuDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion

        #region Method

        public async Task<(bool hasValue, IEnumerable<Models.QuyBu> data)> GetFilterAsync(string searchKey)
        {
            // Excute
            var query = GetFilterQuery(searchKey);
            var queryResult = await Context.QueryAsync<Models.QuyBu>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.QuyBu data)> GetByIdAsync(int id)
        {
            // Excute
            var query = GetByIdQuery(id);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.QuyBu>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }
        public async Task<(bool isSuccess, Models.QuyBu data)> CreateAsync(Models.QuyBu airport)
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

        public async Task<(bool isSuccess, IEnumerable<Models.QuyBu> data)> GetByCodeOrNameAsync(SearchQuyBuRequest request)
        {
            var query = GetByCodeOrNameQuery(request);
            var queryResult = await Context.QueryAsync<Models.QuyBu>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.QuyBu> data, int totalRecords)> PaginationAsync(PaginationQuyBuRequest request)
        {
            // Excute
            var query = PaginationQuery(request);
            var queryResult = await Context.QueryAsync<Models.QuyBu>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult, queryResult.First().TotalRecords);

            return (false, default, 0);
        }

        public async Task<(bool isSuccess, Models.QuyBu data)> UpdateAsync(Models.QuyBu airport)
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
