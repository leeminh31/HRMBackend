using Dapper;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.TaiKhoan.Request;

namespace HRMBackend.DataAccess.TaiKhoan
{
    public partial class TaiKhoanDAO
    {
        #region Method
        private static (string sql, DynamicParameters param) GetFilterQuery(string searchKey)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":searchKey", searchKey, dbType: DbType.String, direction: ParameterDirection.Input);
            string query = @"SELECT * FROM TBL_AIRPORT WHERE STATUS = 1 AND (:searchKey IS NULL OR UPPER(NAME) LIKE '%' || :searchKey || '%')";
            return (query, param);
        }
        private static (string sql, DynamicParameters param) GetByIdQuery(int id)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            string query = @"SELECT * FROM TBL_AIRPORT WHERE STATUS = 1 AND ID = :id";
            return (query, param);
        }
        private static (string sql, DynamicParameters param) CreateQuery(Models.TaiKhoan model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":phanquyen", model.PhanQuyen, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":tendangnhap", model.TenDangNhap, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":matkhau", model.MatKhau, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":mataikhoan", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // SQL component
            string query = @"INSERT INTO public.tbl_taikhoan(
	        manhanvien, phanquyen, tendangnhap, matkhau)
	        VALUES (?, ?, ?, ?);
            RETURNING mataikhoan INTO :mataikhoan";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) PaginationQuery(PaginationTaiKhoanRequest request)
        {
            // Process data
            int pageBegin = (request.Page - 1) * request.PageSize + 1;
            int pageEnd = (request.Page - 1) * request.PageSize + request.PageSize;

            // Param component
            var param = new DynamicParameters();
            param.Add(":code", RemoveSignUnicodeString(request.Code, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":name", RemoveSignUnicodeString(request.Name, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":pageBegin", pageBegin, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":pageEnd", pageEnd, dbType: DbType.Int32, direction: ParameterDirection.Input);
            string orderBy = "ASC";
            if (!string.IsNullOrEmpty(request.Orderby))
            {
                orderBy = request.Orderby.ToUpper();
            }

            // SQL component
            string query = @"SELECT * FROM (
                                SELECT COUNT(*) OVER () ""TOTAL_ELEMENTS"", ROWNUM STT, list.*
                                FROM (
                                    SELECT *
                                    FROM TBL_AIRPORT
                                    WHERE (
                                        (:code IS NULL OR TRANSLATE(UPPER(CODE), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') LIKE '%' || :code || '%')
                                        AND (:name IS NULL OR TRANSLATE(UPPER(NAME), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') LIKE '%' || :name || '%')
                                    )
                                    ORDER BY nlssort(CODE,'NLS_SORT = VIETNAMESE') " + orderBy + @"
                                    ) list
                                )
                                WHERE STT BETWEEN :pageBegin AND :pageEnd";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetByCodeOrNameQuery(SearchTaiKhoanRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":code", RemoveSignUnicodeString(request.Code, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":name", RemoveSignUnicodeString(request.Name, true), dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_AIRPORT
                            WHERE (
                                (:code IS NOT NULL AND TRANSLATE(UPPER(CODE), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') = :code )
                                OR (:name IS NOT NULL AND TRANSLATE(UPPER(NAME), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') = :name)
                            )";

            return (query, param);
        }

        //private static (string sql, DynamicParameters param) UpdateQuery(Models.TaiKhoan model)
        //{
        //    // Param component
        //    var param = new DynamicParameters();
        //    param.Add(":id", model.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
        //    param.Add(":code", model.Code, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":name", model.Name, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":url", model.Url, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":status", model.Status, dbType: DbType.Int32, direction: ParameterDirection.Input);
        //    param.Add(":description", model.Description, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":updateDateUtc", model.UpdatedDateUtc, dbType: DbType.DateTime, direction: ParameterDirection.Input);
        //    string query = @"UPDATE TBL_AIRPORT
        //                SET CODE = :code,
        //                    NAME = :name,
        //                    URL = :url,
        //                    STATUS = :status,
        //                    DESCRIPTION = :description,
        //                    UPDATED_DATE_UTC = :updateDateUtc
        //                WHERE ID = :id";
        //    return (query, param);
        //}
        #endregion

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
