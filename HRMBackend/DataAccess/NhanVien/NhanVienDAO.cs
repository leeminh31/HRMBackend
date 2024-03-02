using Dapper;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.NhanVien.Request;

namespace HRMBackend.DataAccess.NhanVien
{
    public partial class NhanVienDAO : BaseDAO, INhanVienDAO
    {
        #region Constructor
        public NhanVienDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion

        #region Method

        public async Task<(bool hasValue, IEnumerable<string> data)> GetAllEmployeeIdByNameAsync(string? hoTen)
        {
            // Excute
            var query = GetAllEmployeeIdByNameQuery(hoTen);
            var queryResult = await Context.QueryAsync<string>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, IEnumerable<Models.NhanVien> data)> GetFilterAsync(string searchKey)
        {
            // Excute
            var query = GetFilterQuery(searchKey);
            var queryResult = await Context.QueryAsync<Models.NhanVien>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.NhanVien data)> GetByIDAsync(string maNhanVien)
        {
            // Excute
            var query = GetByIdQuery(maNhanVien);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.NhanVien>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.NhanVien data)> GetByIdVanTayAsync(int? idVanTay, string? maNhanVien)
        {
            // Excute
            var query = GetByIdVanTayQuery(idVanTay, maNhanVien);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.NhanVien>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }
        public async Task<(bool isSuccess, Models.NhanVien data)> CreateAsync(Models.NhanVien nhanvien)
        {
            try
            {
                var query = CreateQuery(nhanvien);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    //nhanvien.MaNhanVien = query.param.Get<string>("idOutput");
                    return (true, nhanvien);
                }
            }
            catch (Exception ex)
            {

                return (false, nhanvien);
            }

            return (false, nhanvien);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.NhanVien> data)> UpdateOrInsertListRecordsAsync(IEnumerable<Models.NhanVien> request, IEnumerable<Models.HopDong> request2)
        {
            try
            {
                var query = UpdateOrInsertListRecordsQuery(request, request2);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    //nhanvien.MaNhanVien = query.param.Get<string>("idOutput");
                    return (true,request);
                }
            }
            catch (Exception ex)
            {

                return (false, default);
            }

            return (false, default);
        }

        //public async Task<(bool isSuccess, IEnumerable<Models.NhanVien> data)> GetByCodeOrNameAsync(SearchNhanVienRequest request)
        //{
        //    var query = GetByCodeOrNameQuery(request);
        //    var queryResult = await Context.QueryAsync<Models.NhanVien>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (queryResult.GetEnumerator().MoveNext())
        //        return (true, queryResult);

        //    return (false, default);
        //}

        public async Task<(bool isSuccess, IEnumerable<Models.NhanVien> data)> GetByParamsAsync(string? maNhanVien, int? maPhongBan, int? idVanTay, string? chucVu, string? hoTen)
        {
            var query = GetByParamsQuery(maNhanVien, maPhongBan, idVanTay, chucVu, hoTen);
            var queryResult = await Context.QueryAsync<Models.NhanVien>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        //public async Task<(bool isSuccess, IEnumerable<Models.NhanVien> data, int totalRecords)> PaginationAsync(PaginationNhanVienRequest request)
        //{
        //    // Excute
        //    var query = PaginationQuery(request);
        //    var queryResult = await Context.QueryAsync<Models.NhanVien>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (queryResult.GetEnumerator().MoveNext())
        //        return (true, queryResult, queryResult.First().TotalRecords);

        //    return (false, default, 0);
        //}

        public async Task<(bool isSuccess, Models.NhanVien data)> UpdateAsync(Models.NhanVien airport)
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
