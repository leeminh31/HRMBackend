using AutoMapper;
using HRMBackend.Resources.DTO.DangKyCa.Request;
using HRMBackend.Resources;
using HRMBackend.Services.DangKyCa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using HRMBackend.Services.DangKyCa;
using HRMBackend.Resources.DTO.CaLamViec.Request;
using Microsoft.AspNetCore.Authorization;

namespace HRMBackend.Controllers
{
    [Route("api/v1/DangKyCa")]
    public class DangKyCaController : ParentController
    {
        #region Property
        private readonly IDangKyCaService _dangKyCaService;
        #endregion

        #region Constructor
        public DangKyCaController(IDangKyCaService dangKyCaService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._dangKyCaService = dangKyCaService;
        }
        #endregion

        [HttpPost()]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        [Authorize]
        public async Task<IActionResult> GetByParamsAsync(SearchDangKyCaRequest request)
        {
            var result = await _dangKyCaService.GetByParamsAsync(request);

            return Ok(result);

        }

        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin đăng ký ca")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDangKyCaRequest request)
        {
            var result = await _dangKyCaService.CreateAsync(request);

            return Ok(result);
        }

        [HttpPost("Approve")]
        [SwaggerOperation(summary: "Duyệt đăng ký ca")]
        [Authorize]
        public async Task<IActionResult> ApproveShiftRequestAsync(string maDangKyCa, string nguoiDuyet)
        {
            var result = await _dangKyCaService.ApproveShiftRequestAsync(maDangKyCa, nguoiDuyet);

            return Ok(result);
        }

        [HttpPost("Reject")]
        [SwaggerOperation(summary: "Hủy đăng ký ca")]
        [Authorize]
        public async Task<IActionResult> RejectShiftRequestAsync(string maDangKyCa, string nguoiDuyet)
        {
            var result = await _dangKyCaService.RejectShiftRequestAsync(maDangKyCa, nguoiDuyet);

            return Ok(result);

        }
    }
}
