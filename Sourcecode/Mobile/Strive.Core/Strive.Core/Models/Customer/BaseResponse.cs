using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class BaseResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("status")]
        public string Message { get; set; }
    }
}
