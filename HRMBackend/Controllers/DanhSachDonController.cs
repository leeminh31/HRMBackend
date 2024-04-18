using AutoMapper;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources;
using HRMBackend.Services.DanhSachDon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Resources.DTO.DonConNho.Request;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using Microsoft.AspNetCore.Authorization;

namespace HRMBackend.Controllers
{
    [Route("api/v1/DanhSachDon")]
    public class DanhSachDonController : ParentController
    {
        #region Property
        private readonly IDanhSachDonService _danhSachDonService;
        #endregion

        #region Constructor
        public DanhSachDonController(IDanhSachDonService danhSachDonService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._danhSachDonService = danhSachDonService;
        }
        #endregion

        [HttpGet()]
        [SwaggerOperation(summary: "Lấy thông tin theo mã nhân viên")]
        [Authorize]

        public async Task<IActionResult> GetByEmployeeIdAsync(string maNhanVien, DateTime ngayLamViec)
        {
            var result = await _danhSachDonService.GetByEmployeeIdAsync(maNhanVien, ngayLamViec);

            return Ok(result);

        }


        [HttpPost()]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        [Authorize]
        public async Task<IActionResult> GetByParamsAsync(SearchDanhSachDonRequest danhSachDonRequest)
        {
            var result = await _danhSachDonService.GetByParamsAsync(danhSachDonRequest);

            return Ok(result);

        }

        [HttpPost("ApproveRequest")]
        [SwaggerOperation(summary: "Duyệt đơn")]
        [Authorize]
        public async Task<IActionResult> ApproveRequestAsync(ApproveRequestList request)
        {
            var result = await _danhSachDonService.ApproveAllRequestAsync(request);

            return Ok(result);
        }

        [HttpPost("RejectRequest")]
        [SwaggerOperation(summary: "Hủy đơn")]
        [Authorize]
        public async Task<IActionResult> RejectRequestAsync(ApproveRequestList request)
        {
            var result = await _danhSachDonService.RejectAllRequestAsync(request);

            return Ok(result);

        }

        [HttpPost("CreateDonBu")]
        [SwaggerOperation(summary: "Tạo đơn bù")]
        [Authorize]
        public async Task<IActionResult> CreateDonBuAsync(CreateDonBuRequest request)
        {
            var result = await _danhSachDonService.CreateDonBuAsync(request);

            return Ok(result);

        }

        [HttpPost("CreateDonConNho")]
        [SwaggerOperation(summary: "Tạo đơn con nhỏ")]
        [Authorize]
        public async Task<IActionResult> CreateDonConNhoAsync(CreateDonConNhoRequest request)
        {
            var result = await _danhSachDonService.CreateDonConNhoAsync(request);

            return Ok(result);

        }

        [HttpPost("CreateDonPhep")]
        [SwaggerOperation(summary: "Tạo đơn phép")]
        [Authorize]
        public async Task<IActionResult> CreateDonPhepAsync(CreateDonPhepRequest request)
        {
            var result = await _danhSachDonService.CreateDonPhepAsync(request);

            return Ok(result);

        }

        [HttpPost("CreateDonTangCa")]
        [SwaggerOperation(summary: "Tạo đơn tăng ca")]
        [Authorize]
        public async Task<IActionResult> CreateDonTangCaAsync(CreateDonTangCaRequest request)
        {
            var result = await _danhSachDonService.CreateDonTangCaAsync(request);

            return Ok(result);

        }

        [HttpPost("UpdateDonTangCa")]
        [SwaggerOperation(summary: "Sửa đơn tăng ca")]
        [Authorize]
        public async Task<IActionResult> UpdateDonTangCaAsync(UpdateDonTangCaRequest request)
        {
            var result = await _danhSachDonService.UpdateDonTangCaAsync(request);

            return Ok(result);
        }

        [HttpPost("UpdateDonBu")]
        [SwaggerOperation(summary: "Sửa đơn bù")]
        [Authorize]
        public async Task<IActionResult> UpdateDonBuAsync(UpdateDonBuRequest request)
        {
            var result = await _danhSachDonService.UpdateDonBuAsync(request);

            return Ok(result);
        }

        [HttpPost("UpdateDonPhep")]
        [SwaggerOperation(summary: "Sửa đơn phép")]
        [Authorize]
        public async Task<IActionResult> UpdateDonPhepAsync(UpdateDonPhepRequest request)
        {
            var result = await _danhSachDonService.UpdateDonPhepAsync(request);

            return Ok(result);
        }

        [HttpPost("UpdateDonConNho")]
        [SwaggerOperation(summary: "Sửa đơn con nhỏ")]
        [Authorize]
        public async Task<IActionResult> UpdateDonConNhoAsync(UpdateDonConNhoRequest request)
        {
            var result = await _danhSachDonService.UpdateDonConNhoAsync(request);

            return Ok(result);
        }

        [HttpGet("GetTotalOTMinutes")]
        [SwaggerOperation(summary: "Quỹ bù hiện có")]
        [Authorize]
        public async Task<IActionResult> GetTotalMinutesOTAsync(string maNhanVien, int nam)
        {
            var result = await _danhSachDonService.GetTotalMinutesOTAsync(maNhanVien, nam);

            return Ok(result);
        }

        [HttpGet("GetTotalDayOffByYear")]
        [SwaggerOperation(summary: "Quỹ phép hiện có")]
        [Authorize]
        public async Task<IActionResult> GetTotalDayOffByYearAsync(string maNhanVien, int nam)
        {
            var result = await _danhSachDonService.GetTotalDayOffByYearAsync(maNhanVien, nam);

            return Ok(result);
        }
    }
}
