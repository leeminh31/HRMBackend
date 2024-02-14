using AutoMapper;
using HRMBackend.Extensions;
using HRMBackend.Resources;
using HRMBackend.Resources.Enums;
using HRMBackend.Results;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;

namespace HRMBackend.Services
{
    public abstract class BaseService
    {
        #region Property
        protected readonly IMapper Mapper;
        protected readonly ResponseMessage ResponseMessage;
        #endregion

        #region Constructor
        public BaseService(IMapper mapper,
            IOptionsMonitor<ResponseMessage> responseMessage)
        {
            this.Mapper = mapper;
            this.ResponseMessage = responseMessage.CurrentValue;
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

        protected virtual Base GetPaginationResult<Base, Inner>(CodeMessage statusCode, Inner data = default, StatusEnum status = StatusEnum.Success, string message = "") where Base : PaginationResult<Inner>, new()
        {
            string nameStatusCode = Enum.GetName(typeof(CodeMessage), statusCode).TrimStart('_');

            string tempCode = string.IsNullOrEmpty(nameStatusCode) ? "217" : nameStatusCode.RemoveSpaceCharacter();
            string tempMessage = string.IsNullOrEmpty(message) ? ResponseMessage.Values[tempCode].RemoveSpaceCharacter() : message.RemoveSpaceCharacter();

            return new Base()
            {
                StatusCode = tempCode,
                Data = data,
                Status = status,
                Message = tempMessage
            };
        }
        #endregion
    }
}
