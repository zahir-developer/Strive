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

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }
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

        [JsonProperty("StoreStatus")]
        public string StoreStatus { get; set; }

        [JsonProperty("PhoneNumber")]
        public string ShopPhoneNumber { get; set; }
    }
    public class VehicleByEmailResponse : BaseResponse
    {
        [JsonProperty("Status")]
        public List<EmailStatus> Status { get; set; }
    }
    public class EmailStatus
    {
        [JsonProperty("ClientId")]
        public long ClientID { get; set; }

        [JsonProperty("VehicleId")]
        public long VehicleID { get; set; }

        [JsonProperty("VehicleNumber")]
        public int VehicleNumber { get; set; }

        [JsonProperty("VehicleYear")]
        public int VehicleYear { get; set; }

        [JsonProperty("VehicleColorId")]
        public int VehicleColorId { get; set; }

        [JsonProperty("VehicleMakeId")]
        public int VehicleMakeId { get; set; }

        [JsonProperty("Barcode")]
        public string Barcode { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("VehicleMfr")]
        public string VehicleMfr { get; set; }

        [JsonProperty("VehicleModelId")]
        public long ModelID { get; set; }

        [JsonProperty("VehicleModel")]
        public string VehicleModel { get; set; }

        [JsonProperty("Upcharge")]
        public long UpchargeID { get; set; }

        [JsonProperty("MonthlyCharge")]
        public long MonthlyCharge { get; set; }

        [JsonProperty("VehicleColor")]
        public string VehicleColor { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("MembershipName")]
        public string MembershipName { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("IsMembership")]
        public bool IsMembership { get; set; }

        [JsonProperty("IsDiscount")]
        public bool IsDiscount { get; set; }
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

        [JsonProperty("Email")]
        public string Email { get; set; }

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
        public long TypeID { get; set; }

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

        [JsonProperty("Commission")]
        public bool Commission { get; set; }

        [JsonProperty("CommissionType")]
        public string CommissionType { get; set; }

        [JsonProperty("CommissionCost")]
        public float CommissionCost { get; set; }

        [JsonProperty("IsCeramic")]
        public bool IsCeramic { get; set; }
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
        public string TicketNo { get; set; }

        [JsonProperty("JobId")]
        public int JobId { get; set; }
    }

    public class CreateServiceRequest
    {
        [JsonProperty("job")]
        public Job Job { get; set; }

        [JsonProperty("jobItem")]
        public List<JobItem> JobItems { get; set; }

        [JsonProperty("jobDetail", NullValueHandling = NullValueHandling.Ignore)]
        public JobDetail JobDetail { get; set; } = null;

        [JsonProperty("BaySchedule", NullValueHandling = NullValueHandling.Ignore)]
        public List<BaySchedule> BaySchedules { get; set; } = null;

        [JsonProperty("isMobileApp")]
        public bool isMobileApp { get; set; }

        //[JsonProperty("deletedJobItemId")]
        //public string deletedJobItemId{ get; set; }
    }

    public class JobDetail
    {
        [JsonProperty("jobDetailId")]
        public int JobDetailID { get; set; }

        [JsonProperty("jobId")]
        public int JobID { get; set; }

        [JsonProperty("bayId")]
        public int BayID { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; } = false;

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; } = AppSettings.UserID;

        [JsonProperty("updatedBy")]
        public int UpdatedBy { get; set; }
    }

    public class BaySchedule
    {
        //[JsonProperty("bayScheduleId")]
        //public int BayScheduleID { get; set; }

        [JsonProperty("bayId")]
        public int BayID { get; set; }

        [JsonProperty("jobId")]
        public long JobID { get; set; }

        [JsonProperty("scheduleDate")]
        public string ScheduleDate { get; set; }

        [JsonProperty("scheduleInTime")]
        public string ScheduleInTime { get; set; }

        [JsonProperty("scheduleOutTime")]
        public string ScheduleOutTime { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; } = false;

        //[JsonProperty("createdBy")]
        //public int CreatedBy { get; set; }

        //[JsonProperty("updatedBy")]
        //public int UpdatedBy { get; set; }
    }

    public class Job
    {
        [JsonProperty("actualTimeOut")]
        public DateTime? ActualTimeOut { get; } = null;

        [JsonProperty("clientId")] // Optional
        public long? ClientID { get; set; } = null;

        [JsonProperty("color")]
        public long ColorID { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedByID { get; } = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        [JsonProperty("estimatedTimeOut")]
        public DateTime? EstimatedTimeOut { get; set; } = null;

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; } = false;

        [JsonProperty("jobDate")]
        public string JobDate { get; } = DateTime.Now.ToString("yyyy-MM-dd");

        [JsonProperty("jobId")]
        public int JobID { get; set; }

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
        public string TicketNumber { get; set; }

        [JsonProperty("timeIn")]
        public DateTime TimeIn { get; set; }

        [JsonProperty("updatedBy")]
        public long UpdatedByID { get; } = AppSettings.UserID;

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; } = DateTime.Now;

        [JsonProperty("vehicleId")] // Optional
        public long? VehicleID { get; set; } = null;

        [JsonProperty("barcode")]
        public String Barcode { get; set; } = null;
    }

    public class JobItem
    {
        //[JsonProperty("commission")]
        //public int commission { get; } = 0;

        [JsonProperty("createdBy")]
        public long createdByID { get; } = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        [JsonProperty("jobItemId")]
        public int jobItemId { get; set; }

        //[JsonProperty("jobItemId")]
        //public long jobItemId { get; } = 0;

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; } = 1;

        [JsonProperty("reviewNote")]
        public string ReviewNote { get; } = null;

        [JsonProperty("serviceId")]
        public long ServiceId { get; set; }

        [JsonProperty("updatedBy")]
        public long updatedByID { get; } = AppSettings.UserID;

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; } = DateTime.Now;

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool isDeleted { get; } = false;

        [JsonIgnore]
        public string SeriveName { get; set; }

        [JsonIgnore]
        public long ServiceTypeID { get; set; }

        [JsonIgnore]
        public bool IsCommission { get; set; }

        [JsonIgnore]
        public bool IsCeramic { get; set; }

        [JsonIgnore]
        public string CommissionType { get; set; }

        [JsonIgnore]
        public float CommissionAmount { get; set; }

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
        public string TicketNumber { get; set; }

        [JsonProperty("JobId")]
        public int JobId { get; set; }

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

        //[JsonProperty("MembershipNameOrPaymentStatus")]
        //public string MembershipNameOrPaymentStatus { get; set; }

        //[JsonProperty("valuedesc")]
        //public string Valuedesc { get; set; }

        [JsonProperty("JobType")]
        public string JobType { get; set; }

        [JsonProperty("JobStatus")]
        public string JobStatus { get; set; }
    }

    public class HoldCheckoutReq
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        //[JsonProperty("email")]
        //public string Email { get; set; }

        //[JsonProperty("ticketNumber")]
        //public long TicketNumber { get; set; }

        [JsonProperty("isHold")]
        public bool IsHold { get; } = true;
    }

    public class CreateMembershipResponse : BaseResponse
    {
        public Result Result { get; set; }
    }
    public class Result
    {
        [JsonProperty("JobId")]
        public int JobId { get; set; }
        [JsonProperty("TicketNumber")]
        public string TicketNumber { get; set; }
    }
    public class CompleteCheckoutReq
    {

        [JsonProperty("jobId")]
        public int JobID { get; set; }

        [JsonProperty("actualTimeOut")]
        public DateTime ActualTimeOut { get; } = DateTime.Now;
    }

    public class DoCheckoutReq
    {
        [JsonProperty("jobId")]
        public int JobID { get; set; }

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
        public BillingDetail BillingDetail { get; set; } 

        [JsonProperty("locationId")]
        public int Locationid { get; set; }

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

        [JsonProperty("orderId")]
        public string OrderID { get; set; }
        //[JsonProperty("batchid")]
        //public long BatchID { get; set; }

        //[JsonProperty("currency")]
        //public string Currency { get; set; }

        //[JsonProperty("receipt")]
        //public string Receipt { get; set; }

        //[JsonProperty("ccv")]
        //public short CCV { get; set; }
    }

    public class BillingDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }

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

        [JsonProperty("invoiceId")]
        public object InvoiceID { get; } = new();
    }

    public class SalesPaymentDto
    {
        [JsonProperty("jobPayment")]
        public JobPayment JobPayment { get; set; }

        [JsonProperty("jobPaymentDetail")]
        public List<JobPaymentDetail> JobPaymentDetails { get; set; }

        [JsonProperty("giftCardHistory")]
        public object GiftCardHistory { get; } = null;

        [JsonProperty("jobPaymentCreditCard")]
        public object JobPaymentCreditCard { get; } = null;
    }

    public class AddPaymentReq
    {
        [JsonProperty("SalesPaymentDto")]
        public SalesPaymentDto SalesPaymentDto { get; set; }

        [JsonProperty("locationId")]
        public long LocationID { get; set; }

        [JsonProperty("jobId")]
        public int JobID { get; set; }

        [JsonProperty("ticketNumber")]
        public string TicketNumber { get; set; }
    }

    public class JobPayment
    {
        [JsonProperty("jobPaymentId")]
        public long JobPaymentID { get; } = 0;

        [JsonProperty("membershipId")]
        public string membershipID { get; } = null;

        [JsonProperty("jobId")]
        public int JobID { get; set; }

        //[JsonProperty("drawerId")]
        //public short DrawerID { get; } = 1;

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("taxAmount")]
        public float TaxAmount { get; } = 0;

        [JsonProperty("approval")]
        public bool Approval { get; } = true;

        [JsonProperty("paymentStatus")]
        public long PaymentStatus { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; } = string.Empty;

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool isDeleted { get; } = false;

        [JsonProperty("createdBy")]
        public long CreatedBy { get; } = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        //[JsonProperty("updatedBy")]
        //public long UpdatedBy { get; } = AppSettings.UserID;

        //[JsonProperty("updatedDate")]
        //public DateTime UpdatedDate { get; } = DateTime.Now;

        [JsonProperty("isProcessed")]
        public bool IsProcessed { get; } = true;

        //[JsonProperty("cashback")]
        //public int Cashback { get; } = 0;
    }

    public class JobPaymentDetail
    {
        [JsonProperty("jobPaymentDetailId")]
        public int JobPaymentDetailID { get; } = 0;

        [JsonProperty("jobPaymentId")]
        public int JobPaymentID { get; } = 0;

        [JsonProperty("paymentType")]
        public long PaymentType { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("taxAmount")]
        public float TaxAmount { get; } = 0;

        [JsonProperty("signature")]
        public string Signature { get; } = string.Empty;

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; } = false;

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
        public DateTime CurrentDate { get; } = DateTime.Now;
    }

    public class EmployeeListResponse : BaseResponse
    {
        [JsonProperty("result")]
        public List<Employee> EmployeeList { get; set; }
    }

    public class Employee
    {
        [JsonProperty("EmployeeId")]
        public long ID { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
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
        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }
    }

    public class PayAuthResponse : BaseResponse
    {
        [JsonProperty("authcode")]
        public string Authcode { get; set; }

        [JsonProperty("retref")]
        public string Retref { get; set; }

        [JsonProperty("respstat")]
        public string SucessType { get; set; }

        [JsonProperty("resptext")]
        public string ErrorMessage { get; set; }

        //[JsonProperty("errorMessage")]
        //public string ErrorMessage { get; set; }
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
        public long ChatGroupID { get; set; }

        [JsonProperty("GroupId")]
        public long GroupID { get; set; }

        [JsonProperty("CommunicationId")]
        public string CommunicationID { get; set; }

        [JsonProperty("ChatGroupUserId")]
        public long ChatGroupUserID { get; set; }

        //[JsonProperty("Selected")]
        //public bool Selected { get; set; }
    }

    public class GetContactsRequest
    {
        //[JsonProperty("EndDate")]
        //public string EndDate { get; set; }

        //[JsonProperty("StartDate")]
        //public string StartDate { get; set; }

        [JsonProperty("locationId")]
        public long LocationID { get; set; }

        //[JsonProperty("sortBy")]
        //public string SortBy { get; set; }

        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }

        //[JsonProperty("status")]
        //public bool Status { get; set; }

        //[JsonProperty("query")]
        //public string Query { get; set; }

        //[JsonIgnore]
        //[JsonProperty("pageNo")]
        //public short PageNo { get; set; }

        //[JsonIgnore]
        //[JsonProperty("pageSize")]
        //public short Limit { get; set; }
    }

    public class ContactListResponse : BaseResponse
    {
        [JsonProperty("EmployeeList")]
        public ContactListObject ContactListobj { get; set; }
    }

    public class ContactListObject
    {
        [JsonProperty("Employee")]
        public List<ContactEmployee> ContactsList { get; set; }
    }

    public class ContactEmployee
    {
        [JsonProperty("EmployeeId")]
        public long EmployeeID { get; set; }

        //[JsonProperty("EmployeeCode")]
        //public long EmployeeCode { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        //[JsonProperty("MobileNo")]
        //public string MobileNo { get; set; }

        [JsonProperty("CommunicationId")]
        public string CommunicationID { get; set; }

        //[JsonProperty("Collisions")]
        //public bool Collisions { get; set; }

        //[JsonProperty("Documents")]
        //public bool Documents { get; set; }

        //[JsonProperty("Schedules")]
        //public bool Schedules { get; set; }

        //[JsonProperty("Status")]
        //public bool Status { get; set; }

        [JsonIgnore]
        public bool IsSelected { get; set; }
    }

    public class RemoveUserFromGroupResponse : BaseResponse
    {
        [JsonProperty("ChatGroupUserDelete")]
        public bool IsSuccess { get; set; }
    }

    public class CreategroupRequest
    {
        [JsonProperty("chatGroup")]
        public ChatGroup ChatGroup { get; set; }

        [JsonProperty("chatUserGroup")]
        public List<ChatUserGroup> ChatUserGroup { get; set; }

        [JsonProperty("groupId")]
        public string GroupID { get; set; }
    }

    public class ChatGroup
    {
        [JsonProperty("chatGroupId")]
        public long ChatGroupID { get; set; }

        //public long GroupId { get; set; }

        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        //public string Comments { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; } = false;

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        //public int UpdatedBy { get; set; }

        //public string updatedDate { get; set; }

    }

    public class ChatUserGroup
    {
        //[JsonProperty("chatGroupUserId")]
        //public long chatGroupUserId { get; set; }

        [JsonProperty("communicationId")]
        public string CommunicationID { get; set; }

        [JsonProperty("userId")]
        public long UserID { get; set; }

        [JsonProperty("chatGroupId")]
        public long ChatGroupID { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; } = false;

        //[JsonProperty("createdBy")]
        //public int CreatedBy { get; set; }

        //[JsonProperty("createdDate")]
        //public DateTime CreatedDate { get; set; }

        //Additional property needed
        [JsonIgnore]
        public string FirstName { get; set; }

        [JsonIgnore]
        public string LastName { get; set; }

        [JsonIgnore]
        public long ChatGroupUserID { get; set; }
    }

    public class CreateGroupResponse : BaseResponse
    {
        [JsonProperty("Result")]
        public CreateGroupResult CreateGroupResult { get; set; }
    }

    public class CreateGroupResult
    {
        [JsonProperty("ChatGroupId")]
        public long ChatGroupID { get; set; }

        [JsonProperty("GroupName")]
        public string GroupName { get; set; }

        [JsonProperty("GroupId")]
        public string GroupID { get; set; }
    }

    public class GroupUsersResponse : BaseResponse
    {
        [JsonProperty("EmployeeList")]
        public GroupUserObject GroupUserObject { get; set; }
    }

    public class GroupUserObject
    {
        [JsonProperty("ChatEmployeeList")]
        public List<GroupUser> Users { get; set; }
    }

    public class GroupUser
    {
        [JsonProperty("Id")]
        public long ID { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        //[JsonProperty("IsGroup")]
        //public bool IsGroup { get; set; }

        //[JsonProperty("ChatGroupId")]
        //public long ChatGroupID { get; set; }

        //[JsonProperty("GroupId")]
        //public string GroupID { get; set; }

        [JsonProperty("CommunicationId")]
        public string CommunicationID { get; set; }

        //[JsonProperty("RecentChatMessage")]
        //public string RecentChatMessage { get; set; }

        [JsonProperty("ChatGroupUserId")]
        public long ChatGroupUserID { get; set; }

        //[JsonProperty("CreatedDate")]
        //public string CreatedDate { get; set; }

        //[JsonProperty("IsRead")]
        //public bool IsRead { get; set; }

        //[JsonProperty("Selected")]
        //public bool Selected { get; set; }
    }

    public class ChatMessageRequest
    {
        [JsonProperty("SenderId")]
        public long? SenderID { get; set; }

        [JsonProperty("RecipientId")]
        public long? RecipientID { get; set; }

        [JsonProperty("GroupId")]
        public long GroupID { get; set; }
    }

    public class ChatMessagesResponse : BaseResponse
    {
        [JsonProperty("ChatMessage")]
        public ChatMessageObject ChatMessageObject { get; set; }
    }

    public class ChatMessageObject
    {
        [JsonProperty("ChatMessageDetail")]
        public List<ChatMessage> ChatMessageDetail { get; set; }
    }

    public class ChatMessage
    {
        [JsonProperty("SenderId")]
        public int SenderID { get; set; }

        [JsonProperty("SenderFirstName")]
        public string SenderFirstName { get; set; }

        [JsonProperty("SenderLastName")]
        public string SenderLastName { get; set; }

        [JsonProperty("ReceipientId")]
        public long ReceipientID { get; set; }

        [JsonProperty("RecipientFirstName")]
        public string RecipientFirstName { get; set; }

        [JsonProperty("RecipientLastName")]
        public string RecipientLastName { get; set; }

        [JsonProperty("MessageBody")]
        public string MessageBody { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        public long? ChatMsgId { get; set; }
    }

    public class SendChatMessageReq
    {
        [JsonProperty("chatMessage")]
        public ChatMessageBody ChatMessage { get; set; }

        [JsonProperty("chatMessageRecipient")]
        public ChatMessageRecipient ChatMessageRecipient { get; set; }

        //[JsonProperty("chatGroupRecipient")]
        //public List<ChatGroupRecipient> ChatGroupRecipient { get; set; }

        [JsonProperty("connectionId")]
        public string ConnectionID { get; set; }

        //[JsonProperty("fullName")]
        //public string FullName { get; set; }

        [JsonProperty("groupId")]
        public string GroupID { get; set; }

        //[JsonProperty("groupName")]
        //public string GroupName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }

    public class ChatMessageBody
    {
        //[JsonProperty("chatMessageId")]
        //public int ChatMessageID { get; set; }

        //[JsonProperty("subject")]
        //public string Subject { get; set; }

        [JsonProperty("messagebody")]
        public string Messagebody { get; set; }

        //[JsonProperty("parentChatMessageId")]
        //public int? ParentChatMessageID { get; set; }

        //[JsonProperty("expiryDate")]
        //public string ExpiryDate { get; set; }

        [JsonProperty("isReminder")]
        public bool? IsReminder { get; } = true;

        //[JsonProperty("nextRemindDate")]
        //public string NextRemindDate { get; set; }

        //[JsonProperty("reminderFrequencyId")]
        //public int? ReminderFrequencyId { get; set; }

        //[JsonProperty("createdBy")]
        //public long CreatedBy { get; } = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;
    }

    public class ChatMessageRecipient
    {
        //[JsonProperty("chatRecipientId")]
        //public long ChatRecipientID { get; set; }

        //[JsonProperty("CreatedchatMessageIdDate")]
        //public long? ChatMessageID { get; set; }

        [JsonProperty("senderId")]
        public long SenderID { get; } = AppSettings.UserID;

        [JsonProperty("recipientId")]
        public long? RecipientID { get; set; } = null;

        [JsonProperty("recipientGroupId")]
        public long? RecipientGroupID { get; set; } = null;

        //[JsonProperty("createdDate")]
        //public string CreatedDate { get; set; }

        //[JsonProperty("isRead")]
        //public bool? IsRead { get; set; }
    }

    public class ChatGroupRecipient
    {
        [JsonProperty("chatGroupRecipientId")]
        public int ChatGroupRecipientID { get; } = 0;

        [JsonProperty("chatGroupId")]
        public int ChatGroupID { get; set; }

        [JsonProperty("recipientId")]
        public int? RecipientID { get; set; }

        [JsonProperty("isRead")]
        public bool IsRead { get; } = false;

        //[JsonProperty("createdBy")]
        //public int CreatedBy { get; set; }

        //[JsonProperty("createdDate")]
        //public string CreatedDate { get; set; }
    }

    public class SendMessageResponse : BaseResponse
    {
        [JsonProperty("Status")]
        public bool Status { get; set; }
    }

    public class GetAvailableScheduleReq
    {
        [JsonProperty("locationId")]
        public int LocationID { get; set; }

        [JsonProperty("date")]
        public String CurrentDateTime { get; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public class AvailableScheduleResponse : BaseResponse
    {
        [JsonProperty("GetTimeInDetails")]
        public List<GetTimeInDetails> GetTimeInDetails { get; set; }
    }

    public class GetTimeInDetails
    {
        [JsonProperty("BayId")]
        public int BayID { get; set; }

        [JsonProperty("TimeIn")]
        public string TimeIn { get; set; }
    }

    public class GetDetailsSercviesResponse : BaseResponse
    {
        [JsonProperty("DetailsGrid")]
        public DetailsGrid DetailsGrid { get; set; }
    }

    public class DetailsGrid
    {
        [JsonProperty("BayJobDetailViewModel")]
        public List<DetailService> DetailServices { get; set; }
    }

    public class DetailService
    {
        [JsonProperty("BayId")]
        public int BayID { get; set; }

        [JsonProperty("BayName")]
        public string BayName { get; set; }

        [JsonProperty("JobId")]
        public int JobID { get; set; }

        [JsonProperty("TimeIn")]
        public string TimeIn { get; set; }

        [JsonProperty("ClientName")]
        public string ClientName { get; set; }

        [JsonProperty("PhoneNumber")]
        public long PhoneNumber { get; set; }

        [JsonProperty("EstimatedTimeOut")]
        public string EstimatedTimeOut { get; set; }

        [JsonProperty("Upcharge")]
        public string Upcharge { get; set; }

        [JsonProperty("VehicleColor")]
        public string VehicleColor { get; set; }

        [JsonProperty("VehicleModel")]
        public string VehicleModel { get; set; }

        [JsonProperty("VehicleMake")]
        public string VehicleMake { get; set; }

        [JsonProperty("ServiceTypeName")]
        public string ServiceTypeName { get; set; }

        [JsonProperty("OutsideService")]
        public string OutsideService { get; set; }

        [JsonProperty("JobDate")]
        public DateTime JobDate { get; set; }

        [JsonProperty("LocationId")]
        public long LocationID { get; set; }

        [JsonProperty("LocationName")]
        public string LocationName { get; set; }

        [JsonProperty("JobItemId")]
        public long JobItemID { get; set; }

        [JsonProperty("Cost")]
        public float Cost { get; set; }

        [JsonProperty("Barcode")]
        public string Barcode { get; set; }
    }

    public class LastServiceResponse : BaseResponse
    {
        [JsonProperty("WashesDetail")]
        public LastServiceDetail LastServiceDetail { get; set; }
    }

    public class LastServiceDetail
    {
        [JsonProperty("Washes")]
        public Service[] Services { get; set; }

        [JsonProperty("WashItem")]
        public List<LastServiceJobItem> JobItmes { get; set; }
    }

    public class LastServiceJobItem
    {
        [JsonProperty("ServiceName")]
        public string ServiceName { get; set; }

        [JsonProperty("ServiceType")]
        public string ServiceType { get; set; }
    }

    public class Service
    {
        [JsonProperty("JobTypeName")]
        public string JobTypeName { get; set; }

        [JsonProperty("ReviewNote")]
        public string ReviewNote { get; set; }

        [JsonProperty("VehicleModel")]
        public string VehicleModel { get; set; }

        [JsonProperty("VehicleMake")]
        public string VehicleMake { get; set; }

        [JsonProperty("VehicleColor")]
        public string VehicleColor { get; set; }

        [JsonProperty("Barcode")]
        public string Barcode { get; set; }

        [JsonProperty("JobDate")]
        public DateTime JobDate { get; set; }

        [JsonProperty("TimeIn")]
        public DateTime TimeIn { get; set; }

        [JsonProperty("EstimatedTimeOut")]
        public DateTime EstimatedTimeOut { get; set; }

        [JsonProperty("ClientName")]
        public string CustName { get; set; }
    }

    public class GetUpchargeReq
    {
        [JsonProperty("upchargeServiceType")]
        public long UpchargeServiceTypeID { get; set; }

        [JsonProperty("ModelId")]
        public long ModelID { get; set; }
    }

    public class UpchargeResponse : BaseResponse
    {
        [JsonProperty("upcharge")]
        public Upcharge[] Upcharges { get; set; }
    }

    public class Upcharge
    {
        [JsonProperty("ServiceId")]
        public long ServiceID { get; set; }

        [JsonProperty("ServiceName")]
        public string ServiceName { get; set; }

        [JsonProperty("Upcharges")]
        public string Upcharges { get; set; }

        [JsonProperty("Price")]
        public float Price { get; set; }

        [JsonProperty("ServiceTypeId")]
        public long ServiceTypeID { get; set; }
    }

    public class LocationWashTimeResponse : BaseResponse
    {
        [JsonProperty("Washes")]
        public List<Location> Locations { get; set; }
    }

    public class AssignEmployeeToServiceReq
    {
        [JsonProperty("jobId")]
        public int JobID { get; set; }

        [JsonProperty("jobServiceEmployee")]
        public List<AssignEmployeeToService> JobServiceEmployees { get; set; }
    }

    public class AssignEmployeeToService
    {
        [JsonProperty("jobItemId")]
        public long JobItemID { get; set; }

        [JsonProperty("ServiceId")]
        public long ServiceID { get; set; }

        [JsonProperty("JobServiceEmployeeId")]
        public long JobServiceEmployeeId { get; } = 0;

        [JsonProperty("employeeId")]
        public long EmployeeID { get; set; }

        [JsonProperty("commissionAmount")]
        public float CommissionAmount { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; } = false;
    }

    //public class LocWahTime
    //{
    //    [JsonProperty("WashtimeMinutes")]
    //    public int WashtimeMinutes { get; set; }
    //}

    public class DetailServiceResponse : BaseResponse
    {
        [JsonProperty("DetailsForDetailId")]
        public DetailsForDetailID DetailsForDetailID { get; set; }
    }

    public class DetailsForDetailID
    {
        [JsonProperty("DetailsItem")]
        public List<DetailsItem> DetailsItems { get; set; }
    }

    public class DetailsItem
    {
        [JsonProperty("JobItemId")]
        public long JobItemID { get; set; }

        [JsonProperty("ServiceId")]
        public long ServiceID { get; set; }

        [JsonProperty("CommissionType")]
        public string CommissionType { get; set; }

        [JsonProperty("CommissionCost")]
        public float CommissionCost { get; set; }

        [JsonProperty("Price")]
        public float Price { get; set; }
    }

    public class MembershipResponse : BaseResponse
    {
        [JsonProperty("VehicleMembershipDetails")]
        public VehicleMembershipDetail VehicleMembershipDetail;
    }

    //-----Vehicle Issue Feature

    public class VehicleIssueResponse : BaseResponse
    {
        [JsonProperty("VehicleIssueThumbnail")]
        public VehicleIssueThumbnail VehicleIssueThumbnail;
    }
    public class DeleteResponse: BaseResponse
    {
        [JsonProperty("Status")]
        public bool status;
    }

    
    public class VehicleImageResponse:BaseResponse
    {
        [JsonProperty("VehicleImage")]
        public VehicleImage VehicleImage;
    }
    public class VehicleImage
    {
        [JsonProperty("VehicleIssueImageId")]
        public int VehicleIssueImageId { get; set; }
        [JsonProperty("VehicleIssueId")]
        public int VehicleIssueId { get; set; }
        [JsonProperty("ImageName")]
        public string ImageName { get; set; }
        [JsonProperty("CreatedDate")]
        public string CreatedDate { get; set; }
        [JsonProperty("OriginalImageName")]
        public string OriginalImageName { get; set; }
        [JsonProperty("ThumbnailFileName")]
        public string ThumbnailFileName { get; set; }
        [JsonProperty("Base64Thumbnail")]
        public string Base64Thumbnail { get; set; }
        [JsonProperty("Base64")]
        public string Base64 { get; set; }
    }
    public class VehicleIssueThumbnail
    {
        [JsonProperty("VehicleIssue")]
        public List<VehicleIssue> VehicleIssue;
        [JsonProperty("VehicleIssueImage")]
        public List<VehicleIssueImage> VehicleIssueImage;
    }
    public class VehicleIssueImage
    {
        [JsonProperty("VehicleIssueImageId")]
        public int VehicleIssueImageId { get; set; }
        [JsonProperty("VehicleIssueId")]
        public int VehicleIssueId { get; set; }
        [JsonProperty("ImageName")]
        public string ImageName { get; set; }
        [JsonProperty("CreatedDate")]
        public string CreatedDate { get; set; }
        [JsonProperty("OriginalImageName")]
        public string OriginalImageName { get; set; }
        [JsonProperty("ThumbnailFileName")]
        public string ThumbnailFileName { get; set; }
        [JsonProperty("Base64Thumbnail")]
        public string Base64Thumbnail { get; set; }
        [JsonProperty("Base64")]
        public string Base64 { get; set; }
    }
    public class VehicleIssue
    {
        [JsonProperty("VehicleIssueId")]
        public int VehicleIssueid { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("CreatedDate")]
        public string CreatedDate { get; set; }
        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; } 

    }
    public class VehicleIssueAddRequest
    {

        [JsonProperty("vehicleIssue")]
        public vehicleIssue vehicleIssue;
        [JsonProperty("vehicleIssueImage")]
        public List<vehicleIssueImage> vehicleIssueImage;
    }

    public class vehicleIssueImage
    {
        [JsonProperty("vehicleIssueImageId")]
        public int vehicleIssueImageId { get; set; }
        [JsonProperty("vehicleIssueId")]
        public int vehicleIssueId { get; set; }
        [JsonProperty("imageName")]
        public string imageName { get; set; }
        [JsonProperty("createdDate")]
        public string createdDate { get; set; }
        [JsonProperty("originalImageName")]
        public string originalImageName { get; set; }
        [JsonProperty("thumbnailFileName")]
        public string thumbnailFileName { get; set; }
        [JsonProperty("base64Thumbnail")]
        public string base64Thumbnail { get; set; }
        [JsonProperty("base64")]
        public string base64 { get; set; }
        [JsonProperty("filePath")]
        public string filePath{ get; set; }
        [JsonProperty("documentType")]
        public string documentType{ get; set; }
        [JsonProperty("isActive")]
        public bool isActive { get; set; }
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
        [JsonProperty("createdBy")]
        public int createdBy { get; set; }
        [JsonProperty("updatedDate")]
        public string updatedDate { get; set; }
        [JsonProperty("updatedBy")]
        public int updatedBy { get; set; }
    }

    public class vehicleIssue
    {
        [JsonProperty("vehicleIssueId")]
        public int vehicleIssueid { get; set; }
        [JsonProperty("vehicleId")]
        public long vehicleId { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("createdDate")]
        public string createdDate { get; set; }
        [JsonProperty("createdBy")]
        public int createdBy { get; set; }
        [JsonProperty("updatedDate")]
        public string updatedDate { get; set; }
        [JsonProperty("updatedBy")]
        public int updatedBy { get; set; }
        [JsonProperty("isActive")]
        public bool isActive { get; set; }
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
    //---Vehicle Issue Feature
    public class VehicleMembershipDetail
    {
        [JsonProperty("ClientVehicle")]
        public ClientVehicle ClientVehicle;

        [JsonProperty("ClientVehicleMembership")]
        public ClientVehicleMembership ClientVehicleMembership;

        [JsonProperty("ClientVehicleMembershipService")]
        public ClientVehicleMembershipService[] ClientVehicleMembershipServices;
    }

    public class ClientVehicle
    {
        [JsonProperty("Upcharge")]
        public long UpchargeID;
    }

    public class ClientVehicleMembership
    {
        [JsonProperty("ClientMembershipId")]
        public long ClientMembershipId;
    }

    public class ClientVehicleMembershipService
    {
        [JsonProperty("ServiceId")]
        public long ServiceId;

        [JsonProperty("ServiceTypeId")]
        public long ServiceTypeId;

        [JsonProperty("ServiceType")]
        public string ServiceType;
    }

    public class SendEmailReq
    {
        [JsonProperty("email")]
        public string EmailId;

        [JsonProperty("subject")]
        public string Subject;

        [JsonProperty("body")]
        public string BodyHtml;
    }

    public class ChatCommunication
    {
        [JsonProperty("employeeId")]
        public long EmpID  { get; set; }

        [JsonProperty("communicationId")]
        public string CommunicationID { get; set; }
    }

    public class LogReq
    {
        [JsonProperty("log")]
        public string Log { get; set; }

        [JsonProperty("Environment")]
        public string Environment { get; set; }
    }

    //Printer Related Models--Starting

    public class PrinterIp :BaseResponse
    {
        public PrinterDetail PrinterDetail { get; set; }
    }
    public class PrinterDetail
    {
        [JsonProperty("IpAddress")]
        public string IpAddress { get; set; }
        [JsonProperty("PrinterName")]
        public string PrinterName { get; set; }
    }
    public class TicketModel : BaseResponse
    {
        [JsonProperty("PrintJobDetail")]
        public  PrintJobDetail PrintJobDetail{ get; set; }
    }

    public class PrintJobDetail
    {
        [JsonProperty("Job")]
        public PrintJob Job { get; set; }

        [JsonProperty("JobItem")]
        public List<PrintJobItem> JobItem { get; set; }

        [JsonProperty("ClientDetail")]
        public PrintClientInfo ClientDetail { get; set; }
    }

    public class PrintJobItem
    {
        [JsonProperty("JobItemId")]
        public int JobItemId { get; set; }
        [JsonProperty("serviceName")]
        public string serviceName { get; set; }
        [JsonProperty("ServiceId")]
        public int ServiceId { get; set; }
        [JsonProperty("Price")]
        public long Price { get; set; }
        [JsonProperty("ServiceType")]
        public string ServiceType { get; set; }
    }
    public class PrintClientInfo
    {
        
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
    }

    public class PrintJob
    {
        [JsonProperty("JobId")]
        public int JobId { get; set; }
        [JsonProperty("TicketNumber")]
        public string TicketNumber { get; set; }
        [JsonProperty("TimeIn")]
        public string TimeIn { get; set; }
        [JsonProperty("TimeOut")]
        public string TimeOut { get; set; }
        [JsonProperty("EstimatedTimeOut")]
        public string EstimatedTimeOut { get; set; }
        [JsonProperty("Barcode")]
        public string Barcode { get; set; }
        [JsonProperty("VehicleModel")]
        public string VehicleModel { get; set; }
        [JsonProperty("VehicleMake")]
        public string VehicleMake { get; set; }
        [JsonProperty("VehicleColor")]
        public string VehicleColor { get; set; }
        [JsonProperty("JobDate")]
        public string JobDate { get; set; }
        [JsonProperty("JobType")]
        public string JobType { get; set; }
        [JsonProperty("JobStatus")]
        public string JobStatus { get; set; }
        [JsonProperty("notes")]
        public string notes { get; set; }

    }


    public class PrintContentRequest
    {
        [JsonProperty("job")]
        public printJob job { get; set; }

        [JsonProperty("jobItem")]
        public List<printJobItem> jobItem { get; set; }

        [JsonProperty("clientInfo")]
        public printClientInfo clientInfo { get; set; }

    }
    public class printJob
    {
        [JsonProperty("ticketNumber")]
        public string ticketNumber { get; set; }
        [JsonProperty("inTime")]
        public string inTime { get; set; }
        [JsonProperty("timeOut")]
        public string timeOut { get; set; }
        [JsonProperty("barcode")]
        public string? barcode { get; set; }
        [JsonProperty("vehicleModel")]
        public string vehicleModel { get; set; }
        [JsonProperty("vehicleMake")]
        public string vehicleMake { get; set; }
        [JsonProperty("vehicleColor")]
        public string vehicleColor { get; set; }
    }
    public class printJobItem
    {
        [JsonProperty("serviceName")]
        public string serviceName { get; set; }
        
        [JsonProperty("price")]
        public long price { get; set; }
        [JsonProperty("serviceType")]
        public string serviceType { get; set; }
    }
    public class printClientInfo
    {
        [JsonProperty("firstName")]
        public string firstName { get; set; }
        [JsonProperty("lastName")]
        public string lastName { get; set; }
        [JsonProperty("phoneNumber")]
        public string phoneNumber { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("clientName")]
        public string clientName { get; set; }
    }
    public class GeneratedTicket : BaseResponse
    {
        [JsonProperty("VehiclePrint")]
        public string VehiclePrint { get; set; }

        [JsonProperty("CustomerPrint")]
        public string CustomerPrint { get; set; }
    }

    //Printer Related Models--Ending

}
