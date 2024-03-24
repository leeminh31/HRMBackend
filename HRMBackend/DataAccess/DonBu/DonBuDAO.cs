using Dapper;
using HRMBackend.DataAccess.DonBu;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Resources.DTO.DonBu.Response;

namespace HRMBackend.DataAccess.DonBu
{
    public partial class DonBuDAO : BaseDAO, IDonBuDAO
    {
        #region Constructor
        public DonBuDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, Models.DonBu data)> ApproveAllRequestAsync(ApproveRequestList request)
        {
            var query = ApproveAllRequestTypeQuery(request);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }

        public async Task<(bool isSuccess, Models.DonBu data)> RejectAllRequestAsync(ApproveRequestList request)
        {
            var query = RejectAllRequestTypeQuery(request);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }
        public async Task<(bool isSuccess, Models.DonBu data)> ApproveRequestAsync(string maDonBu)
        {
            var query = ApproveRequestQuery(maDonBu);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }

        public async Task<(bool isSuccess, Models.DonBu data)> CreateAsync(Models.DonBu airport)
        {
            try
            {
                var query = CreateQuery(airport);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    airport.MaDonBu = query.param.Get<int>("madonbu");
                    return (true, airport);
                }
            }
            catch (Exception ex)
            {
                return (false, airport);
            }

            return (false, airport);
        }
        public async Task<(bool isSuccess, IEnumerable<Models.DonBu> data)> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.DonBu>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }


    }
}
