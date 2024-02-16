using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.PhongBan.Request;
using HRMBackend.Services.PhongBan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/PhongBan")]
    public class PhongBanController : ParentController
    {
        #region Property
        private readonly IPhongBanService _phongBanService;
        #endregion

        #region Constructor
        public PhongBanController(IPhongBanService phongBanService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._phongBanService = phongBanService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: Lấy danh sách mã phòng ban + tên phòng ban
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet()]
        [SwaggerOperation(summary: "Lấy thông tin phòng ban")]
        //[Authorize]
        public async Task<IActionResult> GetByParamsAsync([FromQuery] SearchPhongBanRequest request)
        {
            var result = await _phongBanService.GetByParamsAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: tạo mới phongBan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin cảng")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePhongBanRequest request)
        {
            var result = await _phongBanService.CreateAsync(request);

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
        public async Task<IActionResult> GetByCodeOrNameAsync([FromBody] SearchPhongBanRequest request)
        {
            var result = await _phongBanService.GetByCodeOrNameAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng bằng mã kí hiệu và tên theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("pagination")]
        [SwaggerOperation(summary: "Lấy danh sách thông tin cảng dựa theo param (like code và like name)")]
        //[Authorize]
        public async Task<IActionResult> PaginationGetByCodeAndNameAsync([FromBody] PaginationPhongBanRequest request)
        {
            var result = await _phongBanService.PaginationGetByCodeAndNameAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Chức năng: cập nhật thông tin cảng bằng id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost("update")]
        //[SwaggerOperation(summary: "Cập nhật thông tin cảng")]
        //[Authorize]
        //public async Task<IActionResult> UpdateAsync([FromBody] UpdatePhongBanRequest request)
        //{
        //    var result = await _phongBanService.UpdateAsync(request);
        //    return Ok(result);
        //}

        #endregion
    }
}
