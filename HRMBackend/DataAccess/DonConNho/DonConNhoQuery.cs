using Dapper;
using HRMBackend.Resources.DTO.DonConNho.Request;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.DanhSachDon.Request;

namespace HRMBackend.DataAccess.DonConNho
{
    public partial class DonConNhoDAO
    {
        private static (string sql, DynamicParameters param) CreateQuery(Models.DonConNho model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaytaodon", model.NgayTaoDon, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":tungay", model.TuNgay, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":denngay", model.DenNgay, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":lydo", model.LyDo, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nguoiduyet", model.NguoiDuyet, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":madonconnho", model.MaDonConNho, dbType: DbType.Int32, direction: ParameterDirection.Output);

            // SQL component
            string query = @"INSERT INTO public.tbl_donconnho(
	                        manhanvien, ngaytaodon, tungay, denngay, lydo, nguoiduyet, trangthai)
	                        VALUES (:manhanvien, :ngaytaodon, :tungay, :denngay, :lydo, :nguoiduyet, '" + model.TrangThai + @"')
                            RETURNING madonconnho";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) UpdateQuery(Models.DonConNho model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":tungay", model.TuNgay, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add(":denngay", model.DenNgay, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add(":lydo", model.LyDo, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":madonconnho", model.MaDonConNho, dbType: DbType.Int32, direction: ParameterDirection.Input);

            string query = @"UPDATE public.tbl_donconnho
	                        SET tungay=:tungay, 
                                denngay=:denngay, 
                                lydo=:lydo
	                        WHERE madonconnho = :madonconnho;";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) ApproveRequestQuery(string maDonConNho)
        {
            // Param component
            var param = new DynamicParameters();
            //param.Add(":trangthai", request.NgayLamViecBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"UPDATE public.tbl_donconnho
	                        SET trangthai= '1'
	                        WHERE madonconnho IN (" + maDonConNho + ")";

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
                            FROM TBL_DONCONNHO
                            WHERE 
                                (:ngaylamviecbatdau IS NULL OR tungay >= :ngaylamviecbatdau)
                                AND (:ngaylamviecketthuc IS NULL OR denngay <= :ngaylamviecketthuc)
                                AND (:ngaytaodonbatdau IS NULL OR ngaytaodon >= :ngaytaodonbatdau)
                                AND (:ngaytaodonketthuc IS NULL OR ngaytaodon <= :ngaytaodonketthuc)
                            ";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetDonConNhoByDayQuery(SearchDonByDayRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", request.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaylamviec", request.NgayLamViec, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_DONCONNHO
                            WHERE 
                                (:manhanvien IS NULL OR manhanvien = :manhanvien)
                                AND (:ngaylamviec IS NULL OR tungay <= :ngaylamviec)
                                AND (:ngaylamviec IS NULL OR denngay >= :ngaylamviec)
                            ";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetDonConNhoByMonthQuery(SearchDanhSachDonRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":ngaybatdau", request.NgayLamViecBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngayketthuc", request.NgayLamViecKetThuc, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_DONCONNHO
                            WHERE 
                                (tungay >= :ngaybatdau AND tungay <= :ngayketthuc )
                                OR (denngay >= :ngaybatdau AND denngay <= :ngayketthuc )
                                OR (tungay <= :ngaybatdau AND denngay >= :ngayketthuc)
                                AND trangthai = '1'
                            ORDER BY TUNGAY ASC
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
