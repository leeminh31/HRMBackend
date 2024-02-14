using HRMBackend.Extensions.Validation;

namespace HRMBackend.Resources.DTO.DangKyCa.Request
{
    public class PaginationDangKyCaRequest : QueryResource
    {
        #region Property
        public string Code { get; set; }
        public string Name { get; set; }

        [Orderby]
        public string Orderby { get; set; }
        #endregion
    }
}
