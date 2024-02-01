using AutoMapper;
using HRMBackend.Resources.DTO.ChiTietQuyBu.Request;
using HRMBackend.Resources;
using HRMBackend.Services.ChiTietQuyBu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/ChiTietQuyBu")]
    public class ChiTietQuyBuController : ParentController
    {
        #region Property
        private readonly IChiTietQuyBuService _chiTietQuyBuService;
        #endregion

        #region Constructor
        public ChiTietQuyBuController(IChiTietQuyBuService chiTietQuyBuService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._chiTietQuyBuService = chiTietQuyBuService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: tạo mới chiTietQuyBu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin cảng")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateChiTietQuyBuRequest request)
        {
            var result = await _chiTietQuyBuService.CreateAsync(request);

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
        public async Task<IActionResult> GetByCodeOrNameAsync([FromBody] SearchChiTietQuyBuRequest request)
        {
            var result = await _chiTietQuyBuService.GetByCodeOrNameAsync(request);
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
        public async Task<IActionResult> PaginationGetByCodeAndNameAsync([FromBody] PaginationChiTietQuyBuRequest request)
        {
            var result = await _chiTietQuyBuService.PaginationGetByCodeAndNameAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Chức năng: cập nhật thông tin cảng bằng id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [SwaggerOperation(summary: "Cập nhật thông tin cảng")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateChiTietQuyBuRequest request)
        {
            var result = await _chiTietQuyBuService.UpdateAsync(request);
            return Ok(result);
        }

        #endregion
    }
}
