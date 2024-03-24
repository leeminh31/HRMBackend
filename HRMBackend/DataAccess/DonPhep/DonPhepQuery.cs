using Dapper;
using HRMBackend.Resources.DTO.DonPhep.Request;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.DanhSachDon.Request;

namespace HRMBackend.DataAccess.DonPhep
{
    public partial class DonPhepDAO
    {
        private static (string sql, DynamicParameters param) CreateQuery(Models.DonPhep model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaytaodon", model.NgayTaoDon, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaylamviec", model.NgayLamViec, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":lydo", model.LyDo, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nguoiduyet", model.NguoiDuyet, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":madonphep", model.MaDonPhep, dbType: DbType.Int32, direction: ParameterDirection.Output);

            // SQL component
            string query = @"INSERT INTO public.tbl_donphep(
	                        manhanvien, ngaytaodon, ngaylamviec, lydo, nguoiduyet, trangthai)
	                        VALUES (:manhanvien, :ngaytaodon, :ngaylamviec, :lydo, :nguoiduyet, '" + model.TrangThai + @"')
                            RETURNING madonphep";
            return (query, param);
        }
        private static (string sql, DynamicParameters param) ApproveRequestQuery(string maDonPhep)
        {
            // Param component
            var param = new DynamicParameters();
            //param.Add(":trangthai", request.NgayLamViecBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"UPDATE public.tbl_donphep
	                        SET trangthai= '1'
	                        WHERE madonphep IN (" + maDonPhep + ")";

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
                            FROM TBL_DONPHEP
                            WHERE 
                                (:ngaylamviecbatdau IS NULL OR ngaylamviec >= :ngaylamviecbatdau)
                                AND (:ngaylamviecketthuc IS NULL OR ngaylamviec <= :ngaylamviecketthuc)
                                AND (:ngaytaodonbatdau IS NULL OR ngaytaodon >= :ngaytaodonbatdau)
                                AND (:ngaytaodonketthuc IS NULL OR ngaytaodon <= :ngaytaodonketthuc)
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
