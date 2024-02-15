using AutoMapper;
using HRMBackend.DataAccess.UnitOfWork;
using HRMBackend.Resources.DTO.Authentication.Request;
using HRMBackend.Resources.DTO.Authentication;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HRMBackend.Resources.DTO.Authentication.Response;
using HRMBackend.DataAccess.NhanVien;
using HRMBackend.DataAccess.TaiKhoan;

namespace HRMBackend.Services.TokenManagement
{
    public class TokenManagementService : BaseService, ITokenManagementService
    {
        #region Properties
        private readonly ITaiKhoanDAO _taiKhoanDAO;
        //private readonly IRefreshTokenDAO _refreshTokenDAO;
        private readonly IUnitOfWork _unitOfWork;
        private readonly byte[] _secret;
        #endregion

        #region Constructor
        public TokenManagementService(ITaiKhoanDAO taiKhoanDAO,
            //IRefreshTokenDAO refreshTokenDAO,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._taiKhoanDAO = taiKhoanDAO;
            //this._refreshTokenDAO = refreshTokenDAO;
            this._unitOfWork = unitOfWork;
            this._secret = Encoding.ASCII.GetBytes(JwtConfig.Secret);
        }
        #endregion

        #region Method

        public async Task<BaseResult<AccessTokenResponse>> GenerateTokensAsync(LoginRequest loginRequest, DateTime utcNow, string userAgent)
        {
            // Xác thực login-request
            var tempUser = await _taiKhoanDAO.ValidateCredentialsAsync(loginRequest);
            if (!tempUser.isValid)
            {
                return GetBaseResult<AccessTokenResponse>(CodeMessage._531, status: StatusEnum.Success);
            }

            // Tạo access-token
            var accessToken = GenerateAccessToken(tempUser.data, utcNow);

            await _unitOfWork.SaveChangesAsync();

            var dataResult = MappingTokenResoure(tempUser.data, accessToken);

            return GetBaseResult(CodeMessage._200, data: dataResult);
        }

        #region Private work
        private AccessTokenResponse MappingTokenResoure(Models.TaiKhoan user,string accessToken)
        {
            var tokenResponse = Mapper.Map<AccessTokenResponse>(user);
            tokenResponse.TokenResponse.AccessToken = accessToken;

            return tokenResponse;
        }

        private string GenerateAccessToken(Models.TaiKhoan user, DateTime utcNow)
        {
            // Get claim value
            Claim[] claims = GetClaim(user);

            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken(
                JwtConfig.Issuer,
                shouldAddAudienceClaim ? JwtConfig.Audience : string.Empty,
                claims,
                expires: utcNow.AddMinutes(JwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }

        private static Claim[] GetClaim(Models.TaiKhoan taiKhoan)
        {
            var claims = new[]
            {
                // Optional: you can add other Claims.
                // Note: You avoid sensitive information, because this is public.
                new Claim(ClaimTypes.NameIdentifier, taiKhoan.MaTaiKhoan.ToString()),
                new Claim(ClaimTypes.Name, taiKhoan.TenDangNhap)
            };

            return claims;
        }

        #endregion

        #endregion
    }
}
