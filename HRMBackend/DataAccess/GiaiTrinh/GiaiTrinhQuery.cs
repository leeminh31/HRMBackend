using Dapper;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.GiaiTrinh.Request;
using System.Data;
using System.Text;

namespace HRMBackend.DataAccess.GiaiTrinh
{
    public partial class GiaiTrinhDAO
    {
        private static (string sql, DynamicParameters param) CreateQuery(Models.GiaiTrinh model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaytaogiaitrinh", model.NgayTaoGiaiTrinh, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaylamviec", model.NgayLamViec, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":lydo", model.LyDo, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":loaigiaitrinh", model.LoaiGiaiTrinh, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nguoiduyet", model.NguoiDuyet, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":magiaitrinh", model.MaGiaiTrinh, dbType: DbType.Int32, direction: ParameterDirection.Output);

            // SQL component
            string query = @"INSERT INTO public.tbl_giaitrinh(
                            manhanvien, ngaytaogiaitrinh, ngaylamviec, loaigiaitrinh, lydo, nguoiduyet, trangthai)
                            VALUES (:manhanvien, :ngaytaogiaitrinh, :ngaylamviec, :loaigiaitrinh, :lydo, :nguoiduyet, '" + model.TrangThai + @"')
                            RETURNING magiaitrinh";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) UpdateQuery(Models.GiaiTrinh model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":lydo", model.LyDo, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":magiaitrinh", model.MaGiaiTrinh, dbType: DbType.Int32, direction: ParameterDirection.Input);

            string query = @"UPDATE public.tbl_giaitrinh
	                        SET lydo=:lydo
	                        WHERE magiaitrinh = :magiaitrinh;";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetByEmployeeIdQuery(SearchGiaiTrinhRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":ngaylamviecbatdau", request.NgayLamViecBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaylamviecketthuc", request.NgayLamViecKetThuc, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaytaodonbatdau", request.NgayTaoBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaytaodonketthuc", request.NgayTaoKetThuc, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":loaigiaitrinh", request.LoaiGiaiTrinh, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_GIAITRINH
                            WHERE 
                                (:ngaylamviecbatdau IS NULL OR ngaylamviec >= :ngaylamviecbatdau)
                                AND (:ngaylamviecketthuc IS NULL OR ngaylamviec <= :ngaylamviecketthuc)
                                AND (:ngaytaodonbatdau IS NULL OR ngaytaogiaitrinh >= :ngaytaodonbatdau)
                                AND (:ngaytaodonketthuc IS NULL OR ngaytaogiaitrinh <= :ngaytaodonketthuc)
                                AND (:loaigiaitrinh IS NULL OR loaigiaitrinh = :loaigiaitrinh)
                            ";

            return (query, param);
        }
        private static (string sql, DynamicParameters param) GetByParamsQuery(SearchGiaiTrinhRequest request)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":ngaylamviecbatdau", request.NgayLamViecBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaylamviecketthuc", request.NgayLamViecKetThuc, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaytaodonbatdau", request.NgayTaoBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaytaodonketthuc", request.NgayTaoKetThuc, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":loaigiaitrinh", request.LoaiGiaiTrinh, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_GIAITRINH
                            WHERE 
                                (:ngaylamviecbatdau IS NULL OR ngaylamviec >= :ngaylamviecbatdau)
                                AND (:ngaylamviecketthuc IS NULL OR ngaylamviec <= :ngaylamviecketthuc)
                                AND (:ngaytaodonbatdau IS NULL OR ngaytaogiaitrinh >= :ngaytaodonbatdau)
                                AND (:ngaytaodonketthuc IS NULL OR ngaytaogiaitrinh <= :ngaytaodonketthuc)
                                AND (:loaigiaitrinh IS NULL OR loaigiaitrinh = :loaigiaitrinh)
                            ";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) ApproveAllExplantionTypeQuery(string maGiaiTrinh, string nguoiDuyet)
        {
            // Param component
            var param = new DynamicParameters();
            string query = @"
                UPDATE public.tbl_giaitrinh
	                    SET trangthai= '1',
                        nguoiduyet = '" + nguoiDuyet + @"'
	                    WHERE magiaitrinh IN (" + maGiaiTrinh + ");";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) RejectAllExplanationTypeQuery(string maGiaiTrinh, string nguoiDuyet)
        {
            // Param component
            var param = new DynamicParameters();
            string query = @"
                UPDATE public.tbl_giaitrinh
	                    SET trangthai= '2',
                        nguoiduyet = '" + nguoiDuyet + @"'
	                    WHERE magiaitrinh IN (" + maGiaiTrinh + ");";

            return (query.ToString(), param);
        }
    }
}
