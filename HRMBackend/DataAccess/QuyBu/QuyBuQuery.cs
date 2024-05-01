using Dapper;
using System.Data;

namespace HRMBackend.DataAccess.QuyBu
{
    public partial class QuyBuDAO
    {
        private static (string sql, DynamicParameters param) GetByEmployeeIDQuery(string? maNhanVien)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", maNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_QUYBU
                            WHERE 
                                (:manhanvien IS NULL OR TRANSLATE(UPPER(MANHANVIEN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :manhanvien || '%')                           
                            ";

            return (query, param);
        }

        private static (string sql, DynamicParameters param) GetByYearForEmployeeQuery(string maNhanVien, int nam)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", maNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(":nam", nam, dbType: DbType.Int32, direction: ParameterDirection.Input);

            // SQL component
            string query = @"select * 
                            from tbl_quybu join tbl_chitietquybu ON tbl_quybu.maquybu = tbl_chitietquybu.maquybu
                            WHERE 
                                (manhanvien = :manhanvien AND nam = :nam)                        
                            ";

            return (query, param);
        }
    }
}
