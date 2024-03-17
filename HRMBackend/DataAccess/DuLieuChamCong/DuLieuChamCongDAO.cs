using Dapper;
using HRMBackend.DataAccess.DuLieuChamCong;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;

namespace HRMBackend.DataAccess.DuLieuChamCong
{
    public partial class DuLieuChamCongDAO : BaseDAO, IDuLieuChamCongDAO
    {
        #region Constructor
        public DuLieuChamCongDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion

        #region Method

        public async Task<(bool hasValue, IEnumerable<Models.DuLieuChamCong> data)> GetAllContractAsync()
        {
            // Excute
            var query = GetAllContractQuery();
            var queryResult = await Context.QueryAsync<Models.DuLieuChamCong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, IEnumerable<Models.DuLieuChamCong> data)> GetTotalHourkWorkByDayAsync(string? maNhanVien, DateTime? ngayLamViec)
        {
            // Excute
            var query = GetTotalHourkWorkByDayQuery(maNhanVien,ngayLamViec);
            var queryResult = await Context.QueryAsync<Models.DuLieuChamCong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.DuLieuChamCong data)> GetByIDAsync(string maDuLieuChamCong)
        {
            // Excute
            var query = GetByIdQuery(maDuLieuChamCong);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.DuLieuChamCong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }
        //public async Task<(bool isSuccess, Models.DuLieuChamCong data)> CreateAsync(Models.DuLieuChamCong nhanvien)
        //{
        //    try
        //    {
        //        var query = CreateQuery(nhanvien);
        //        var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //        // Process result
        //        if (res > 0)
        //        {
        //            //nhanvien.MaDuLieuChamCong = query.param.Get<string>("idOutput");
        //            return (true, nhanvien);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return (false, nhanvien);
        //    }

        //    return (false, nhanvien);
        //}

        //public async Task<(bool isSuccess, IEnumerable<Models.DuLieuChamCong> data)> GetByCodeOrNameAsync(SearchDuLieuChamCongRequest request)
        //{
        //    var query = GetByCodeOrNameQuery(request);
        //    var queryResult = await Context.QueryAsync<Models.DuLieuChamCong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (queryResult.GetEnumerator().MoveNext())
        //        return (true, queryResult);

        //    return (false, default);
        //}

        public async Task<(bool isSuccess, IEnumerable<Models.DuLieuChamCong> data)> GetByParamsAsync(string? maNhanVien, DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            var query = GetByParamsQuery(maNhanVien, ngayBatDau, ngayKetThuc);
            var queryResult = await Context.QueryAsync<Models.DuLieuChamCong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        //public async Task<(bool isSuccess, IEnumerable<Models.DuLieuChamCong> data, int totalRecords)> PaginationAsync(PaginationDuLieuChamCongRequest request)
        //{
        //    // Excute
        //    var query = PaginationQuery(request);
        //    var queryResult = await Context.QueryAsync<Models.DuLieuChamCong>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (queryResult.GetEnumerator().MoveNext())
        //        return (true, queryResult, queryResult.First().TotalRecords);

        //    return (false, default, 0);
        //}

        //public async Task<(bool isSuccess, Models.DuLieuChamCong data)> UpdateAsync(Models.DuLieuChamCong airport)
        //{
        //    var query = UpdateQuery(airport);
        //    var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (result > 0)
        //        return (true, airport);

        //    return (false, airport);
        //}

        public async Task<(bool isSuccess, IEnumerable<Models.DuLieuChamCong> data)> UpdateOrInsertListRecordsAsync(IEnumerable<Models.DuLieuChamCong> request)
        {
            try
            {
                var query = UpdateOrInsertListRecordsQuery(request);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res >= 0)
                {
                    return (true, request);
                }
            }
            catch (Exception ex)
            {

                return (false, default);
            }

            return (false, default);
        }
        #endregion
    }
}

