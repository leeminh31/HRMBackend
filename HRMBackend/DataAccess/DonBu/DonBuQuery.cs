using Dapper;
using HRMBackend.Resources.DTO.DonBu.Request;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Models;

namespace HRMBackend.DataAccess.DonBu
{
    public partial class DonBuDAO
    {
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
                            FROM TBL_DONBU
                            WHERE 
                                (:ngaylamviecbatdau IS NULL OR ngaylamviec >= :ngaylamviecbatdau)
                                AND (:ngaylamviecketthuc IS NULL OR ngaylamviec <= :ngaylamviecketthuc)
                                AND (:ngaytaodonbatdau IS NULL OR ngaytaodon >= :ngaytaodonbatdau)
                                AND (:ngaytaodonketthuc IS NULL OR ngaytaodon <= :ngaytaodonketthuc)
                            ";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) ApproveRequestQuery(string maDonBu)
        {
            // Param component
            var param = new DynamicParameters();
            //param.Add(":trangthai", request.NgayLamViecBatDau, dbType: DbType.Date, direction: ParameterDirection.Input);

            // SQL component
            string query = @"UPDATE public.tbl_donbu
	                        SET trangthai= '1'
	                        WHERE madonbu IN (" + maDonBu + ")";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) CreateQuery(Models.DonBu model)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", model.MaNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":ngaytaodon", model.NgayTaoDon, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":ngaylamviec", model.NgayLamViec, dbType: DbType.Date, direction: ParameterDirection.Input);
            param.Add(":sophutxinbu", model.SoPhutXinBu, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add(":lydo", model.LyDo, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nguoiduyet", model.NguoiDuyet, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":madonbu", model.MaDonBu, dbType: DbType.Int32, direction: ParameterDirection.Output);

            // SQL component
            string query = @"INSERT INTO public.tbl_donbu(
	                        manhanvien, ngaytaodon, ngaylamviec, sophutxinbu, lydo, nguoiduyet, trangthai)
	                        VALUES (:manhanvien, :ngaytaodon, :ngaylamviec, :sophutxinbu, :lydo, :nguoiduyet, '"+ model.TrangThai +@"')
                            RETURNING madonbu";
            return (query, param);
        }

        private static (string sql, DynamicParameters param) ApproveAllRequestTypeQuery(ApproveRequestList request)
        {
            // Param component
            var param = new DynamicParameters();
            StringBuilder query = new StringBuilder("");

            if (!String.IsNullOrEmpty(request.maDonBu))
            {
                query.AppendLine(@"
                        UPDATE public.tbl_donbu
	                    SET trangthai= '1'
	                    WHERE madonbu IN (" + request.maDonBu + ");");
                query.Append(Environment.NewLine);
            }


            if (!String.IsNullOrEmpty(request.maDonConNho))
            {
                query.AppendLine(@"
                        UPDATE public.tbl_donconnho
	                    SET trangthai= '1'
	                    WHERE madonconnho IN (" + request.maDonConNho + ");");
                query.Append(Environment.NewLine);

            }

            if (!String.IsNullOrEmpty(request.maDonPhep))
            {
                query.AppendLine(@"
                    UPDATE public.tbl_donphep
                    SET trangthai= '1'
                    WHERE madonphep IN (" + request.maDonPhep + ");");
                query.Append(Environment.NewLine);
            }

            if (!String.IsNullOrEmpty(request.maDonTangCa))
            {
                query.AppendLine(@"
                     UPDATE public.tbl_dontangca
                     SET trangthai= '1'
                     WHERE madontangca IN (" + request.maDonTangCa + ");");
                query.Append(Environment.NewLine);
            }

            return (query.ToString(), param);
        }

        private static (string sql, DynamicParameters param) RejectAllRequestTypeQuery(ApproveRequestList request)
        {
            // Param component
            var param = new DynamicParameters();
            StringBuilder query = new StringBuilder();

            if (!String.IsNullOrEmpty(request.maDonBu))
            {
                query.AppendLine(@"
                        UPDATE public.tbl_donbu
	                    SET trangthai= '2'
	                    WHERE madonbu IN (" + request.maDonBu + ");");
                query.Append(Environment.NewLine);
            }


            if (!String.IsNullOrEmpty(request.maDonConNho))
            {
                query.AppendLine(@"
                        UPDATE public.tbl_donconnho
	                    SET trangthai= '2'
	                    WHERE madonconnho IN (" + request.maDonConNho + ");");
                query.Append(Environment.NewLine);

            }

            if (!String.IsNullOrEmpty(request.maDonPhep))
            {
                query.AppendLine(@"
                    UPDATE public.tbl_donphep
                    SET trangthai= '2'
                    WHERE madonphep IN (" + request.maDonPhep + ");");
                query.Append(Environment.NewLine);
            }

            if (!String.IsNullOrEmpty(request.maDonTangCa))
            {
                query.AppendLine(@"
                     UPDATE public.tbl_dontangca
                     SET trangthai= '2'
                     WHERE madontangca IN (" + request.maDonTangCa + ");");
                query.Append(Environment.NewLine);
            }

            return (query.ToString(), param);
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
