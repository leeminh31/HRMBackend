using Dapper;
using HRMBackend.Resources.DTO.DonPhep.Request;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.DanhSachDon.Request;

namespace HRMBackend.DataAccess.DonPhep
{
    public partial class DonPhepDAO
    {
        private static (string sql, DynamicParameters param) GetByParamsQuery(SearchDanhSachDonRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":ngaylamviec", request.NgayLamViec, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaytaodon", request.NgayTao, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":trangthai", request.TrangThai, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_DONPHEP
                            WHERE 
                                (:ngaylamviec IS NULL OR ngaylamviec = :ngaylamviec)
                                AND (:ngaytaodon IS NULL OR ngaytaodon = :ngaytaodon)
                                AND (:trangthai IS NULL OR trangthai = :trangthai)
                            ";

            return (query, param);
        }

        public static string RemoveSignUnicodeString(string s, bool toUpper = false)
        {
            if (!string.IsNullOrEmpty(s?.Trim()))
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = s.Normalize(NormalizationForm.FormD);
                var res = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                return toUpper ? res?.ToUpper() : res;
            }
            else
            {
                return s;
            }
        }
    }
}
