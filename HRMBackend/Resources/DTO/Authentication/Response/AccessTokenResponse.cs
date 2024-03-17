using HRMBackend.Resources.DTO.TaiKhoan.Response;

namespace HRMBackend.Resources.DTO.Authentication.Response
{
    public class AccessTokenResponse : TaiKhoanResponse
    {
        public TokenResponse TokenResponse { get; set; }
    }

    public class TokenResponse
    { 
        public DateTime ExpireTimeUTC { get; set; }
        public string AccessToken { get; set; }
        public string HoTen {  get; set; }
    }
}
