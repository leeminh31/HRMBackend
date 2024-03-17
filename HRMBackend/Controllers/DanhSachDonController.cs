using AutoMapper;
using HRMBackend.Resources.DTO.DanhSachDon.Request;
using HRMBackend.Resources;
using HRMBackend.Services.DanhSachDon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace HRMBackend.Controllers
{
    [Route("api/v1/DanhSachDon")]
    public class DanhSachDonController : ParentController
    {
        #region Property
        private readonly IDanhSachDonService _danhSachDonService;
        #endregion

        #region Constructor
        public DanhSachDonController(IDanhSachDonService danhSachDonService,
            IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage) : base(mapper, responseMessage)
        {
            this._danhSachDonService = danhSachDonService;
        }
        #endregion

        [HttpPost()]
        [SwaggerOperation(summary: "Lấy thông tin theo điều kiện tìm kiếm")]
        //[Authorize]
        public async Task<IActionResult> GetByParamsAsync(SearchDanhSachDonRequest danhSachDonRequest)
        {
            var result = await _danhSachDonService.GetByParamsAsync(danhSachDonRequest);

            return Ok(result);

        }
    }
}
