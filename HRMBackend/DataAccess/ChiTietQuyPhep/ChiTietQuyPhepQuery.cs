using Dapper;
using System.Data;

namespace HRMBackend.DataAccess.ChiTietQuyPhep
{
    public partial class ChiTietQuyPhepDAO
    {
        private static (string sql, DynamicParameters param) GetByYearQuery(int? nam)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":nam", nam, dbType: DbType.Int32, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_CHITIETQUYPHEP
                            WHERE (:nam IS NULL OR NAM = :nam)                              
                            ";

            return (query, param);
        }
    }
}
