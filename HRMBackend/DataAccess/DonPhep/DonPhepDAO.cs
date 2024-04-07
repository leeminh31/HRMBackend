using Dapper;
using HRMBackend.DataAccess.DonPhep;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Resources.DTO.DonPhep.Response;

namespace HRMBackend.DataAccess.DonPhep
{
    public partial class DonPhepDAO : BaseDAO, IDonPhepDAO
    {
        #region Constructor
        public DonPhepDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, Models.DonPhep data)> CreateAsync(Models.DonPhep airport)
        {
            try
            {
                var query = CreateQuery(airport);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    airport.MaDonPhep = query.param.Get<int>("madonphep");
                    return (true, airport);
                }
            }
            catch (Exception ex)
            {
                return (false, airport);
            }

            return (false, airport);
        }

        public async Task<(bool isSuccess, Models.DonPhep data)> UpdateAsync(Models.DonPhep airport)
        {
            var query = UpdateQuery(airport);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, airport);

            return (false, airport);
        }

        public async Task<(bool isSuccess, Models.DonPhep data)> ApproveRequestAsync(string maDonPhep)
        {
            var query = ApproveRequestQuery(maDonPhep);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }
        public async Task<(bool isSuccess, IEnumerable<Models.DonPhep> data)> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.DonPhep>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.DonPhep> data)> GetDayOffAsync(SearchDonPhepRequest request)
        {
            var query = GetDayOffQuery(request);
            var queryResult = await Context.QueryAsync<Models.DonPhep>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, int data)> GetDayOffByYearAndIDAsync(string maNhanVien, int nam)
        {
            var query = GetDayOffByYearAndIDQuery(maNhanVien, nam);
            var queryResult = await Context.QuerySingleOrDefaultAsync<int>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult > 0)
                return (true, queryResult);

            return (false, default);
        }
    }
}
