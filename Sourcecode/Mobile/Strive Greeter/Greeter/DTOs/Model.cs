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
        public long ID { get; set; }

        [JsonProperty("LocationName")]
        public string Name { get; set; }
    }

    public class BarcodeResponse : BaseResponse
    {
        [JsonProperty("ClientAndVehicleDetail")]
        public List<ClientAndVehicleDetail> ClientAndVehicleDetailList { get; set; }
    }

    public class ClientAndVehicleDetail
    {
        [JsonProperty("ClientId")]
        public long ClientId { get; set; }

        [JsonProperty("Barcode")]
        public string Barcode { get; set; }

        [JsonProperty("Upcharge")]
        public string Upcharge { get; set; }
    }

    public class GlobalDataResponse : BaseResponse
    {
        [JsonProperty("Codes")]
        public List<Code> Codes { get; set; }
    }

    public class Code
    {
        [JsonProperty("CodeId")]
        public string CodeID { get; set; }

        [JsonProperty("CodeValue")]
        public string CodeValue { get; set; }
    }

    public class CheckoutRequest
    {
        [JsonProperty("EndDate")]
        public string EndDate { get; set; }

        [JsonProperty("StartDate")]
        public string StartDate { get; set; }

        [JsonProperty("locationId")]
        public long LocationID { get; set; }

        [JsonProperty("sortBy")]
        public string SortBy { get; set; }

        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }

    public class CheckoutResponse : BaseResponse
    {
        [JsonProperty("GetCheckedInVehicleDetails")]
        public CheckinVehicleDetails CheckinVehicleDetails { get; set; }
    }

    public class CheckinVehicleDetails : BaseResponse
    {
        [JsonProperty("checkOutViewModel")]
        public List<Checkout> CheckOutList { get; set; }
    }


    public class Checkout
    {
        [JsonProperty("TicketNumber")]
        public string ID { get; set; }

        [JsonProperty("ColorCode")]
        public string ColorCode { get; set; }

        [JsonProperty("CustomerFirstName")]
        public string CustomerFirstName { get; set; }

        [JsonProperty("CustomerLastName")]
        public string CustomerLastName { get; set; }

        [JsonProperty("VehicleMake")]
        public string VehicleMake { get; set; }

        [JsonProperty("VehicleModel")]
        public string VehicleModel { get; set; }

        [JsonProperty("VehicleColor")]
        public string VehicleColor { get; set; }

        [JsonProperty("Services")]
        public string Services { get; set; }

        [JsonProperty("AdditionalServices")]
        public string AdditionalServices { get; set; }

        [JsonProperty("Checkin")]
        public string CheckinTime { get; set; }

        [JsonProperty("Checkout")]
        public string CheckoutTime { get; set; }
    }
}
