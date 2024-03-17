using Dapper;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.DanhSachDon.Request;

namespace HRMBackend.DataAccess.DanhSachDon
{
    public partial class DanhSachDonDAO
    {
        private static (string sql, DynamicParameters param) GetByParamsQuery(SearchDanhSachDonRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":maDanhSachDon", maDanhSachDon, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":tenca", RemoveSignUnicodeString(tenCa, true), dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_DanhSachDon
                            WHERE 
                                (:tenca IS NULL OR TRANSLATE(UPPER(TENCA), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :tenca || '%')
                                AND (:maDanhSachDon IS NULL OR MADanhSachDon = :maDanhSachDon)                              
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
