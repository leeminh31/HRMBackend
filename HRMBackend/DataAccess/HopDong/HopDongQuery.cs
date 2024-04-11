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
        private static (string sql, DynamicParameters param) GetByIdQuery(string tenHopDong)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenhopdong", tenHopDong, dbType: DbType.String, direction: ParameterDirection.Input);
            string query = @"SELECT * FROM TBL_HOPDONG WHERE TENHOPDONG = :tenhopdong";
            return (query, param);
        }
        private static (string sql, DynamicParameters param) CreateQuery(Models.HopDong model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenhopdong", model.TenHopDong, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("ngaybatdauhopdong", model.NgayBatDauHopDong, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngayketthuchopdong", model.NgayKetThucHopDong, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":loaihopdong", model.loaiHopDong, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":tilehuongluong", model.TiLeHuongLuong, dbType: DbType.Double, direction: ParameterDirection.Input);
            param.Add(":giolamviec", model.GioLamViec, dbType: DbType.Double, direction: ParameterDirection.Input);

            // SQL component
            string query = @"INSERT INTO public.tbl_hopdong(
	                        tenhopdong, manhanvien, ngaybatdauhopdong, ngayketthuchopdong, loaihopdong, tilehuongluong, giolamviec)
	                        VALUES (:tenhopdong, :manhanvien, :ngaybatdauhopdong, :ngayketthuchopdong, :loaihopdong, :tilehuongluong, :giolamviec);";
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

        private static (string sql, DynamicParameters param) GetByParamsQuery(string? tenHopDong, string? loaiHopDong)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tenhopdong", RemoveSignUnicodeString(tenHopDong, true), dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":loaihopdong", loaiHopDong, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_HOPDONG
                            WHERE (
                                (:tenhopdong IS NULL OR TRANSLATE(UPPER(TENHOPDONG), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :tenhopdong || '%')
                                AND (:loaihopdong IS NULL OR loaihopdong = :loaihopdong)
                            )";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetContractByYearQuery(string? maNhanVien, int? nam)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", maNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nam", nam, dbType: DbType.Int32, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_HOPDONG
                            WHERE (
                                ( :manhanvien IS NULL OR manhanvien = :manhanvien)
                                AND (:nam IS NULL OR EXTRACT(YEAR from ngaybatdauhopdong) = :nam OR EXTRACT(YEAR from ngayketthuchopdong) = :nam OR (EXTRACT(YEAR from ngaybatdauhopdong) < :nam AND EXTRACT(YEAR from ngayketthuchopdong) > :nam))
                            )
                            ORDER BY ngaybatdauhopdong ASC";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetContractByTimeRangeAndIDQuery(DateTime? ngayKetThuc, DateTime? ngayBatDau)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":ngayketthuc", ngayKetThuc, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaybatdau", ngayBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_HOPDONG
                            WHERE (
                                ngaybatdauhopdong <= :ngayketthuc
                                AND ngayketthuchopdong >=:ngayBatDau
                            )
                            ORDER BY ngaybatdauhopdong ASC";

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
            param.Add(":loaihopdong", model.loaiHopDong, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":tilehuongluong", model.TiLeHuongLuong, dbType: DbType.Double, direction: ParameterDirection.Input);
            param.Add(":giolamviec", model.GioLamViec, dbType: DbType.Double, direction: ParameterDirection.Input);

            string query = @"UPDATE public.tbl_hopdong
	                        SET ngaybatdauhopdong= :ngaybatdauhopdong, 
                                ngayketthuchopdong= :ngayketthuchopdong, 
                                loaihopdong= :loaihopdong, 
                                tilehuongluong= :tilehuongluong, 
                                giolamviec= :giolamviec
	                        WHERE tenhopdong = :tenhopdong
                            AND manhanvien = :manhanvien;";
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
