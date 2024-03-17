using AutoMapper;
using HRMBackend.Resources.DTO.CaLamViec.Request;
using HRMBackend.Resources;
using HRMBackend.Services.CaLamViec;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using HRMBackend.Services.CaLamViec;
using HRMBackend.Resources.DTO.PhongBan.Request;

namespace HRMBackend.Controllers
{
    [Route("api/v1/CaLamViec")]
    public class CaLamViecController :ParentController
    {
        #region Property
        private readonly ICaLamViecService _caLamViecService;
        #endregion

        #region Constructor
        public CaLamViecController(ICaLamViecService caLamViecService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._caLamViecService = caLamViecService;
        }
        #endregion

        [HttpGet()]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        //[Authorize]
        public async Task<IActionResult> GetByParamsAsync(int? maCa, string? tenCa)
        {
            var result = await _caLamViecService.GetByShiftIdAsync(maCa, tenCa);

            return Ok(result);

        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin ca làm việc")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCaLamViecRequest request)
        {
            var result = await _caLamViecService.CreateAsync(request);

            return Ok(result);
        }

        [HttpPost("update")]
        [SwaggerOperation(summary: "Cập nhật thông tin ca làm việc")]
        //[Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCaLamViecRequest request)
        {
            var result = await _caLamViecService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete()]
        //[Authorize]
        //[NonAction]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _caLamViecService.DeleteAsync(id);

            return Ok(result);
        }

    }
}
