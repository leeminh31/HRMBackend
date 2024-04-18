using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.NhanVien.Request;
using HRMBackend.Services.NhanVien;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/NhanVien")]
    public class NhanVienController : ParentController
    {
        #region Property
        private readonly INhanVienService _nhanVienService;
        #endregion

        #region Constructor
        public NhanVienController(INhanVienService nhanVienService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._nhanVienService = nhanVienService;
        }
        #endregion

        #region Action
        [HttpGet("HoTen/{hoTen}")]
        [SwaggerOperation(summary: "Lấy mã nhân viên theo tìm kiếm họ tên")]
        //[Authorize]
        public async Task<IActionResult> GetAllEmployeeIdByNameAsync(string hoTen)
        {
            var result = await _nhanVienService.GetAllEmployeeIdByNameAsync(hoTen);

            return Ok(result);
        }

        [HttpPost("CreateAccount")]
        [SwaggerOperation(summary: "Tạo tài khoản cho nhân viên")]
        //[Authorize]
        public async Task<IActionResult> CreateListAsyncAsync([FromBody] string maNhanVien)
        {
            var result = await _nhanVienService.CreateListAsync(maNhanVien);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: Lấy thông tin nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet()]
        [SwaggerOperation(summary: "Lấy thông tin nhân viên theo điều kiện tìm kiếm")]
        //[Authorize]
        public async Task<IActionResult> GetByParamsAsync(string? maNhanVien, int? maPhongBan, int? idVanTay, string? chucVu, string? hoTen)
        {
            var result = await _nhanVienService.GetByParamsAsync(maNhanVien, maPhongBan, idVanTay, chucVu, hoTen);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: Lấy thông tin nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{maNhanVien}")]
        [SwaggerOperation(summary: "Lấy thông tin nhân viên theo mã nhân viên")]
        //[Authorize]
        public async Task<IActionResult> GetByIDAsync(string maNhanVien)
        {
            var result = await _nhanVienService.GetByIDAsync(maNhanVien);

            return Ok(result);
        }
        /// <summary>
        /// Chức năng: tạo mới nhanVien
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin nhân viên")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateNhanVienRequest request)
        {
            var result = await _nhanVienService.CreateAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: cập nhật thông tin cảng bằng id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [SwaggerOperation(summary: "Cập nhật thông tin nhân viên")]
        //[Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateNhanVienRequest request)
        {
            var result = await _nhanVienService.UpdateAsync(request);
            return Ok(result);
        }

        #endregion
    }
}
