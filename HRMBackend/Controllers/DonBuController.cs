using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DonBu.Request;
using HRMBackend.Services.DonBu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/DonBu")]
    public class DonBuController : ParentController
    {
        #region Property
        private readonly IDonBuService _donBuService;
        #endregion

        #region Constructor
        public DonBuController(IDonBuService donBuService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._donBuService = donBuService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: tạo mới donBu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin cảng")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDonBuRequest request)
        {
            var result = await _donBuService.CreateAsync(request);

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
        public async Task<IActionResult> GetByCodeOrNameAsync([FromBody] SearchDonBuRequest request)
        {
            var result = await _donBuService.GetByCodeOrNameAsync(request);
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
        public async Task<IActionResult> PaginationGetByCodeAndNameAsync([FromBody] PaginationDonBuRequest request)
        {
            var result = await _donBuService.PaginationGetByCodeAndNameAsync(request);
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
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDonBuRequest request)
        {
            var result = await _donBuService.UpdateAsync(request);
            return Ok(result);
        }

        #endregion
    }
}
