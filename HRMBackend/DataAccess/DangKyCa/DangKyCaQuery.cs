using Dapper;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.DangKyCa.Request;
using System.Drawing;

namespace HRMBackend.DataAccess.DangKyCa
{
    public partial class DangKyCaDAO
    {
        private static (string sql, DynamicParameters param) GetByParamsQuery(SearchDangKyCaRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", request.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaytao", request.NgayTao, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":calamviecmoi", request.CaLamViecMoi, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_DANGKYCA
                            WHERE 
                                (:manhanvien IS NULL OR TRANSLATE(UPPER(MANHANVIEN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :manhanvien || '%')
                                AND (:ngaytao IS NULL OR NGAYTAO = :ngaytao)                              
                                AND (:calamviecmoi IS NULL OR CALAMVIECMOI = :calamviecmoi)
                            ";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetByEmployeeIDQuery(string? maNhanVien, DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", maNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngayketthuc", ngayKetThuc, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_DANGKYCA
                            WHERE 
                                (MANHANVIEN = :manhanvien)
                                AND (NGAYBATDAUCAMOI <= :ngayketthuc)
                            ORDER BY NGAYBATDAUCAMOI ASC
                            ";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) ApproveShiftRequestQuery(string maDonDangKyCa, string nguoiDuyet)
        {
            // Param component
            var param = new DynamicParameters();
            string query = @"
                UPDATE public.tbl_dangkyca
	                    SET trangthai= '1',
                        nguoiduyet = '" + nguoiDuyet + @"'
	                    WHERE madangkyca IN (" + maDonDangKyCa + ");";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) RejectShiftRequestQuery(string maDonDangKyCa, string nguoiDuyet)
        {
            // Param component
            var param = new DynamicParameters();
            string query = @"
                UPDATE public.tbl_dangkyca
	                    SET trangthai= '2',
                        nguoiduyet = '" + nguoiDuyet + @"'
	                    WHERE madangkyca IN (" + maDonDangKyCa + ");";

            return (query.ToString(), param);
        }

        private static (string sql, DynamicParameters param) CreateQuery(Models.DangKyCa model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaytao", model.NgayTao, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":calamviechientai", model.CaLamViecHienTai, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":calamviecmoi", model.CaLamViecMoi, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaybatdaucamoi", model.NgayBatDauCaMoi, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":nguoiduyet", model.NguoiDuyet, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":madangkyca", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // SQL component
            string query = @"INSERT INTO public.tbl_dangkyca(
	                        manhanvien, ngaytao, calamviechientai, calamviecmoi, ngaybatdaucamoi, nguoiduyet, trangthai)
	                        VALUES (:manhanvien, :ngaytao, :calamviechientai, :calamviecmoi, :ngaybatdaucamoi, :nguoiduyet, '" + model.TrangThai + @"')
                            RETURNING madangkyca";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetLatestShiftToUpdateQuery()
        {
            // Param component
            var param = new DynamicParameters();

            // SQL component
            string query = @"SELECT 
	                        sub.manhanvien,
	                        sub.ngaybatdaucamoi,
	                        sub.calamviecmoi
                            FROM 
                                (
                                    SELECT 
                                        manhanvien,
                                        MAX(ngaybatdaucamoi) AS ngaybatdaucamoi
                                    FROM 
                                        tbl_dangkyca
                                    WHERE trangthai = '1'
                                    GROUP BY 
                                        manhanvien
                                ) AS main
                            JOIN 
                                tbl_dangkyca AS sub ON main.manhanvien = sub.manhanvien AND main.ngaybatdaucamoi = sub.ngaybatdaucamoi;";
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
