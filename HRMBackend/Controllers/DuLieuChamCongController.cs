using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DuLieuChamCong.Request;
using HRMBackend.Services.DuLieuChamCong;
using HRMBackend.Services.HopDong;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/DuLieuChamCong")]
    public class DuLieuChamCongController :ParentController
    {

        #region Property
        private readonly IDuLieuChamCongService _duLieuChamCongService;
        private readonly IFormFile _formFile;
        #endregion

        #region Constructor
        public DuLieuChamCongController(IDuLieuChamCongService duLieuChamCongService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._duLieuChamCongService = duLieuChamCongService;
        }
        #endregion

        [HttpPost()]
        [SwaggerOperation(summary: "Lấy thông tin dữ liệu chấm công theo điều kiện tìm kiếm")]
        //[Authorize]
        public async Task<IActionResult> GetByParamsAsync([FromBody] SearchDuLieuChamCongRequest request )
        {
            var result = await _duLieuChamCongService.GetByParamsAsync(request);

            return Ok(result);

        }

        [HttpPost("upload")]
        [SwaggerOperation(summary: "Upload File Excel để Import dữ liệu hợp đồng")]
        //[Authorize]
        public async Task<IActionResult> UploadFileAsync(IFormFile formFile)
        {
            var result = await _duLieuChamCongService.UploadFileTimeKeepingAsync(formFile);
            return Ok(result);
        }

        [HttpGet("dowload")]
        [SwaggerOperation(summary: "Dowload Excel Template")]
        //[Authorize]
        public async Task<IActionResult> DowloadFileAsync()
        {
            try
            {
                string pathToFile = $"{Directory.GetCurrentDirectory()}\\Resources\\ExcelTemplate\\DuLieuChamCong.xlsx";
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
    }
}
