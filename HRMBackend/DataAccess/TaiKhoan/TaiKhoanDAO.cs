using Dapper;
using HRMBackend.DataAccess.TaiKhoan;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Extensions;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.Authentication.Request;
using HRMBackend.Resources.DTO.TaiKhoan.Request;

namespace HRMBackend.DataAccess.TaiKhoan
{
    public partial class TaiKhoanDAO : BaseDAO, ITaiKhoanDAO
    {
        #region Constructor
        public TaiKhoanDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion

        #region Method

        public async Task<(bool hasValue, IEnumerable<Models.TaiKhoan> data)> GetFilterAsync(string searchKey)
        {
            // Excute
            var query = GetFilterQuery(searchKey);
            var queryResult = await Context.QueryAsync<Models.TaiKhoan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.TaiKhoan data)> GetByIdAsync(int id)
        {
            // Excute
            var query = GetByIdQuery(id);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.TaiKhoan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }
        public async Task<(bool isSuccess, Models.TaiKhoan data)> CreateAsync(Models.TaiKhoan taikhoan)
        {
            try
            {
                var query = CreateQuery(taikhoan);
                var res = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

                // Process result
                if (res > 0)
                {
                    taikhoan.MaTaiKhoan = query.param.Get<int>("mataikhoan");
                    return (true, taikhoan);
                }
            }
            catch (Exception ex)
            {

                return (false, taikhoan);
            }

            return (false, taikhoan);
        }

        public async Task<(bool isSuccess, IEnumerable<Models.TaiKhoan> data)> GetByCodeOrNameAsync(SearchTaiKhoanRequest request)
        {
            var query = GetByCodeOrNameQuery(request);
            var queryResult = await Context.QueryAsync<Models.TaiKhoan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult.GetEnumerator().MoveNext())
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool hasValue, Models.TaiKhoan data)> GetByUsernameAsync(string username)
        {
            // Excute
            var query = GetByUsernameQuery(username);
            var queryResult = await Context.QuerySingleOrDefaultAsync<Models.TaiKhoan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }

        public async Task<(bool isValid, Models.TaiKhoan data)> ValidateCredentialsAsync(LoginRequest loginRequest)
        {
            bool flag = false;
            Models.TaiKhoan user = new();

            var result = await GetByUsernameAsync(loginRequest.TenDangNhap);
            if (result.hasValue)
            {
                flag = result.data.MatKhau.CheckingPassword(loginRequest.MatKhau);
                user = result.data;
            }

            return (flag, user);
        }

        //public async Task<(bool isSuccess, IEnumerable<Models.TaiKhoan> data, int totalRecords)> PaginationAsync(PaginationTaiKhoanRequest request)
        //{
        //    // Excute
        //    var query = PaginationQuery(request);
        //    var queryResult = await Context.QueryAsync<Models.TaiKhoan>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (queryResult.GetEnumerator().MoveNext())
        //        return (true, queryResult, queryResult.First().TotalRecords);

        //    return (false, default, 0);
        //}

        //public async Task<(bool isSuccess, Models.TaiKhoan data)> UpdateAsync(Models.TaiKhoan taikhoan)
        //{
        //    var query = UpdateQuery(taikhoan);
        //    var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (result > 0)
        //        return (true, taikhoan);

        //    return (false, taikhoan);
        //}

        //public async Task<(bool isSuccess, Models.TaiKhoan data)> ChangePasswordAsync(string maNhanVien, string password)
        //{
        //    var query = ChangePasswordQuery(maNhanVien, password);
        //    var result = await Context.ExecuteAsync(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

        //    // Process result
        //    if (result > 0)
        //        return (true, taikhoan);

        //    return (false, taikhoan);
        //}
        #endregion
    }
}
