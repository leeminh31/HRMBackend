using AutoMapper;
using HRMBackend.Resources.DTO.HopDong.Request;
using HRMBackend.Resources;
using HRMBackend.Services.HopDong;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using HRMBackend.Services.NhanVien;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;

namespace HRMBackend.Controllers
{
    [Route("api/v1/HopDong")]
    public class HopDongController : ParentController
    {
        #region Property
        private readonly IHopDongService _hopDongService;
        private readonly IFormFile _formFile;
        #endregion

        #region Constructor
        public HopDongController(IHopDongService hopDongService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._hopDongService = hopDongService;
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
        [Authorize]
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
        [SwaggerOperation(summary: "Lấy thông tin hợp đồng theo hợp đồng")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateHopDongRequest request)
        {
            var result = await _hopDongService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpPost("upload")]
        [SwaggerOperation(summary: "Upload File Excel để Import dữ liệu hợp đồng")]
        [Authorize]
        public async Task<IActionResult> UploadFileAsync(IFormFile formFile)
        {
            var result = await _hopDongService.UploadFileAsync(formFile);
            return Ok(result);
        }

        //[HttpPost("upload/timekeeping")]
        //[SwaggerOperation(summary: "Upload File Excel để Import dữ liệu chấm công")]
        //[Authorize]
        //public async Task<IActionResult> UploadFileTimekeepingAsync(IFormFile formFile)
        //{
        //    var result = await _hopDongService.UploadFileTimeKeepingAsync(formFile);
        //    return Ok(result);
        //}

        [HttpGet("dowload")]
        [SwaggerOperation(summary: "Dowload Excel Template")]
        [Authorize]
        public async Task<IActionResult> DowloadFileAsync()
        {
            try
            {
                string pathToFile = $"{Directory.GetCurrentDirectory()}\\Resources\\ExcelTemplate\\MasterFile.xlsx";
                var fileName = System.IO.Path.GetFileName(pathToFile);
                var content = await System.IO.File.ReadAllBytesAsync(pathToFile);
                new FileExtensionContentTypeProvider()
                    .TryGetContentType(fileName, out string contentType);
                return File(content, contentType, fileName);
            }
            catch
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
