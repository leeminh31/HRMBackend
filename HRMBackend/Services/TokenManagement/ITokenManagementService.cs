using HRMBackend.Resources.DTO.Authentication.Request;
using HRMBackend.Resources.DTO.Authentication.Response;
using HRMBackend.Results;

namespace HRMBackend.Services.TokenManagement
{
    public interface ITokenManagementService
    {
        /// <summary>
        /// Chức năng: tạo access-token bằng thông tin đăng nhập
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <param name="utcNow"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        Task<BaseResult<AccessTokenResponse>> GenerateTokensAsync(LoginRequest loginRequest, DateTime utcNow, string userAgent);
    }
}
