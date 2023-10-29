using Newtonsoft.Json;

namespace Aurora.Framework.Entities
{
    public class PagedViewRequest
    {
        [JsonProperty(PropertyName = "i")]
        public int PageIndex { get; set; }

        [JsonProperty(PropertyName = "s")]
        public int PageSize { get; set; }
    }
}