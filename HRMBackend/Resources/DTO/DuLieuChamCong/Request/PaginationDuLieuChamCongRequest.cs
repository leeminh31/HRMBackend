using HRMBackend.Extensions.Validation;

namespace HRMBackend.Resources.DTO.DuLieuChamCong.Request
{
    public class PaginationDuLieuChamCongRequest : QueryResource
    {
        #region Property
        public string Code { get; set; }
        public string Name { get; set; }

        [Orderby]
        public string Orderby { get; set; }
        #endregion
    }
}
