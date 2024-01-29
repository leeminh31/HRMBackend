using Dapper;
using HRMBackend.DataAccess.PhanQuyen;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.PhanQuyen.Request;

namespace HRMBackend.DataAccess.PhanQuyen
{
    public partial class PhanQuyenDAO : BaseDAO, IPhanQuyenDAO
    {
        #region Constructor
        public PhanQuyenDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion

        #region Method

        public async Task<(bool hasValue, IEnumerable<Models.PhanQuyen> data)> GetFilterAsync(string searchKey)
        {
            // Excute
            var query = GetFilterQuery(searchKey);
            var queryResult = await Context.QueryAsync<Models.PhanQuyen>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.PhanQuyen data)> GetByIdAsync(int id)
        {
            // Excute
            var query = GetByIdQuery(id);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.PhanQuyen>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }
        public async Task<(bool isSuccess, Models.PhanQuyen data)> CreateAsync(Models.PhanQuyen airport)
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

        public async Task<(bool isSuccess, IEnumerable<Models.PhanQuyen> data)> GetByCodeOrNameAsync(SearchPhanQuyenRequest request)
        {
            var query = GetByCodeOrNameQuery(request);
            var queryResult = await Context.QueryAsync<Models.PhanQuyen>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.PhanQuyen> data, int totalRecords)> PaginationAsync(PaginationPhanQuyenRequest request)
        {
            // Excute
            var query = PaginationQuery(request);
            var queryResult = await Context.QueryAsync<Models.PhanQuyen>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult, queryResult.First().TotalRecords);

            return (false, default, 0);
        }

        public async Task<(bool isSuccess, Models.PhanQuyen data)> UpdateAsync(Models.PhanQuyen airport)
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
