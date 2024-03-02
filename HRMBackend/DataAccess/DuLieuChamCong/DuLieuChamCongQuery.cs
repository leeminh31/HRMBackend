using Dapper;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

namespace HRMBackend.DataAccess.DuLieuChamCong
{
    public partial class DuLieuChamCongDAO
    {
        #region Method
        private static (string sql, DynamicParameters param) UpdateOrInsertListRecordsQuery(IEnumerable<Models.DuLieuChamCong> requests)
        {
            // Param component
            var param = new DynamicParameters();

            StringBuilder valueQuery = new StringBuilder();
            for (int i = 0; i < requests.Count(); i++)
            {
                if (i != requests.Count() - 1)
                    valueQuery.AppendFormat("('{0}', '{1}', {2}, '{3}'),"
                        , requests.ElementAt(i).MaNhanVien,
                        requests.ElementAt(i).NgayChamCong.ToString("yyyy-MM-dd"),
                        requests.ElementAt(i).LanChamCong,
                        requests.ElementAt(i).GioChamCong.ToString("HH:mm:ss"));
                else
                {
                    valueQuery.AppendFormat("('{0}', '{1}', {2}, '{3}')"
                        , requests.ElementAt(i).MaNhanVien,
                        requests.ElementAt(i).NgayChamCong.ToString("yyyy-MM-dd"),
                        requests.ElementAt(i).LanChamCong,
                        requests.ElementAt(i).GioChamCong.ToString("HH:mm:ss"));
                }
            }

            string query = @"INSERT INTO public.tbl_dulieuchamcong(
	                        manhanvien, ngaychamcong, lanchamcong, giochamcong)
	                        VALUES " +valueQuery +";";
            return (query, param);
        }
        private static (string sql, DynamicParameters param) GetAllContractQuery()
        {
            // Param component
            var param = new DynamicParameters();
            string query = @"SELECT * FROM tbl_DuLieuChamCong";
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
        private static (string sql, DynamicParameters param) GetByIdQuery(string tenDuLieuChamCong)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenhopdong", tenDuLieuChamCong, dbType: DbType.String, direction: ParameterDirection.Input);
            string query = @"SELECT * FROM TBL_HOPDONG WHERE TENHOPDONG = :tenhopdong";
            return (query, param);
        }
        //private static (string sql, DynamicParameters param) CreateQuery(Models.DuLieuChamCong model)
        //{
        //    // Param component
        //    var param = new DynamicParameters();
        //    param.Add(":tenhopdong", model.TenDuLieuChamCong, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add("ngaybatdauhopdong", model.NgayBatDauDuLieuChamCong, dbType: DbType.Date, direction: ParameterDirection.Input);
        //    param.Add(":ngayketthuchopdong", model.NgayKetThucDuLieuChamCong, dbType: DbType.Date, direction: ParameterDirection.Input);
        //    param.Add(":loaihopdong", model.loaiDuLieuChamCong, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":tilehuongluong", model.TiLeHuongLuong, dbType: DbType.Double, direction: ParameterDirection.Input);
        //    param.Add(":giolamviec", model.GioLamViec, dbType: DbType.Double, direction: ParameterDirection.Input);

        //    // SQL component
        //    string query = @"INSERT INTO public.tbl_hopdong(
	       //                 tenhopdong, manhanvien, ngaybatdauhopdong, ngayketthuchopdong, loaihopdong, tilehuongluong, giolamviec)
	       //                 VALUES (:tenhopdong, :manhanvien, :ngaybatdauhopdong, :ngayketthuchopdong, :loaihopdong, :tilehuongluong, :giolamviec);";
        //    return (query, param);
        //}

        private static (string sql, DynamicParameters param) PaginationQuery(PaginationDuLieuChamCongRequest request)
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

        private static (string sql, DynamicParameters param) GetByParamsQuery(string? maNhanVien, DateTime? ngayLamViec)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", RemoveSignUnicodeString(maNhanVien, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaylamviec", ngayLamViec, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_DULIEUCHAMCONG
                            WHERE (
                                (:manhanvien IS NULL OR TRANSLATE(UPPER(MANHANVIEN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :manhanvien || '%')
                                AND (:ngaylamviec IS NULL OR ngaychamcong = :ngaylamviec)
                            )";

            return (query, param);
        }

        //private static (string sql, DynamicParameters param) UpdateQuery(Models.DuLieuChamCong model)
        //{
        //    // Param component
        //    var param = new DynamicParameters();
        //    param.Add(":tenhopdong", model.TenDuLieuChamCong, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":ngaybatdauhopdong", model.NgayBatDauDuLieuChamCong, dbType: DbType.Date, direction: ParameterDirection.Input);
        //    param.Add(":ngayketthuchopdong", model.NgayKetThucDuLieuChamCong, dbType: DbType.Date, direction: ParameterDirection.Input);
        //    param.Add(":loaihopdong", model.loaiDuLieuChamCong, dbType: DbType.String, direction: ParameterDirection.Input);
        //    param.Add(":tilehuongluong", model.TiLeHuongLuong, dbType: DbType.Double, direction: ParameterDirection.Input);
        //    param.Add(":giolamviec", model.GioLamViec, dbType: DbType.Double, direction: ParameterDirection.Input);

        //    string query = @"UPDATE public.tbl_hopdong
        //                 SET ngaybatdauhopdong= :ngaybatdauhopdong, 
        //                        ngayketthuchopdong= :ngayketthuchopdong, 
        //                        loaihopdong= :loaihopdong, 
        //                        tilehuongluong= :tilehuongluong, 
        //                        giolamviec= :giolamviec
        //                 WHERE tenhopdong = :tenhopdong
        //                    AND manhanvien = :manhanvien;";
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
