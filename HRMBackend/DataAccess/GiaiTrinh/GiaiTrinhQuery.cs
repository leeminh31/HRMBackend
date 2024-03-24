using Dapper;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources.DTO.GiaiTrinh.Request;
using System.Data;
using System.Text;

namespace HRMBackend.DataAccess.GiaiTrinh
{
    public partial class GiaiTrinhDAO
    {
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

        private static (string sql, DynamicParameters param) ApproveAllExplantionTypeQuery(string maGiaiTrinh)
        {
            // Param component
            var param = new DynamicParameters();
            string query = @"
                UPDATE public.tbl_giaitrinh
	                    SET trangthai= '1'
	                    WHERE magiaitrinh IN (" + maGiaiTrinh + ");";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) RejectAllExplanationTypeQuery(string maGiaiTrinh)
        {
            // Param component
            var param = new DynamicParameters();
            string query = @"
                UPDATE public.tbl_giaitrinh
	                    SET trangthai= '2'
	                    WHERE magiaitrinh IN (" + maGiaiTrinh + ");";

            return (query.ToString(), param);
        }
    }
}
