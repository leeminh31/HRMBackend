using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.TaiKhoan.Request;
using HRMBackend.Services.TaiKhoan;
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
        #endregion

        #region Constructor
        public TaiKhoanController(ITaiKhoanService taiKhoanService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._taiKhoanService = taiKhoanService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: tạo mới taiKhoan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("change-password")]
        [SwaggerOperation(summary: "Thay đổi mật khẩu")]
        //[Authorize]
        public async Task<IActionResult> ChangePasswordAsync(string maNhanVien, string password)
        {
            var result = await _taiKhoanService.ChangePasswordAsync(maNhanVien, password);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: tạo mới taiKhoan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo tài khoản mới")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaiKhoanRequest request)
        {
            var result = await _taiKhoanService.CreateAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng bằng mã kí hiệu or tên theo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("search")]
        [SwaggerOperation(summary: "Lấy danh sách thông tin cảng dựa theo param (== code hoặc == name)")]
        //[Authorize]
        //[NonAction]
        public async Task<IActionResult> GetByCodeOrNameAsync([FromBody] SearchTaiKhoanRequest request)
        {
            var result = await _taiKhoanService.GetByCodeOrNameAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng bằng mã kí hiệu và tên theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost("pagination")]
        //[SwaggerOperation(summary: "Lấy danh sách thông tin cảng dựa theo param (like code và like name)")]
        ////[Authorize]
        //public async Task<IActionResult> PaginationGetByCodeAndNameAsync([FromBody] PaginationTaiKhoanRequest request)
        //{
        //    var result = await _taiKhoanService.PaginationGetByCodeAndNameAsync(request);
        //    return Ok(result);
        //}

        /// <summary>
        /// Chức năng: cập nhật thông tin cảng bằng id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost("update")]
        //[SwaggerOperation(summary: "Cập nhật thông tin cảng")]
        //[Authorize]
        //public async Task<IActionResult> UpdateAsync([FromBody] UpdateTaiKhoanRequest request)
        //{
        //    var result = await _taiKhoanService.UpdateAsync(request);
        //    return Ok(result);
        //}

        #endregion
    }
}
