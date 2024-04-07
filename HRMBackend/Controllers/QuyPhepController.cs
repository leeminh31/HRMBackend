using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.QuyPhep.Request;
using HRMBackend.Services.CaLamViec;
using HRMBackend.Services.QuyPhep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/QuyPhep")]
    public class QuyPhepController : ParentController
    {
        #region Property
        private readonly IQuyPhepService _quyPhepService;
        #endregion

        #region Constructor
        public QuyPhepController(IQuyPhepService quyPhepService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._quyPhepService = quyPhepService;
        }
        #endregion

        [HttpPost()]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        //[Authorize]
        public async Task<IActionResult> GetByParamsAsync(SearchQuyPhepRequest request)
        {
            var result = await _quyPhepService.GetByYearAsync(request);

            return Ok(result);

        }
    }
    }
