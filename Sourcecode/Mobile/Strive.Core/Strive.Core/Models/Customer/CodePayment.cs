using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class CodePayment
    {
        [JsonProperty("CodeId")]
        public long ID { get; set; }

        [JsonProperty("CodeValue")]
        public string Name { get; set; }
    }
}
