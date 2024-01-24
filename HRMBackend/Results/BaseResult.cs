using HRMBackend.Resources.Enums;
using System.Text.Json.Serialization;

namespace HRMBackend.Results
{
    public class BaseResult<T>
    {
        #region Property
        [JsonPropertyName("data")]
        public T Data { get; init; }

        [JsonPropertyName("status")]
        [JsonIgnore]
        public StatusEnum Status { get; init; }

        [JsonPropertyName("statusCode")]
        public string StatusCode { get; init; }

        [JsonPropertyName("message")]
        public string Message { get; init; }
        #endregion

        #region Constructor
        public BaseResult() { }

        public BaseResult(string message)
        {
            Data = default;
            Status = StatusEnum.Failed;
            Message = message;
        }
        #endregion
    }
}
