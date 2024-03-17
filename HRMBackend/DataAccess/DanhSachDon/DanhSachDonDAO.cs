using Dapper;
using HRMBackend.DataAccess.DanhSachDon;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.DanhSachDon.Response;

namespace HRMBackend.DataAccess.DanhSachDon
{
    public partial class DanhSachDonDAO : BaseDAO, IDanhSachDonDAO
    {
        #region Constructor
        public DanhSachDonDAO(IUnitOfWorkContext unitOfWorkContext)
        {
            this.Context = unitOfWorkContext.Context;
            this.Transaction = unitOfWorkContext.Transaction;
        }
        #endregion
        public async Task<(bool isSuccess, DanhSachDonResponse data)> GetByParamsAsync(SearchDanhSachDonRequest request)
        {
            var query = GetByParamsQuery(request);
            var queryResult = await Context.QuerySingleOrDefaultAsync<DanhSachDonResponse>(query.sql, query.param, Transaction, Constant.TimeOutCancelDAO);

            // Process result
            if (queryResult != null)
                return (true, queryResult);

            return (false, default);
        }
    }
}
