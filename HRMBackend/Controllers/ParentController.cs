using AutoMapper;
using HRMBackend.Extensions;
using HRMBackend.Resources.Enums;
using HRMBackend.Resources;
using HRMBackend.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using HRMBackend.Resources.CustomExceptions;

namespace HRMBackend.Controllers
{
    [ApiController]

    public abstract class ParentController : ControllerBase
    {
        #region Property
        protected readonly ResponseMessage ResponseMessage;
        protected readonly IMapper Mapper;
        #endregion

        #region Constructor
        public ParentController(IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage)
        {
            this.ResponseMessage = responseMessage.CurrentValue;
            this.Mapper = mapper;
        }
        #endregion

        #region Method
        protected virtual BaseResult<Inner> GetBaseResult<Inner>(CodeMessage statusCode, Inner data = default, StatusEnum status = StatusEnum.Success, string message = "")
        {
            string nameStatusCode = Enum.GetName(typeof(CodeMessage), statusCode).TrimStart('_');

            string tempCode = string.IsNullOrEmpty(nameStatusCode) ? "217" : nameStatusCode.RemoveSpaceCharacter();
            string tempMessage = string.IsNullOrEmpty(message) ? ResponseMessage.Values[tempCode].RemoveSpaceCharacter() : message.RemoveSpaceCharacter();

            return new BaseResult<Inner>()
            {
                StatusCode = tempCode,
                Data = data,
                Status = status,
                Message = tempMessage
            };
        }

        protected virtual async Task ValidateIsofhTokenAsync()
        {
            var accessToken = Request.Headers.Authorization;

            if (string.IsNullOrEmpty(accessToken))
                throw new UnauthorizedResultException();
        }
        #endregion
    }
}
