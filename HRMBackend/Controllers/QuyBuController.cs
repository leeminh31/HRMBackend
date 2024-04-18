using AutoMapper;
using HRMBackend.Resources.DTO.QuyPhep.Request;
using HRMBackend.Resources;
using HRMBackend.Services.QuyPhep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using HRMBackend.Services.QuyBu;
using HRMBackend.Resources.DTO.QuyBu.Request;
using Microsoft.AspNetCore.Authorization;

namespace HRMBackend.Controllers
{
    [Route("api/v1/QuyBu")]
    public class QuyBuController : ParentController
    {
        #region Property
        private readonly IQuyBuService _quyBuService;
        #endregion

        #region Constructor
        public QuyBuController(IQuyBuService quyBuService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._quyBuService = quyBuService;
        }
        #endregion

        [HttpPost()]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        [Authorize]
        public async Task<IActionResult> GetByParamsAsync(SearchQuyBuRequest request)
        {
            var result = await _quyBuService.GetByYearAsync(request);

            return Ok(result);

        }
    }
}
