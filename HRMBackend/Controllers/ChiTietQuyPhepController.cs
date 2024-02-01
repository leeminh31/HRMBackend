using AutoMapper;
using HRMBackend.Resources.DTO.ChiTietQuyPhep.Request;
using HRMBackend.Resources;
using HRMBackend.Services.ChiTietQuyPhep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/ChiTietQuyPhep")]
    public class ChiTietQuyPhepController : ParentController
    {
        #region Property
        private readonly IChiTietQuyPhepService _chiTietQuyPhepService;
        #endregion

        #region Constructor
        public ChiTietQuyPhepController(IChiTietQuyPhepService chiTietQuyPhepService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._chiTietQuyPhepService = chiTietQuyPhepService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: tạo mới chiTietQuyPhep
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin cảng")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateChiTietQuyPhepRequest request)
        {
            var result = await _chiTietQuyPhepService.CreateAsync(request);

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
        public async Task<IActionResult> GetByCodeOrNameAsync([FromBody] SearchChiTietQuyPhepRequest request)
        {
            var result = await _chiTietQuyPhepService.GetByCodeOrNameAsync(request);
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
        public async Task<IActionResult> PaginationGetByCodeAndNameAsync([FromBody] PaginationChiTietQuyPhepRequest request)
        {
            var result = await _chiTietQuyPhepService.PaginationGetByCodeAndNameAsync(request);
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
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateChiTietQuyPhepRequest request)
        {
            var result = await _chiTietQuyPhepService.UpdateAsync(request);
            return Ok(result);
        }

        #endregion
    }
}
