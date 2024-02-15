using HRMBackend.Resources.DTO.NhanVien.Response;

namespace HRMBackend.Resources.DTO.Authentication.Response
{
    public class AccessTokenResponse : NhanVienResponse
    {
        public TokenResponse TokenResponse { get; set; }
    }

    public class TokenResponse
    {
        public int Id { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpireTimeUTC { get; set; }

        public string AccessToken { get; set; }

        public string Role { get; set; }
    }
}
