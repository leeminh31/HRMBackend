using Dapper;
using HRMBackend.DataAccess.DangKyCa;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DangKyCa.Request;

namespace HRMBackend.DataAccess.DangKyCa
{
    public partial class DangKyCaDAO : BaseDAO, IDangKyCaDAO
    {
        #region Constructor
        public DangKyCaDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, IEnumerable<Models.DangKyCa> data)> GetByParamsAsync(SearchDangKyCaRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QueryAsync<Models.DangKyCa>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.DangKyCa> data)> GetByEmployeeIDAsync(string? maNhanVien, DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            var query = GetByEmployeeIDQuery(maNhanVien, ngayBatDau, ngayKetThuc);
            var queryResult = await Context.QueryAsync<Models.DangKyCa>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.DangKyCa> data)> GetLatestShiftToUpdateAsync()
        {
            var query = GetLatestShiftToUpdateQuery();
            var queryResult = await Context.QueryAsync<Models.DangKyCa>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isSuccess, Models.DangKyCa data)> CreateAsync(Models.DangKyCa airport)
        {
            try
            {
                var query = CreateQuery(airport);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    airport.MaDangKyCa = query.param.Get<int>("madangkyca");
                    return (true, airport);
                }
            }
            catch (Exception ex)
            {
                return (false, airport);
            }

            return (false, airport);
        }

        public async Task<(bool isSuccess, Models.DangKyCa data)> ApproveShiftRequestAsync(string maDangKyCa, string nguoiDuyet)
        {
            var query = ApproveShiftRequestQuery(maDangKyCa, nguoiDuyet);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }

        public async Task<(bool isSuccess, Models.DangKyCa data)> RejectShiftRequestAsync(string maDangKyCa, string nguoiDuyet)
        {
            var query = RejectShiftRequestQuery(maDangKyCa, nguoiDuyet);
            var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (result > 0)
                return (true, default);

            return (false, default);
        }
    }
}
