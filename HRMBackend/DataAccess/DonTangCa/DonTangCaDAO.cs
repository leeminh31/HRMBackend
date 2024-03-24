using Dapper;
using HRMBackend.DataAccess.DonTangCa;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using HRMBackend.Resources.DTO.DonTangCa.Response;

namespace HRMBackend.DataAccess.DonTangCa
{
    public partial class DonTangCaDAO : BaseDAO, IDonTangCaDAO
    {
        #region Constructor
        public DonTangCaDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, Models.DonTangCa data)> CreateAsync(Models.DonTangCa airport)
        {
            try
            {
                var query = CreateQuery(airport);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    airport.MaDonTangCa = query.param.Get<int>("madontangca");
                    return (true, airport);
                }
            }
            catch (Exception ex)
            {
                return (false, airport);
            }

            return (false, airport);
        }
        public async Task<(bool isSuccess, Models.DonBu data)> ApproveRequestAsync(string maDonTangCa)
        {
            var query = ApproveRequestQuery(maDonTangCa);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }
        public async Task<(bool isSuccess, IEnumerable<Models.DonTangCa> data)> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.DonTangCa>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }
    }
}
