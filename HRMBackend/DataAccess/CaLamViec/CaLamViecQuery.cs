using Dapper;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

namespace HRMBackend.DataAccess.CaLamViec
{
    public partial class CaLamViecDAO
    {
        private static (string sql, DynamicParameters param) GetByShiftIDQuery(int? maCaLamViec, string? tenCa)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":macalamviec", maCaLamViec, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":tenca", RemoveSignUnicodeString(tenCa, true), dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_CALAMVIEC
                            WHERE 
                                (:tenca IS NULL OR TRANSLATE(UPPER(TENCA), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :tenca || '%')
                                AND (:macalamviec IS NULL OR MACALAMVIEC = :macalamviec)                              
                            ";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetByShiftNameQuery(string? tenCa)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenca", tenCa, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_CALAMVIEC
                            WHERE 
                                (TENCA = :tenca)                              
                            ";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) CreateQuery(Models.CaLamViec model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenca", model.TenCa, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":giobatdauca", model.GioBatDauCa, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":giobatdaunghi", model.GioBatDauNghi, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":gioketthucca", model.GioKetThucCa, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":gioketthucnghi", model.GioKetThucNghi, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":maca", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // SQL component
            string query = @"INSERT INTO public.tbl_calamviec(
	                        tenca, giobatdauca, gioketthucca, giobatdaunghi, gioketthucnghi)
	                        VALUES (:tenca, :giobatdauca, :gioketthucca, :giobatdaunghi, :gioketthucnghi)
                            RETURNING macalamviec";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) UpdateQuery(Models.CaLamViec model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":macalamviec", model.MaCa, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":tenca", model.TenCa, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":giobatdauca", model.GioBatDauCa, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":gioketthucca", model.GioKetThucCa, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":giobatdaunghi", model.GioBatDauNghi, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":gioketthucnghi", model.GioKetThucNghi, dbType: DbType.Time, direction: ParameterDirection.Input);

            string query = @"UPDATE public.tbl_calamviec
	                        SET tenca=:tenca, 
                                giobatdauca=:giobatdauca, 
                                gioketthucca=:gioketthucca, 
                                giobatdaunghi=:giobatdaunghi, 
                                gioketthucnghi=:gioketthucnghi
	                        WHERE macalamviec = :macalamviec;";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) DeleteQuery(string id)
        {
            // Param component
            var param = new DynamicParameters();
            //param.Add(":id", id, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"DELETE FROM tbl_calamviec WHERE (MACALAMVIEC IN (" + id + "))";

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
