using Dapper;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;

namespace HRMBackend.DataAccess.CaLamViec
{
    public partial class CaLamViecDAO : BaseDAO, ICaLamViecDAO
    {
        #region Constructor
        public CaLamViecDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.CaLamViec> data)> GetByShiftIDAsync(int? maCaLamViec, string? tenCa)
        {
            var query = GetByShiftIDQuery(maCaLamViec, tenCa);
            var queryResult = await Context.QueryAsync<Models.CaLamViec>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.CaLamViec data)> GetByShiftNameAsync(string? tenCa)
        {
            var query = GetByShiftNameQuery(tenCa);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.CaLamViec>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, Models.CaLamViec data)> CreateAsync(Models.CaLamViec airport)
        {
            try
            {
                var query = CreateQuery(airport);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    airport.MaCa = query.param.Get<int>("maca");
                    return (true, airport);
                }
            }
            catch (Exception ex)
            {
                return (false, airport);
            }

            return (false, airport);
        }

        public async Task<(bool isSuccess, Models.CaLamViec data)> UpdateAsync(Models.CaLamViec airport)
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
    }
}

