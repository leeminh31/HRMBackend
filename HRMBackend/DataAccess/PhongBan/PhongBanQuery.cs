using Dapper;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.PhongBan.Request;
using HRMBackend.Models;

namespace HRMBackend.DataAccess.PhongBan
{
    public partial class PhongBanDAO
    {
        #region Method
        private static (string sql, DynamicParameters param) DeleteQuery(string id)
        {
            // Param component
            var param = new DynamicParameters();
            //param.Add(":id", id, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"DELETE FROM tbl_phongban WHERE (MAPHONGBAN IN ("+id+"))";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetByTenPhongBanQuery(string tenPhongBan, int? maPhongBan)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenphongban", tenPhongBan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":maphongban", maPhongBan, dbType: DbType.Int32, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_PHONGBAN
                            WHERE (TENPHONGBAN = :tenphongban) AND (:maphongban IS NULL OR MAPHONGBAN != :maphongban)";

            return (query, param);
        }
        private static (string sql, DynamicParameters param) GetByParamsQuery(SearchPhongBanRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenphongban", RemoveSignUnicodeString(request.TenPhongBan, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":truongphongban", RemoveSignUnicodeString(request.TruongPhongBan, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":thukyphongban", RemoveSignUnicodeString(request.ThuKyPhongBan, true), dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_PHONGBAN
                            WHERE (
                                (:tenphongban IS NULL OR TRANSLATE(UPPER(TENPHONGBAN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :tenphongban || '%')
                                AND (:truongphongban IS NULL OR TRANSLATE(UPPER(TRUONGPHONGBAN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :truongphongban || '%')
                                AND (:thukyphongban IS NULL OR TRANSLATE(UPPER(THUKYPHONGBAN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :thukyphongban || '%')
                            )";

            return (query, param);
        }
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
        private static (string sql, DynamicParameters param) CreateQuery(Models.PhongBan model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenphongban", model.TenPhongBan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":solanchamcong", model.SoLanChamCong, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":truongphongban", model.TruongPhongBan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":thukyphongban", model.ThuKyPhongBan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":maphongban", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // SQL component
            string query = @"INSERT INTO public.tbl_phongban(
	                        tenphongban, solanchamcong, truongphongban, thukyphongban)
	                        VALUES (:tenphongban, :solanchamcong, :truongphongban, :thukyphongban)
                            RETURNING maphongban";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) PaginationQuery(PaginationPhongBanRequest request)
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

        //private static (string sql, DynamicParameters param) GetByCodeOrNameQuery(SearchPhongBanRequest request)
        //{
        //    // Param component
        //    var param = new DynamicParameters();
        //    param.Add(":code", RemoveSignUnicodeString(request.Code, true), dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":name", RemoveSignUnicodeString(request.Name, true), dbType: DbType.String, direction: ParameterDirection.Input);

        //    // SQL component
        //    string query = @"SELECT *
        //                    FROM TBL_AIRPORT
        //                    WHERE (
        //                        (:code IS NOT NULL AND TRANSLATE(UPPER(CODE), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') = :code )
        //                        OR (:name IS NOT NULL AND TRANSLATE(UPPER(NAME), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') = :name)
        //                    )";

        //    return (query, param);
        //}

        private static (string sql, DynamicParameters param) UpdateQuery(Models.PhongBan model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenphongban", model.TenPhongBan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":solanchamcong", model.SoLanChamCong, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":truongphongban", model.TruongPhongBan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":thukyphongban", model.ThuKyPhongBan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":maphongban", model.MaPhongBan, dbType: DbType.Int32, direction: ParameterDirection.Input);
 
            string query = @"UPDATE public.tbl_phongban
	                        SET tenphongban=:tenphongban, 
                                solanchamcong=:solanchamcong, 
                                truongphongban=:truongphongban, 
                                thukyphongban=:thukyphongban
	                        WHERE maphongban=:maphongban;";
            return (query, param);
        }
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
