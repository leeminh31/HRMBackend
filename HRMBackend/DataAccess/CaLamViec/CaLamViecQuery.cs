using Dapper;
using System.Data;

namespace HRMBackend.DataAccess.CaLamViec
{
    public partial class CaLamViecDAO
    {
        private static (string sql, DynamicParameters param) GetByShiftIDQuery(int? maCaLamViec)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":macalamviec", maCaLamViec, dbType: DbType.Int32, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_CALAMVIEC
                            WHERE 
                                (:macalamviec IS NULL OR MACALAMVIEC = :macalamviec)                              
                            ";

            return (query, param);
        }
    }
}
