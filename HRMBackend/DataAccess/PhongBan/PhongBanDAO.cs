using Dapper;
using HRMBackend.DataAccess.PhongBan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Models;
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

        public async Task<(bool hasValue, Models.PhongBan data)> GetByTenPhongBanAsync(string tenPhongBan, int? maPhongBan)
        {
            // Excute
            var query = GetByTenPhongBanQuery(tenPhongBan, maPhongBan);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.PhongBan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
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
            var query = GetByIdQuery(2);
            var queryResult = await Context.QueryAsync<Models.PhongBan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
        public async Task<(bool isSuccess, Models.PhongBan data)> CreateAsync(Models.PhongBan airport)
        {
            try
            {
                var query = CreateQuery(airport);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    airport.MaPhongBan = query.param.Get<int>("maphongban");
                    return (true, airport);
                }
            }
            catch (Exception ex)
            {
                return (false, airport);
            }

            return (false, airport);
        }

        //public async Task<(bool isSuccess, IEnumerable<Models.PhongBan> data)> GetByCodeOrNameAsync(SearchPhongBanRequest request)
        //{
        //    var query = GetByCodeOrNameQuery(request);
        //    var queryResult = await Context.QueryAsync<Models.PhongBan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (queryResult.GetEnumerator().MoveNext())
        //        return (true, queryResult);

        //    return (false, default);
        //}

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
            var query = UpdateQuery(airport);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, airport);

            return (false, airport);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            // Excute
            var query = DeleteQuery(id);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            return result > 0 ? true : false;
        }
        #endregion
    }
}
