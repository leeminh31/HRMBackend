using AutoMapper;
using HRMBackend.Resources.DTO.HopDong.Request;
using HRMBackend.Resources;
using HRMBackend.Services.HopDong;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using HRMBackend.Services.NhanVien;

namespace HRMBackend.Controllers
{
    [Route("api/v1/HopDong")]
    public class HopDongController : ParentController
    {
        #region Property
        private readonly IHopDongService _hopDongService;
        private readonly INhanVienService _nhanVienService;
        #endregion

        #region Constructor
        public HopDongController(IHopDongService hopDongService,
            INhanVienService nhanVienService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._hopDongService = hopDongService;
            this._nhanVienService = nhanVienService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: Lấy thông tin nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet()]
        [SwaggerOperation(summary: "Lấy thông tin hợp đồng theo điều kiện tìm kiếm")]
        //[Authorize]
        public async Task<IActionResult> GetByParamsAsync(string? tenHopDong, string? loaiHopDong)
        {
            var result = await _hopDongService.GetByParamsAsync(tenHopDong, loaiHopDong);

            return Ok(result);
            
        }

        /// <summary>
        /// Chức năng: Lấy thông tin nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{maHopDong}")]
        [SwaggerOperation(summary: "Lấy thông tin nhân viên theo mã nhân viên")]
        //[Authorize]
        public async Task<IActionResult> GetByIDAsync(string maHopDong)
        {
            var result = await _hopDongService.GetByIDAsync(maHopDong);

            return Ok(result);
        }
        /// <summary>
        /// Chức năng: tạo mới hopDong
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin hợp đồng")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateHopDongRequest request)
        {
            var result = await _hopDongService.CreateAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: cập nhật thông tin cảng bằng id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [SwaggerOperation(summary: "Cập nhật thông tin hợp đồng")]
        //[Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateHopDongRequest request)
        {
            var result = await _hopDongService.UpdateAsync(request);
            return Ok(result);
        }

        #endregion
    }
}
