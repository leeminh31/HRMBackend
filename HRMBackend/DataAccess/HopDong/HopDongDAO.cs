using Dapper;
using HRMBackend.DataAccess.HopDong;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;

namespace HRMBackend.DataAccess.HopDong
{
    public partial class HopDongDAO : BaseDAO, IHopDongDAO
    {
        #region Constructor
        public HopDongDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion

        #region Method

        public async Task<(bool hasValue, IEnumerable<Models.HopDong> data)> GetAllContractAsync()
        {
            // Excute
            var query = GetAllContractQuery();
            var queryResult = await Context.QueryAsync<Models.HopDong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, IEnumerable<Models.HopDong> data)> GetFilterAsync(string searchKey)
        {
            // Excute
            var query = GetFilterQuery(searchKey);
            var queryResult = await Context.QueryAsync<Models.HopDong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, IEnumerable<Models.HopDong> data)> GetContractByYearAsync(string? maNhanVien, int? nam )
        {
            // Excute
            var query = GetContractByYearQuery(maNhanVien, nam);
            var queryResult = await Context.QueryAsync<Models.HopDong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, IEnumerable<Models.HopDong> data)> GetContractByTimeRangeAndIDAsync(string maNhanVien, DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            // Excute
            var query = GetContractByTimeRangeAndIDQuery(maNhanVien, ngayKetThuc, ngayBatDau);
            var queryResult = await Context.QueryAsync<Models.HopDong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.HopDong data)> GetByIDAsync(string maHopDong)
        {
            // Excute
            var query = GetByIdQuery(maHopDong);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.HopDong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }
        public async Task<(bool isSuccess, Models.HopDong data)> CreateAsync(Models.HopDong nhanvien)
        {
            try
            {
                var query = CreateQuery(nhanvien);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    //nhanvien.MaHopDong = query.param.Get<string>("idOutput");
                    return (true, nhanvien);
                }
            }
            catch (Exception ex)
            {

                return (false, nhanvien);
            }

            return (false, nhanvien);
        }

        //public async Task<(bool isSuccess, IEnumerable<Models.HopDong> data)> GetByCodeOrNameAsync(SearchHopDongRequest request)
        //{
        //    var query = GetByCodeOrNameQuery(request);
        //    var queryResult = await Context.QueryAsync<Models.HopDong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (queryResult.GetEnumerator().MoveNext())
        //        return (true, queryResult);

        //    return (false, default);
        //}

        public async Task<(bool isSuccess, IEnumerable<Models.HopDong> data)> GetByParamsAsync(string? tenHopDong, string? loaiHopDong)
        {
            var query = GetByParamsQuery(tenHopDong, loaiHopDong);
            var queryResult = await Context.QueryAsync<Models.HopDong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        //public async Task<(bool isSuccess, IEnumerable<Models.HopDong> data, int totalRecords)> PaginationAsync(PaginationHopDongRequest request)
        //{
        //    // Excute
        //    var query = PaginationQuery(request);
        //    var queryResult = await Context.QueryAsync<Models.HopDong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (queryResult.GetEnumerator().MoveNext())
        //        return (true, queryResult, queryResult.First().TotalRecords);

        //    return (false, default, 0);
        //}

        public async Task<(bool isSuccess, Models.HopDong data)> UpdateAsync(Models.HopDong airport)
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
