using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.Authentication.Request;
using HRMBackend.Resources.DTO.TaiKhoan.Request;
using HRMBackend.Resources.Enums;
using HRMBackend.Services.TaiKhoan;
using HRMBackend.Services.TokenManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/TaiKhoan")]
    public class TaiKhoanController : ParentController
    {
        #region Property
        private readonly ITaiKhoanService _taiKhoanService;
        private readonly ITokenManagementService _tokenManagementService;
        #endregion

        #region Constructor
        public TaiKhoanController(ITokenManagementService tokenManagementService, 
            ITaiKhoanService taiKhoanService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._tokenManagementService = tokenManagementService;
            this._taiKhoanService = taiKhoanService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: đăng nhập
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation(summary: "Đăng nhập hệ thống")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            string userAgent = Request.Headers["User-Agent"].ToString();
            try
            {
                var result = await _tokenManagementService.GenerateTokensAsync(loginRequest, DateTime.UtcNow, userAgent);

                return result.Status == StatusEnum.Success ? Ok(result) : Unauthorized(result);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }


        /// <summary>
        /// Chức năng: tạo mới taiKhoan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("change-password")]
        [SwaggerOperation(summary: "Thay đổi mật khẩu")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            var result = await _taiKhoanService.ChangePasswordAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: tạo mới taiKhoan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo tài khoản mới")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaiKhoanRequest request)
        {
            var result = await _taiKhoanService.CreateAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: lấy danh sách mã nhân viên 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [SwaggerOperation(summary: "Lấy danh sách mã nhân viên")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeIdAsync()
        {
            var result = await _taiKhoanService.GetEmployeeIdAsync();
            return Ok(result);
        }

        /// <summary>
        /// Chức năng: tạo danh sách tài khoản
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create-list")]
        [SwaggerOperation(summary: "Tạo danh sách tài khoản mới")]
        [Authorize]
        public async Task<IActionResult> CreateListAsync([FromBody] CreateTaiKhoanRequest request)
        {
            var result = await _taiKhoanService.CreateAsync(request);

            return Ok(result);
        }

        #endregion
    }
}
