using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Greeter.DTOs
{
    //public record Model(string UserId, string pswd);

    public class CommonResponse : BaseResponse
    {
        [JsonProperty("resultData")]
        public string ResultData { get; set; }

        //[JsonProperty("errorMessage")]
        //public string ErrorMessage { get; set; }
    }

    public class LoginRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("passwordHash")]
        public string Pswd { get; set; }
    }

    public class LoginResponse : BaseResponse
    {
        [JsonProperty("Token")]
        public string AuthToken { get; set; }
    }

    public class LocationsResponse : BaseResponse
    {
        [JsonProperty("Location")]
        public List<Location> Locations { get; set; }
    }

    public class Location
    {
        [JsonProperty("LocationId")]
        public string ID { get; set; }

        [JsonProperty("LocationName")]
        public string Name { get; set; }
    }

    public class BarcodeResponse : BaseResponse
    {
        [JsonProperty("")]
        public string Soemthing { get; set; }
    }

    public class CheckoutResponse : BaseResponse
    {
        [JsonProperty("")]
        public List<Checkuot> Locations { get; set; }
    }

    public class Checkuot
    {
        [JsonProperty("")]
        public string ID { get; set; }

        [JsonProperty("")]
        public string Name { get; set; }
    }
}
