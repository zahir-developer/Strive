using Newtonsoft.Json;

namespace Greeter.DTOs
{
    public class BaseResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("status")]
        public string Message { get; set; }
    }
}