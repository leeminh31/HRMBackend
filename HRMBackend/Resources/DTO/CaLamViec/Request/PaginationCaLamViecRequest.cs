using HRMBackend.Extensions.Validation;

namespace HRMBackend.Resources.DTO.CaLamViec.Request
{
    public class PaginationCaLamViecRequest : QueryResource
    {
        #region Property
        public string Code { get; set; }
        public string Name { get; set; }

        [Orderby]
        public string Orderby { get; set; }
        #endregion
    }
}
