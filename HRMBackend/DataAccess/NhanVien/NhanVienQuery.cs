using Dapper;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.NhanVien.Request;
using Org.BouncyCastle.Asn1.Ocsp;

namespace HRMBackend.DataAccess.NhanVien
{
    public partial class NhanVienDAO
    {
        #region Method
        private static (string sql, DynamicParameters param) UpdateOrInsertListRecordsQuery(IEnumerable<Models.NhanVien> requests, IEnumerable<Models.HopDong> request2)
        {
            // Param component
            var param = new DynamicParameters();

            StringBuilder contractQuery = new StringBuilder();
            for (int i = 0; i<request2.Count(); i++)
            {
                if(i!= request2.Count()-1)
                    contractQuery.AppendFormat("('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}),"
                        , request2.ElementAt(i).TenHopDong,
                        request2.ElementAt(i).MaNhanVien,
                        request2.ElementAt(i).NgayBatDauHopDong.ToString("yyyy-MM-dd"),
                        request2.ElementAt(i).NgayKetThucHopDong.ToString("yyyy-MM-dd"),
                        request2.ElementAt(i).loaiHopDong,
                        request2.ElementAt(i).TiLeHuongLuong,
                        request2.ElementAt(i).GioLamViec);
                else
                {
                    contractQuery.AppendFormat("('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6})"
                        , request2.ElementAt(i).TenHopDong,
                        request2.ElementAt(i).MaNhanVien,
                        request2.ElementAt(i).NgayBatDauHopDong.ToString("yyyy-MM-dd"),
                        request2.ElementAt(i).NgayKetThucHopDong.ToString("yyyy-MM-dd"),
                        request2.ElementAt(i).loaiHopDong,
                        request2.ElementAt(i).TiLeHuongLuong,
                        request2.ElementAt(i).GioLamViec);
                }
            } 

            StringBuilder valueQuery = new StringBuilder();
            for (int i = 0; i< requests.Count(); i++)
            {
                if (i != requests.Count() -1)
                    valueQuery.AppendFormat("('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', {15}, 1),"
                        ,requests.ElementAt(i).MaNhanVien,
                        requests.ElementAt(i).MaPhongBan,
                        requests.ElementAt(i).HoTen, 
                        requests.ElementAt(i).ChucVu, 
                        requests.ElementAt(i).Mail, 
                        requests.ElementAt(i).NgaySinh.ToString("yyyy-MM-dd"), 
                        requests.ElementAt(i).SoDienThoai, 
                        requests.ElementAt(i).SoCCCD, 
                        requests.ElementAt(i).NgayCap.ToString("yyyy-MM-dd"), 
                        requests.ElementAt(i).QueQuan, 
                        requests.ElementAt(i).NoiOHienTai, 
                        requests.ElementAt(i).NguoiThanLienHe, 
                        requests.ElementAt(i).SoDienThoaiNguoiLienHe, 
                        requests.ElementAt(i).STKNganHang, 
                        requests.ElementAt(i).NganHang, 
                        requests.ElementAt(i).IDVanTay);
                else
                {
                    valueQuery.AppendFormat("('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', {15}, 1)"
                        , requests.ElementAt(i).MaNhanVien,
                        requests.ElementAt(i).MaPhongBan,
                        requests.ElementAt(i).HoTen,
                        requests.ElementAt(i).ChucVu,
                        requests.ElementAt(i).Mail,
                        requests.ElementAt(i).NgaySinh.ToString("yyyy-MM-dd"),
                        requests.ElementAt(i).SoDienThoai,
                        requests.ElementAt(i).SoCCCD,
                        requests.ElementAt(i).NgayCap.ToString("yyyy-MM-dd"),
                        requests.ElementAt(i).QueQuan,
                        requests.ElementAt(i).NoiOHienTai,
                        requests.ElementAt(i).NguoiThanLienHe,
                        requests.ElementAt(i).SoDienThoaiNguoiLienHe,
                        requests.ElementAt(i).STKNganHang,
                        requests.ElementAt(i).NganHang,
                        requests.ElementAt(i).IDVanTay);
                }
            }

            string query = @"INSERT INTO public.tbl_nhanvien(
	                        manhanvien, maphongban, hoten, chucvu, mail, ngaysinh, sodienthoai, socccd, ngaycap, quequan, noiohientai, nguoithanlienhe, sodienthoainguoilienhe, stknganhang, nganhang, idvantay, macalamviec)
	                        VALUES" + valueQuery +
                            @"
	                        ON CONFLICT(manhanvien) 
	                        DO UPDATE SET
	                          maphongban = EXCLUDED.maphongban,
	                          hoten = EXCLUDED.hoten,
	                          chucvu = EXCLUDED.chucvu,
	                          mail = EXCLUDED.mail,
	                          ngaysinh = EXCLUDED.ngaysinh,
	                          sodienthoai = EXCLUDED.sodienthoai,
	                          ngaycap = EXCLUDED.ngaycap,
	                          quequan = EXCLUDED.quequan,
	                          noiohientai = EXCLUDED.noiohientai,
	                          nguoithanlienhe = EXCLUDED.nguoithanlienhe,
	                          sodienthoainguoilienhe = EXCLUDED.sodienthoainguoilienhe,
	                          stknganhang = EXCLUDED.stknganhang,
	                          nganhang = EXCLUDED.nganhang,
	                          idvantay = EXCLUDED.idvantay,
	                          socccd = EXCLUDED.socccd;

                            INSERT INTO public.tbl_hopdong(
	                        tenhopdong, manhanvien, ngaybatdauhopdong, ngayketthuchopdong, loaihopdong, tilehuongluong, giolamviec)
	                        VALUES" + contractQuery+
	                        @"
                            ON CONFLICT(tenhopdong) 
	                        DO UPDATE SET
	                          manhanvien = EXCLUDED.manhanvien,
	                          ngayketthuchopdong = EXCLUDED.ngayketthuchopdong,
	                          loaihopdong = EXCLUDED.loaihopdong,
	                          tilehuongluong = EXCLUDED.tilehuongluong,
	                          giolamviec = EXCLUDED.giolamviec,
	                          ngaybatdauhopdong = EXCLUDED.ngaybatdauhopdong;
                            ";
            return (query, param);
        }
        private static (string sql, DynamicParameters param) GetAllEmployeeIdByNameQuery(string? hoTen)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":hoten", RemoveSignUnicodeString(hoTen, true), dbType: DbType.String, direction: ParameterDirection.Input);
            string query = @"SELECT manhanvien
                            FROM TBL_NHANVIEN
                            WHERE (:hoten IS NULL OR TRANSLATE(UPPER(HOTEN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :hoten || '%')
                            ";
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

        private static (string sql, DynamicParameters param) GetByIdVanTayQuery(int? idVanTay, string? maNhanVien)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":idvantay", idVanTay, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":manhanvien", maNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            string query = @"SELECT * FROM TBL_NHANVIEN WHERE (:idvantay IS NULL OR IDVANTAY = :idvantay) AND(:manhanvien IS NULL OR MANHANVIEN != :manhanvien)";
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
	        manhanvien, maphongban, hoten, chucvu, mail, ngaysinh, sodienthoai, socccd, ngaycap, quequan, noiohientai, nguoithanlienhe, sodienthoainguoilienhe, stknganhang, nganhang, idvantay, macalamviec)
	        VALUES (:manhanvien, :maphongban, :hoten, :chucvu, :mail, :ngaysinh, :sodienthoai, :socccd, :ngaycap, :quequan, :noiohientai, :nguoithanlienhe, :sodienthoainguoilienhe, :stknganhang, :nganhang, :idvantay, 1);";
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

        private static (string sql, DynamicParameters param) GetByParamsQuery(string? maNhanVien, int? maPhongBan, int? idVanTay, string? chucVu, string? hoTen)
        {
            var test = RemoveSignUnicodeString(hoTen, true);
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", RemoveSignUnicodeString(maNhanVien, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":maphongban", maPhongBan, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":idvantay", idVanTay, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":chucvu", RemoveSignUnicodeString(chucVu, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":hoten", RemoveSignUnicodeString(hoTen, true), dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_NHANVIEN
                            WHERE (
                                (:manhanvien IS NULL OR TRANSLATE(UPPER(MANHANVIEN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :manhanvien || '%')
                                AND (:maphongban IS NULL OR maphongban = :maphongban)
                                AND (:chucvu IS NULL OR TRANSLATE(UPPER(CHUCVU), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :chucvu || '%')
                                AND (:hoten IS NULL OR TRANSLATE(UPPER(HOTEN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :hoten || '%')
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

        private static (string sql, DynamicParameters param) UpdateShiftIDQuery(IEnumerable<Models.NhanVien> model, string? employeeId)
        {
            // Param component
            var param = new DynamicParameters();

            StringBuilder valueQuery = new StringBuilder();
            for (int i = 0; i < model.Count(); i++)
            {
                valueQuery.AppendFormat("WHEN MANHANVIEN = '{0}' THEN {1}\n"
                    , model.ElementAt(i).MaNhanVien,
                    model.ElementAt(i).MaCa);
                
            }

            string query = @"UPDATE tbl_nhanvien
                            SET macalamviec = CASE 
                            " + valueQuery + @"
                                ELSE macalamviec
                            END
                            WHERE manhanvien IN (" + employeeId+ @");";
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
