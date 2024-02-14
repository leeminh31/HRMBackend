using System.Text.Json.Serialization;

namespace HRMBackend.Results
{
    public class PaginationResult<T> : BaseResult<T>
    {
        #region Property
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("firstPage")]
        public int? FirstPage { get; set; }

        [JsonPropertyName("lastPage")]
        public int? LastPage { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }

        [JsonPropertyName("nextPage")]
        public int? NextPage { get; set; }

        [JsonPropertyName("previousPage")]
        public int? PreviousPage { get; set; }
        #endregion

        #region Constructor
        public PaginationResult() : base() { }
        public PaginationResult(string message) : base(message) { }
        #endregion
    }
}
