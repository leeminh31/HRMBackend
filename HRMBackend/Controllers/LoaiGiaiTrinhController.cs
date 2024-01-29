using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.LoaiGiaiTrinh.Request;
using HRMBackend.Services.LoaiGiaiTrinh;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/LoaiGiaiTrinh")]
    public class LoaiGiaiTrinhController : ParentController
    {
        #region Property
        private readonly ILoaiGiaiTrinhService _loaiGiaiTrinhService;
        #endregion

        #region Constructor
        public LoaiGiaiTrinhController(ILoaiGiaiTrinhService loaiGiaiTrinhService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._loaiGiaiTrinhService = loaiGiaiTrinhService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: tạo mới loaiGiaiTrinh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin cảng")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateLoaiGiaiTrinhRequest request)
        {
            var result = await _loaiGiaiTrinhService.CreateAsync(request);

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
        public async Task<IActionResult> GetByCodeOrNameAsync([FromBody] SearchLoaiGiaiTrinhRequest request)
        {
            var result = await _loaiGiaiTrinhService.GetByCodeOrNameAsync(request);
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
        public async Task<IActionResult> PaginationGetByCodeAndNameAsync([FromBody] PaginationLoaiGiaiTrinhRequest request)
        {
            var result = await _loaiGiaiTrinhService.PaginationGetByCodeAndNameAsync(request);
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
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateLoaiGiaiTrinhRequest request)
        {
            var result = await _loaiGiaiTrinhService.UpdateAsync(request);
            return Ok(result);
        }

        #endregion
    }
}
