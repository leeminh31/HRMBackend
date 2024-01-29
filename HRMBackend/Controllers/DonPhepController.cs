using AutoMapper;
using HRMBackend.Resources;
using HRMBackend.Resources.DTO.DonPhep.Request;
using HRMBackend.Services.DonPhep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/DonPhep")]
    public class DonPhepController : ParentController
    {
        #region Property
        private readonly IDonPhepService _donPhepService;
        #endregion

        #region Constructor
        public DonPhepController(IDonPhepService donPhepService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._donPhepService = donPhepService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Chức năng: tạo mới donPhep
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerOperation(summary: "Tạo thông tin cảng")]
        //[Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDonPhepRequest request)
        {
            var result = await _donPhepService.CreateAsync(request);

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
        public async Task<IActionResult> GetByCodeOrNameAsync([FromBody] SearchDonPhepRequest request)
        {
            var result = await _donPhepService.GetByCodeOrNameAsync(request);
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
        public async Task<IActionResult> PaginationGetByCodeAndNameAsync([FromBody] PaginationDonPhepRequest request)
        {
            var result = await _donPhepService.PaginationGetByCodeAndNameAsync(request);
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
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDonPhepRequest request)
        {
            var result = await _donPhepService.UpdateAsync(request);
            return Ok(result);
        }

        #endregion
    }
}
