using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.BaoCaoTheoThang.Request;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;
using HRMBackend.Services.CaLamViec;
using HRMBackend.Services.DuLieuChamCong;
using HRMBackend.Services.PhanCaNhanVien;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/BaoCaoTheoThang")]
    public class BaoCaoTheoThangController : ParentController
    {
        #region Property
        private readonly IBaoCaoTheoThangService _baoCaoTheoThangService;
        private readonly IDuLieuChamCongService _duLieuChamCongService;
        #endregion

        #region Constructor
        public BaoCaoTheoThangController(IBaoCaoTheoThangService baoCaoTheoThangService,
            IDuLieuChamCongService duLieuChamCongService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._duLieuChamCongService = duLieuChamCongService;
            this._baoCaoTheoThangService = baoCaoTheoThangService;
        }
            #endregion

        [HttpPost()]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        [Authorize]
        public async Task<IActionResult> GetByParamsAsync(SearchBaoCaoTheoThangRequest request)
        {
            var result = await _baoCaoTheoThangService.GetByParamsAsync(request);

            return Ok(result);

        }

        [HttpPost("GetWorkHour")]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        [Authorize]
        public async Task<IActionResult> GetByParamsAsync(SearchBaoCaoTheoThangByDay request)
        {
            var result = await _baoCaoTheoThangService.GetTotalHourkWorkByDayAsync(request);

            return Ok(result);

        }

        [HttpPost("GetAll")]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        [Authorize]
        public async Task<IActionResult> GetByEmployeePerMonthAsync(SearchDuLieuChamCongByMonthRequest request)
        {
            var result = await _duLieuChamCongService.GetByEmployeePerMonthAsync(request);

            return Ok(result);
        }

        [HttpPost("AssignShifts")]
        [SwaggerOperation(summary: "Phân ca nhân viên")]
        [Authorize]
        public async Task<IActionResult> AssignShiftsToEmployeeAsync()
        {
            var result = await _baoCaoTheoThangService.AssignShiftsToEmployeeAsync();
            return Ok(result);
        }
    }
}

