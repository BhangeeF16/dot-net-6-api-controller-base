using Newtonsoft.Json;

namespace Domain.Common.DataAccessHelpers.Pagination
{
    public class PagingData
    {
        [JsonProperty("TotalCount")]
        public int TotalCount { get; set; } = 0;
    }
}
