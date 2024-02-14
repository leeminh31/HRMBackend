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
        /// Chức năng: lấy dữ liệu cảng bằng mã kí hiệu or tên theo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost("search")]
        //[SwaggerOperation(summary: "Lấy danh sách thông tin nhân viên dựa theo param")]
        ////[Authorize]
        ////[NonAction]
        //public async Task<IActionResult> GetByParamsAsync([FromBody] SearchNhanVienRequest request)
        //{
        //    var result = await _nhanVienService.GetByParamsAsync(request);
        //    return Ok(result);
        //}

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng bằng mã kí hiệu và tên theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost("pagination")]
        //[SwaggerOperation(summary: "Lấy danh sách thông tin cảng dựa theo param (like code và like name)")]
        //[Authorize]
        //public async Task<IActionResult> PaginationGetByCodeAndNameAsync([FromBody] PaginationNhanVienRequest request)
        //{
        //    var result = await _nhanVienService.PaginationGetByCodeAndNameAsync(request);
        //    return Ok(result);
        //}

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
