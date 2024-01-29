using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DonTangCa.Request;
using HRMBackend.Services.DonTangCa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/DonTangCa")]
    public class DonTangCaController : ParentController
    {
        #region Property
        private readonly IDonTangCaService _donTangCaService;
        #endregion

        #region Constructor
        public DonTangCaController(IDonTangCaService donTangCaService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._donTangCaService = donTangCaService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: tạo mới donTangCa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin cảng")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDonTangCaRequest request)
        {
            var result = await _donTangCaService.CreateAsync(request);

            return Ok(result);
        }

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng bằng mã kí hiệu or tên theo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("search")]
        [SwaggerOperation(summary: "Lấy danh sách thông tin cảng dựa theo param (== code hoặc == name)")]
        //[Authorize]
        //[NonAction]
        public async Task<IActionResult> GetByCodeOrNameAsync([FromBody] SearchDonTangCaRequest request)
        {
            var result = await _donTangCaService.GetByCodeOrNameAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Chức năng: lấy dữ liệu cảng bằng mã kí hiệu và tên theo phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("pagination")]
        [SwaggerOperation(summary: "Lấy danh sách thông tin cảng dựa theo param (like code và like name)")]
        //[Authorize]
        public async Task<IActionResult> PaginationGetByCodeAndNameAsync([FromBody] PaginationDonTangCaRequest request)
        {
            var result = await _donTangCaService.PaginationGetByCodeAndNameAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Chức năng: cập nhật thông tin cảng bằng id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [SwaggerOperation(summary: "Cập nhật thông tin cảng")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDonTangCaRequest request)
        {
            var result = await _donTangCaService.UpdateAsync(request);
            return Ok(result);
        }

        #endregion
    }
}
