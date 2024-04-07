using AutoMapper;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources;
using HRMBackend.Services.DanhSachDon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using HRMBackend.Services.GIaiTrinh;
using HRMBackend.Resources.DTO.GiaiTrinh.Request;
using HRMBackend.Results;
using HRMBackend.Resources.DTO.DonTangCa.Request;

namespace HRMBackend.Controllers
{
    [Route("api/v1/DanhSachGiaiTrinh")]
    public class DanhSachGiaiTrinhController : ParentController
    {
        #region Property
        private readonly IGiaiTrinhService _giaiTrinhService;
        #endregion

        #region Constructor
        public DanhSachGiaiTrinhController(IGiaiTrinhService giaiTrinhService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._giaiTrinhService = giaiTrinhService;
        }
        #endregion
        [HttpPost("Create")]
        [SwaggerOperation(summary: "Tạo giải trình")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync(CreateGiaiTrinhRequest request)
        {
            var result = await _giaiTrinhService.CreateAsync(request);

            return Ok(result);

        }

        [HttpPost("Update")]
        [SwaggerOperation(summary: "Sửa giải trình")]
        //[Authorize]
        public async Task<IActionResult> UpdateAsync(UpdateGiaiTrinhRequest request)
        {
            var result = await _giaiTrinhService.UpdateAsync(request);

            return Ok(result);

        }

        [HttpGet()]
        [SwaggerOperation(summary: "Lấy thông tin theo mã nhân viên")]
        public async Task<IActionResult> GetByEmployeeIdAsync(string? maNhanVien)
        {
            var result = await _giaiTrinhService.GetByEmployeeIdAsync(maNhanVien);

            return Ok(result);

        }

        [HttpPost()]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        //[Authorize]
        public async Task<IActionResult> GetByParamsAsync(SearchGiaiTrinhRequest giaiTrinhRequest)
        {
            var result = await _giaiTrinhService.GetByParamsAsync(giaiTrinhRequest);
            return Ok(result);
        }

        [HttpPost("ApproveExplanation")]
        [SwaggerOperation(summary: "Duyệt giải trình")]
        //[Authorize]
        public async Task<IActionResult> ApproveExplanationAsync(string maGiaiTrinh, string nguoiDuyet)
        {
            var result = await _giaiTrinhService.ApproveAllExplanationAsync(maGiaiTrinh, nguoiDuyet);

            return Ok(result);
        }

        [HttpPost("RejectExplanation")]
        [SwaggerOperation(summary: "Hủy giải trình")]
        //[Authorize]
        public async Task<IActionResult> RejectExplanationAsync(string maGiaiTrinh, string nguoiDuyet)
        {
            var result = await _giaiTrinhService.RejectAllExplanationAsync(maGiaiTrinh, nguoiDuyet);

            return Ok(result);

        }
    }
    
}
