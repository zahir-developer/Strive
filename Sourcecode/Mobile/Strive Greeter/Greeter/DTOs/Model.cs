using System;
using System.Collections.Generic;
using Greeter.Common;
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

        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("EmployeeDetails")]
        public EmployeeDetails EmployeeDetails { get; set; }
    }

    public class EmployeeDetails
    {
        [JsonProperty("EmployeeLogin")]
        public EmployeeLogin EmployeeLogin { get; set; }
    }

    public class EmployeeLogin
    {
        [JsonProperty("EmployeeId")]
        public long EmployeeId { get; set; }
    }

    public class LocationsResponse : BaseResponse
    {
        [JsonProperty("Location")]
        public List<Location> Locations { get; set; }
    }

    public class Location
    {
        [JsonProperty("LocationId")]
        public int ID { get; set; }

        [JsonProperty("LocationName")]
        public string Name { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("WashTimeMinutes")]
        public short WashTimeMinutes { get; set; }
    }

    public class BarcodeResponse : BaseResponse
    {
        [JsonProperty("ClientAndVehicleDetail")]
        public List<ClientAndVehicleDetail> ClientAndVehicleDetailList { get; set; }
    }

    public class ClientAndVehicleDetail
    {
        [JsonProperty("ClientId")]
        public long ClientID { get; set; }

        [JsonProperty("VehicleId")]
        public long VehicleID { get; set; }

        [JsonProperty("Barcode")]
        public string Barcode { get; set; }

        [JsonProperty("VehicleMfr")]
        public long MakeID { get; set; }

        [JsonProperty("VehicleModelId")]
        public long ModelID { get; set; }

        [JsonProperty("Upcharge")]
        public long UpchargeID { get; set; }

        [JsonProperty("VehicleModel")]
        public string Model { get; set; }

        [JsonProperty("VehicleColor")]
        public int ColorID { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        //[JsonProperty("MiddleName")]
        //public string MiddleName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }
    }

    public class MakeResponse : BaseResponse
    {
        [JsonProperty("Make")]
        public List<Make> MakeList { get; set; }
    }

    public class Make
    {
        [JsonProperty("MakeId")]
        public int ID { get; set; }

        [JsonProperty("MakeValue")]
        public string Name { get; set; }
    }

    public class ModelResponse : BaseResponse
    {
        [JsonProperty("Model")]
        public List<Model> ModelList { get; set; }
    }

    public class Model
    {
        [JsonProperty("ModelId")]
        public int ID { get; set; }

        [JsonProperty("ModelValue")]
        public string Name { get; set; }
    }

    public class ServiceResponse : BaseResponse
    {
        [JsonProperty("AllServiceDetail")]
        public List<ServiceDetail> ServiceDetailList { get; set; }
    }

    public class ServiceDetail
    {
        [JsonProperty("ServiceId")]
        public long ID { get; set; }

        [JsonProperty("ServiceTypeId")]
        public string TypeId { get; set; }

        [JsonProperty("ServiceName")]
        public string Name { get; set; }

        [JsonProperty("ServiceTypeName")]
        public string Type { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("Upcharges")]
        public string Upcharges { get; set; }

        [JsonProperty("EstimatedTime")]
        public float Time { get; set; }
    }

    public class GlobalDataResponse : BaseResponse
    {
        [JsonProperty("Codes")]
        public List<Code> Codes { get; set; }
    }

    public class Code
    {
        [JsonProperty("CodeId")]
        public long ID { get; set; }

        [JsonProperty("CodeValue")]
        public string Name { get; set; }
    }

    public class TicketResponse : BaseResponse
    {
        [JsonProperty("GetTicketNumber")]
        public Ticket Ticket { get; set; }
    }

    public class Ticket
    {
        [JsonProperty("TicketNumber")]
        public long TicketNo { get; set; }

        //[JsonProperty("JobId")]
        //public string Name { get; set; }
    }

    public class CreateServiceRequest
    {
        [JsonProperty("job")]
        public Job Job { get; set; }

        [JsonProperty("jobItem")]
        public List<JobItem> JobItems { get; set; }
    }

    public class Job
    {
        [JsonProperty("actualTimeOut")]
        public DateTime? ActualTimeOut { get; } = null;

        [JsonProperty("clientId")] // Optional
        public long? ClientId { get; set; } = null;

        [JsonProperty("color")]
        public long ColorId { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedByID { get; } = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        [JsonProperty("estimatedTimeOut")]
        public DateTime EstimatedTimeOut { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; } = false;

        [JsonProperty("jobDate")]
        public DateTime JobDate { get; } = DateTime.Now;

        [JsonProperty("jobId")]
        public long JobId { get; set; }

        [JsonProperty("jobStatus")]
        public long JobStatusID { get; set; }

        [JsonProperty("jobType")]
        public long JobTypeID { get; set; }

        [JsonProperty("locationId")]
        public long LocationID { get; set; }

        [JsonProperty("make")]
        public long MakeID { get; set; }

        [JsonProperty("model")]
        public long ModelID { get; set; }

        //[JsonProperty("notes")]
        //public string Notes { get; }

        [JsonProperty("ticketNumber")]
        public long TicketNumber { get; set; }

        [JsonProperty("timeIn")]
        public DateTime TimeIn { get; } = DateTime.Now;

        [JsonProperty("updatedBy")]
        public long UpdatedByID { get; } = AppSettings.UserID;

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; } = DateTime.Now;

        [JsonProperty("vehicleId")] // Optional
        public long? VehicleId { get; set; } = null;
    }

    public class JobItem
    {
        //[JsonProperty("commission")]
        //public int commission { get; } = 0;

        [JsonProperty("createdBy")]
        public long createdByID { get; } = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        [JsonProperty("jobId")]
        public long JobId { get; set; }

        //[JsonProperty("jobItemId")]
        //public long jobItemId { get; } = 0;

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; } = 1;

        //[JsonProperty("reviewNote")]
        //public string ReviewNote { get; } = null;

        [JsonProperty("serviceId")]
        public long ServiceId { get; set; }

        [JsonProperty("updatedBy")]
        public long updatedByID { get; } = AppSettings.UserID;

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; } = DateTime.Now;

        [JsonIgnore]
        public string SeriveName { get; set; }

        [JsonIgnore]
        public float Time { get; set; }

        [JsonIgnore]
        public bool IsMainService { get; set; }
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

        //[JsonIgnore]
        //[JsonProperty("pageNo")]
        //public short PageNo { get; set; }

        //[JsonIgnore]
        //[JsonProperty("pageSize")]
        //public short Limit { get; set; }
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
        public long ID { get; set; }

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

        [JsonProperty("Cost")]
        public float Cost { get; set; }

        [JsonProperty("AdditionalServices")]
        public string AdditionalServices { get; set; }

        [JsonProperty("Checkin")]
        public string CheckinTime { get; set; }

        [JsonProperty("Checkout")]
        public string CheckoutTime { get; set; }

        [JsonProperty("MembershipName")]
        public string MembershipName { get; set; }

        [JsonProperty("PaymentStatus")]
        public string PaymentStatus { get; set; }
    }

    public class HoldCheckoutReq
    {
        [JsonProperty("id")]
        public long ID { get; set; }

        //[JsonProperty("email")]
        //public string Email { get; set; }

        //[JsonProperty("ticketNumber")]
        //public long TicketNumber { get; set; }

        [JsonProperty("isHold")]
        public bool IsHold { get; } = true;
    }

    public class CompleteCheckoutReq
    {
        [JsonProperty("jobId")]
        public long JobId { get; set; }

        [JsonProperty("actualTimeOut")]
        public DateTime ActualTimeOut { get; } = DateTime.Now;
    }

    public class DoCheckoutReq
    {
        [JsonProperty("jobId")]
        public long JobId { get; set; }

        [JsonProperty("checkOut")]
        public bool Checkout { get; } = true;

        [JsonProperty("checkOutTime")]
        public DateTime CheckOutTime { get; } = DateTime.Now;
    }

    public class PaymentAuthReq
    {
        [JsonProperty("cardConnect")]
        public object CardConnect { get; } = new();

        [JsonProperty("paymentDetail")]
        public PaymentDetail PaymentDetail { get; set; }

        [JsonProperty("billingDetail")]
        public BillingDetail BillingDetail { get; } = new();

        //[JsonProperty("isRepeatTransaction")]
        //public bool isRepeatTransaction { get; } = false;
    }

    public class PaymentDetail
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("expiry")]
        public string Expiry { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        //[JsonProperty("orderId")]
        //public long OrderID { get; set; }

        //[JsonProperty("batchid")]
        //public long BatchID { get; set; }

        //[JsonProperty("currency")]
        //public string Currency { get; set; }

        //[JsonProperty("receipt")]
        //public string Receipt { get; set; }

        [JsonProperty("ccv")]
        public short CCV { get; set; }
    }

    public class BillingDetail
    {
        //[JsonProperty("name")]
        //public string Name { get; set; }

        //[JsonProperty("address")]
        //public string Address { get; set; }

        //[JsonProperty("city")]
        //public string City { get; set; }

        //[JsonProperty("country")]
        //public string Country { get; set; }

        //[JsonProperty("region")]
        //public string Region { get; set; }

        //[JsonProperty("postal")]
        //public string Postal { get; set; }
    }

    public class PaymentCaptureReq
    {
        [JsonProperty("authCode")]
        public string AuthCode { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("retRef")]
        public string RetRef { get; set; }

        //[JsonProperty("invoiceId")]
        //public object InvoiceID { get; } = new();
    }

    public class SalesPaymentDto
    {
        [JsonProperty("jobPayment")]
        public JobPayment JobPayment { get; set; }

        [JsonProperty("jobPaymentDetail")]
        public List<JobPaymentDetail> JobPaymentDetails { get; set; }

        //[JsonProperty("giftCardHistory")]
        //public object GiftCardHistory { get; } = null;

        //[JsonProperty("jobPaymentCreditCard")]
        //public object JobPaymentCreditCard { get; } = null;
    }

    public class AddPaymentReq
    {
        [JsonProperty("SalesPaymentDto")]
        public SalesPaymentDto SalesPaymentDto { get; set; }

        [JsonProperty("locationId")]
        public long LocationID { get; set; }

        [JsonProperty("jobId")]
        public long JobID { get; set; } 
    }

    public class JobPayment
    {
        //[JsonProperty("jobPaymentId")]
        //public long JobPaymentID { get; } = 0;

        //[JsonProperty("membershipId")]
        //public string membershipID { get; } = null;

        [JsonProperty("jobId")]
        public long JobID { get; set; }

        //[JsonProperty("drawerId")]
        //public short DrawerID { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("taxAmount")]
        public float TaxAmount { get; } = 0;

        //[JsonProperty("approval")]
        //public bool Approval { get; } = true;

        [JsonProperty("paymentStatus")]
        public long PaymentStatus { get; set; }

        //[JsonProperty("comments")]
        //public string Comments { get; } = null;

        //[JsonProperty("isActive")]
        //public bool IsActive { get; } = true;

        //[JsonProperty("isDeleted")]
        //public bool isDeleted { get; } = false;

        [JsonProperty("createdBy")]
        public long CreatedBy { get; } = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        //[JsonProperty("updatedBy")]
        //public long UpdatedBy { get; } = AppSettings.UserID;

        //[JsonProperty("updatedDate")]
        //public DateTime UpdatedDate { get; } = DateTime.Now;

        //[JsonProperty("isProcessed")]
        //public bool IsProcessed { get; } = true;

        //[JsonProperty("cashback")]
        //public int Cashback { get; } = 0;
    }

    public class JobPaymentDetail
    {
        //[JsonProperty("jobPaymentDetailId")]
        //public int JobPaymentDetailID { get; } = 0;

        //[JsonProperty("jobPaymentId")]
        //public int JobPaymentID { get; } = 0;

        [JsonProperty("paymentType")]
        public long PaymentType { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("taxAmount")]
        public float TaxAmount { get; } = 0;

        //[JsonProperty("signature")]
        //public string Signature { get; } = null;

        //[JsonProperty("isActive")]
        //public bool IsActive { get; } = true;

        //[JsonProperty("isDeleted")]
        //public bool IsDeleted { get; } = false;

        [JsonProperty("createdBy")]
        public long CreatedBy { get; } = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        //[JsonProperty("updatedBy")]
        //public long UpdatedBy { get; } = AppSettings.UserID;

        //[JsonProperty("updatedDate")]
        //public DateTime UpdatedDate { get; } = DateTime.Now;
    }

    public class GetDetailEmployeeReq
    {
        [JsonProperty("locationId")]
        public int LocationID { get; set; }

        [JsonProperty("date")]
        public DateTime CurrentDate { get; } = DateTime.Now.AddMonths(-1);
    }

    public class EmployeeListResponse : BaseResponse
    {
        [JsonProperty("result")]
        public List<Employee> EmployeeList { get; set; }
    }

    public class Employee
    {
        [JsonProperty("EmployeeId")]
        public int ID { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("")]
        public string EmailID { get; set; }
    }

    public class RefreshTokenReq
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenResponse : BaseResponse
    {
        [JsonProperty("")]
        public string Token { get; set; }

        [JsonProperty("")]
        public string RefreshToken { get; set; }
    }

    public class PayAuthResponse : BaseResponse
    {
        [JsonProperty("authcode")]
        public string Authcode { get; set; }

        [JsonProperty("retref")]
        public string Retref { get; set; }
    }

    public class RecentChatListResponse : BaseResponse
    {
        [JsonProperty("EmployeeList")]
        public EmployeeList EmployeeList { get; set; }
    }

    public class EmployeeList
    {
        [JsonProperty("ChatEmployeeList")]
        public List<RecentChat> RecentChats { get; set; }
    }

    public class RecentChat
    {
        [JsonProperty("Id")]
        public long ID { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("IsGroup")]
        public bool IsGroup { get; set; }

        [JsonProperty("RecentChatMessage")]
        public string RecentChatMessage { get; set; }

        //[JsonProperty("CreatedDate")]
        //public DateTime CreatedDate { get; set; }

        [JsonProperty("IsRead")]
        public bool IsRead { get; set; }

        [JsonProperty("ChatGroupId")]
        public long ChatGroupId { get; set; }

        [JsonProperty("GroupId")]
        public long GroupId { get; set; }

        [JsonProperty("CommunicationId")]
        public long CommunicationId { get; set; }

        [JsonProperty("ChatGroupUserId")]
        public long ChatGroupUserId { get; set; }

        //[JsonProperty("Selected")]
        //public bool Selected { get; set; }
    }
}
