using Dapper;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.DanhSachDon.Request;

namespace HRMBackend.DataAccess.DonTangCa
{
    public partial class DonTangCaDAO
    {
        private static (string sql, DynamicParameters param) CreateQuery(Models.DonTangCa model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaytaodon", model.NgayTaoDon, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaylamviec", model.NgayLamViec, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":tangcatu", model.TangCaTu, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":tangcaden", model.TangCaDen, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":lydo", model.LyDo, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nguoiduyet", model.NguoiDuyet, dbType: DbType.String, direction: ParameterDirection.Input);
            //param.Add(":thoigiancapnhat", model.ThoiGianCapNhat, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add(":madontangca", model.MaDonTangCa, dbType: DbType.Int32, direction: ParameterDirection.Output);

            // SQL component
            string query = @"INSERT INTO public.tbl_dontangca(
	                        manhanvien, ngaytaodon, ngaylamviec, tangcatu, tangcaden, lydo, nguoiduyet,thoigiancapnhat, trangthai)
	                        VALUES (:manhanvien, :ngaytaodon, :ngaylamviec, :tangcatu, :tangcaden, :lydo,:thoigiancapnhat, :nguoiduyet, '" + model.TrangThai + @"')
                            RETURNING madontangca";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) UpdateQuery(Models.DonTangCa model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tangcatu", model.TangCaTu, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":tangcaden", model.TangCaDen, dbType: DbType.Time, direction: ParameterDirection.Input);
            param.Add(":lydo", model.LyDo, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":madontangca", model.MaDonTangCa, dbType: DbType.Int32, direction: ParameterDirection.Input);

            string query = @"UPDATE public.tbl_dontangca
	                        SET tangcatu=:tangcatu, 
                                tangcaden=:tangcaden, 
                                lydo=:lydo
	                        WHERE madontangca = :madontangca;";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) ApproveRequestQuery(string maDonTangCa)
        {
            // Param component
            var param = new DynamicParameters();
            //param.Add(":trangthai", request.NgayLamViecBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"UPDATE public.tbl_dontangca
	                        SET trangthai= '1'
	                        WHERE madontangca IN (" + maDonTangCa + ")";

            return (query, param);
        }
        private static (string sql, DynamicParameters param) GetByParamsQuery(SearchDanhSachDonRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":ngaylamviecbatdau", request.NgayLamViecBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaylamviecketthuc", request.NgayLamViecKetThuc, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaytaodonbatdau", request.NgayTaoBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaytaodonketthuc", request.NgayTaoKetThuc, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_DONTANGCA
                            WHERE 
                                (:ngaylamviecbatdau IS NULL OR ngaylamviec >= :ngaylamviecbatdau)
                                AND (:ngaylamviecketthuc IS NULL OR ngaylamviec <= :ngaylamviecketthuc)
                                AND (:ngaytaodonbatdau IS NULL OR ngaytaodon >= :ngaytaodonbatdau)
                                AND (:ngaytaodonketthuc IS NULL OR ngaytaodon <= :ngaytaodonketthuc)
                            ";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetDonTangCaByDayQuery(SearchDonByDayRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", request.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaylamviec", request.NgayLamViec, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_DONTANGCA
                            WHERE 
                                (:manhanvien IS NULL OR manhanvien = :manhanvien)
                                AND (:ngaylamviec IS NULL OR ngaylamviec = :ngaylamviec)
                            ";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetTotalMinutesOTQuery(string maNhanVien, int nam)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", maNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nam", nam, dbType: DbType.Int32, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT SUM(FLOOR((EXTRACT(EPOCH from tangcaden) - EXTRACT(EPOCH from tangcatu)) /60)) as quynghibuhienco
                            FROM TBL_DONTANGCA
                            WHERE 
                                (manhanvien = :manhanvien)
                                AND (trangthai = '1')
                                AND EXTRACT(YEAR from ngaylamviec) = :nam
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
