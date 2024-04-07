using Dapper;
using System.Data;

namespace HRMBackend.DataAccess.QuyPhep
{
    public partial class QuyPhepDAO
    {
        private static (string sql, DynamicParameters param) GetByEmployeeIDQuery(string? maNhanVien)
        {
            // Param component
            var param = new DynamicParameters();
            param.Add(":manhanvien", maNhanVien, dbType: DbType.String, direction: ParameterDirection.Input);

            // SQL component
            string query = @"SELECT *
                            FROM TBL_QUYPHEP
                            WHERE 
                                (:manhanvien IS NULL OR TRANSLATE(UPPER(MANHANVIEN), 'ÁÀẢẠÃĂẮẰẲẶẴÂẤẦẨẬẪĐÉÈẺẸẼÊẾỀỂỆỄÍÌỈỊĨÓÒỎỌÕỐỒỘỖÔỔƠỚỜỞỠỢÚÙỦỤŨƯỨỪỬỰỮÝỲỶỴỸáàảạãăắẵằẳặâấầẩậẫđéèẻẹẽêếềểệễíìỉịĩóòỏọõốồổộỗôơớờởỡợúùủụũưứừửựữýỳỷỵỹ', 'AAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY') LIKE '%' || :manhanvien || '%')                           
                            ";

            return (query, param);
        }
    }
}
