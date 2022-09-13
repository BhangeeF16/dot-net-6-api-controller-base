using Newtonsoft.Json;

namespace Domain.Common.DTO.Pagination
{
    public class PagingData
    {
        [JsonProperty("TotalCount")]
        public int TotalCount { get; set; } = 0;
    }
}
