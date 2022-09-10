using Newtonsoft.Json;

namespace Domain.Entities.GeneralModule.Pagination
{
    public class PagingData
    {
        [JsonProperty("TotalCount")]
        public int TotalCount { get; set; } = 0;
    }
}
