using AutoMapper;
using HRMBackend.Resources.DTO.CaLamViec.Request;
using HRMBackend.Resources;
using HRMBackend.Services.CaLamViec;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using HRMBackend.Services.CaLamViec;

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
        public async Task<IActionResult> GetByParamsAsync(int? maCa)
        {
            var result = await _caLamViecService.GetByShiftIdAsync(maCa);

            return Ok(result);

        }
        
    }
}
