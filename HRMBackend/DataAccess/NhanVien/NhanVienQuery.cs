using Dapper;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.NhanVien.Request;

namespace HRMBackend.DataAccess.NhanVien
{
    public partial class NhanVienDAO
    {
        #region Method
        private static (string sql, DynamicParameters param) GetAllEmployeeQuery()
        {
            // Param component
            var param = new DynamicParameters();
            string query = @"SELECT * FROM tbl_NhanVien";
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
        private static (string sql, DynamicParameters param) GetByIdQuery(string maNhanVien)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", maNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            string query = @"SELECT * FROM TBL_NHANVIEN WHERE MANHANVIEN = :manhanvien";
            return (query, param);
        }
        private static (string sql, DynamicParameters param) CreateQuery(Models.NhanVien model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", model.MaNhanVien,dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":maphongban", model.MaPhongBan, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":hoten", model.HoTen, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":chucvu", model.ChucVu, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":mail", model.Mail, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaysinh", model.NgaySinh, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":sodienthoai", model.SoDienThoai, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":socccd", model.SoCCCD, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaycap", model.NgayCap, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":quequan", model.QueQuan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":noiohientai", model.NoiOHienTai, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nguoithanlienhe", model.NguoiThanLienHe, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":sodienthoainguoilienhe", model.SoDienThoaiNguoiLienHe, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":stknganhang", model.STKNganHang, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nganhang", model.NganHang, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":idvantay", model.IDVanTay, dbType: DbType.Int32, direction: ParameterDirection.Input);

            // SQL component
            string query = @"INSERT INTO public.tbl_nhanvien(
	        manhanvien, maphongban, hoten, chucvu, mail, ngaysinh, sodienthoai, socccd, ngaycap, quequan, noiohientai, nguoithanlienhe, sodienthoainguoilienhe, stknganhang, nganhang, idvantay)
	        VALUES (:manhanvien, :maphongban, :hoten, :chucvu, :mail, :ngaysinh, :sodienthoai, :socccd, :ngaycap, :quequan, :noiohientai, :nguoithanlienhe, :sodienthoainguoilienhe, :stknganhang, :nganhang, :idvantay);";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) PaginationQuery(PaginationNhanVienRequest request)
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

        //private static (string sql, DynamicParameters param) GetByCodeOrNameQuery(SearchNhanVienRequest request)
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

        private static (string sql, DynamicParameters param) GetByParamsQuery(SearchNhanVienRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", request.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":maphongban", request.MaPhongBan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":idvantay", request.IDVanTay, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":chucvu", request.ChucVu, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":hoten", request.HoTen, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_NHANVIEN
                            WHERE (
                                (:manhanvien IS NULL OR TRANSLATE(UPPER(MANHANVIEN), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') LIKE '%' || :manhanvien || '%')
                                AND (:maphongban IS NULL OR TRANSLATE(UPPER(MAPHONGBAN), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') LIKE '%' || :maphongban || '%')
                                AND (:chucvu IS NULL OR TRANSLATE(UPPER(CHUCVU), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') LIKE '%' || :chucvu || '%')
                                AND (:hoten IS NULL OR TRANSLATE(UPPER(HOTEN), 'ÁÀẢẠÃẤẦẨẬẪĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỔỘỖÔỐỒỔỘỖÚÙỦỤŨỨỪỬỰỮÝỲỶỴỸáàảạãấầẩậẫđéèẻẹẽếềểệễíìỉịĩóòỏọõốồổộỗôốồổộỗúùủụũứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY') LIKE '%' || :hoten || '%')
                                AND (:idvantay IS NULL OR IDVANTAY = :idvantay)
                            )";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) UpdateQuery(Models.NhanVien model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":maphongban", model.MaPhongBan, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":hoten", model.HoTen, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":chucvu", model.ChucVu, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":mail", model.Mail, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaysinh", model.NgaySinh, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":sodienthoai", model.SoDienThoai, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":socccd", model.SoCCCD, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaycap", model.NgayCap, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":quequan", model.QueQuan, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":noiohientai", model.NoiOHienTai, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nguoithanlienhe", model.NguoiThanLienHe, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":sodienthoainguoilienhe", model.SoDienThoaiNguoiLienHe, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":stknganhang", model.STKNganHang, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nganhang", model.NganHang, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":idvantay", model.IDVanTay, dbType: DbType.Int32, direction: ParameterDirection.Input);

            string query = @"UPDATE tbl_nhanvien
                        SET maphongban = :maphongban,
                            hoten = :hoten,
                            chucvu = :chucvu,
                            mail = :mail,
                            ngaysinh = :ngaysinh,
                            sodienthoai = :sodienthoai,
                            socccd = :socccd,
                            ngaycap = :ngaycap,
                            quequan = :quequan,
                            noiohientai = :noiohientai,
                            nguoithanlienhe = :nguoithanlienhe,
                            sodienthoainguoilienhe = :sodienthoainguoilienhe,
                            stknganhang = :stknganhang, 
                            nganhang = :nganhang,
                            idvantay = :idvantay
                        WHERE manhanvien = :manhanvien";
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
