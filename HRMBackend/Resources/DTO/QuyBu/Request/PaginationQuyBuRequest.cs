using HRMBackend.Extensions.Validation;

namespace HRMBackend.Resources.DTO.QuyBu.Request
{
    public class PaginationQuyBuRequest : QueryResource
    {
        #region Property
        public string Code { get; set; }
        public string Name { get; set; }

        [Orderby]
        public string Orderby { get; set; }
        #endregion
    }
}
