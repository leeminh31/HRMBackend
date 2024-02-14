using HRMBackend.Extensions.Validation;

namespace HRMBackend.Resources.DTO.PhongBan.Request
{
    public class PaginationPhongBanRequest : QueryResource
    {
        #region Property
        public string Code { get; set; }
        public string Name { get; set; }

        [Orderby]
        public string Orderby { get; set; }
        #endregion
    }
}
