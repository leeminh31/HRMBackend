using Dapper;
using HRMBackend.Resources.DTO.HopDong.Request;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

namespace HRMBackend.DataAccess.HopDong
{
    public partial class HopDongDAO
    {
        #region Method
        private static (string sql, DynamicParameters param) GetAllContractQuery()
        {
            // Param component
            var param = new DynamicParameters();
            string query = @"SELECT * FROM tbl_HopDong";
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
        private static (string sql, DynamicParameters param) GetByIdQuery(string maHopDong)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", maHopDong, dbType: DbType.String, direction: ParameterDirection.Input);
            string query = @"SELECT * FROM TBL_NHANVIEN WHERE MANHANVIEN = :manhanvien";
            return (query, param);
        }
        private static (string sql, DynamicParameters param) CreateQuery(Models.HopDong model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenhopdong", model.TenHopDong, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaybatdauhopdong", model.NgayBatDauHopDong, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngayketthuchopdong", model.NgayKetThucHopDong, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":loaihopdong", model.LoaiHopDong, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":tylehuongluong", model.TyLeHuongLuong, dbType: DbType.Double, direction: ParameterDirection.Input);
            param.Add(":giolamviec", model.GioLamViec, dbType: DbType.Double, direction: ParameterDirection.Input);
            param.Add(":congchuan", model.CongChuan, dbType: DbType.Double, direction: ParameterDirection.Input);

            // SQL component
            string query = @"INSERT INTO public.tbl_hopdong(
	        manhanvien, tenhopdong, ngaybatdauhopdong, ngayketthuchopdong, loaihopdong, tylehuongluong, giolamviec, congchuan)
	        VALUES (:manhanvien, :tenhopdong, :ngaybatdauhopdong, :ngayketthuchopdong, :loaihopdong, :tylehuongluong, :giolamviec, :congchuan);";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) PaginationQuery(PaginationHopDongRequest request)
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

        //private static (string sql, DynamicParameters param) GetByCodeOrNameQuery(SearchHopDongRequest request)
        //{
        //    Param component
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

        private static (string sql, DynamicParameters param) GetByParamsQuery(string? tenHopDong, string? tenNhanVien, string? loaiHopDong)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenhopdong", RemoveSignUnicodeString(tenHopDong, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":tennhanvien", RemoveSignUnicodeString(tenNhanVien, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":loaihopdong", RemoveSignUnicodeString(loaiHopDong, true), dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_NHANVIEN
                            WHERE (
                                (:manhanvien IS NULL OR TRANSLATE(UPPER(MANHANVIEN), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') LIKE '%' || :manhanvien || '%')
                                AND (:maphongban IS NULL OR maphongban = :maphongban)
                                AND (:chucvu IS NULL OR TRANSLATE(UPPER(CHUCVU), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') LIKE '%' || :chucvu || '%')
                                AND (:hoten IS NULL OR TRANSLATE(UPPER(HOTEN), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') LIKE '%' || :hoten || '%')
                                AND (:idvantay IS NULL OR IDVANTAY = :idvantay)
                            )";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) UpdateQuery(Models.HopDong model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenhopdong", model.TenHopDong, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaybatdauhopdong", model.NgayBatDauHopDong, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngayketthuchopdong", model.NgayKetThucHopDong, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":loaihopdong", model.LoaiHopDong, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":tylehuongluong", model.TyLeHuongLuong, dbType: DbType.Double, direction: ParameterDirection.Input);
            param.Add(":giolamviec", model.GioLamViec, dbType: DbType.Double, direction: ParameterDirection.Input);
            param.Add(":congchuan", model.CongChuan, dbType: DbType.Double, direction: ParameterDirection.Input);

            string query = @"UPDATE tbl_hopdong
                        SET manhanvien = :manhanvien,
                            ngaybatdauhopdong = :ngaybatdauhopdong,
                            ngayketthuchopdong = :ngayketthuchopdong,
                            loaihopdong = :loaihopdong,
                            tylehuongluong = :tylehuongluong,
                            giolamviec = :giolamviec,
                            congchuan = :congchuan,
                        WHERE tenhopdong = :tenhopdong";
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
