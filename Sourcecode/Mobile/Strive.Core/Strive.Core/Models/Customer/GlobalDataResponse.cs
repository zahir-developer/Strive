using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class GlobalDataResponse : BaseResponsePayment
    {
        [JsonProperty("Codes")]
        public List<CodePayment> Codes { get; set; }
    }
}
