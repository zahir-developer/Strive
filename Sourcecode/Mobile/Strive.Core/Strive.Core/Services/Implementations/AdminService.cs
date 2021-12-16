using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MvvmCross;
using Newtonsoft.Json;
using Strive.Core.Models;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.Employee.Collisions;
using Strive.Core.Models.Employee.Common;
using Strive.Core.Models.Employee.Documents;
using Strive.Core.Models.Employee.PayRoll;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Models.Owner;
using Strive.Core.Models.TimInventory;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;
using EditProduct = Strive.Core.Models.TimInventory.Product_Id;

namespace Strive.Core.Services.Implementations
{
    public class AdminService : IAdminService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();

        public AdminService()
        {
        }

        public async Task<object> Login(string username, string password)
        {
            return await _restClient.MakeApiCall<object>(ApiUtils.URL_LOGIN_EMPLOYEE, HttpMethod.Post, new User());
        }

        public async Task<EmployeeResultData> EmployeeLogin(EmployeeLoginRequest request)
        {
            return await _restClient.MakeApiCall<EmployeeResultData>(ApiUtils.URL_LOGIN_EMPLOYEE, HttpMethod.Post, request);
        }

        public async Task<CustomerLoginResponse> CustomerLogin(CustomerLoginRequest loginRequest)
        {
            return await _restClient.MakeApiCall<CustomerLoginResponse>(ApiUtils.URL_LOGIN_EMPLOYEE, HttpMethod.Post, loginRequest);
        }

        public async Task<CustomerResponse> CustomerSignUp(CustomerSignUp signUpRequest)
        {
            return await _restClient.MakeApiCall<CustomerResponse>(ApiUtils.URL_CUST_SIGN_UP, HttpMethod.Post, signUpRequest);
        }

        public async Task<CustomerResponse> CustomerForgotPassword(string emailID)
        {
            var uriBuilder = new UriBuilder(ApiUtils.URL_CUST_FORGOT_PASSWORD);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["emailId"] = emailID;
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.Uri.PathAndQuery.ToString();
            return await _restClient.MakeApiCall<CustomerResponse>(url, HttpMethod.Put);            
        }

        public async Task<CustomerResponse> CustomerConfirmPassword(CustomerResetPassword resetPasswordRequest)
        {
            return await _restClient.MakeApiCall<CustomerResponse>(ApiUtils.URL_CUST_CONFIRM_PASSWORD, HttpMethod.Post, resetPasswordRequest);
        }

        public async Task<CustomerResponse> CustomerVerifyOTP(CustomerVerifyOTPRequest otpRequest)
        {
            var uriBuilder = new UriBuilder(ApiUtils.URL_CUST_VERIFY_OTP);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["emailId"] = otpRequest.emailId;
            query["otp"] = otpRequest.otp;
            uriBuilder.Query = query.ToString();    
            var url = uriBuilder.Uri.PathAndQuery.ToString();
            return await _restClient.MakeApiCall<CustomerResponse>(url, HttpMethod.Get);
        }

        public async Task<TimeClockRootList> GetClockInStatus(TimeClockRequest request)
        {
            //var uriBuilder = new UriBuilder(ApiUtils.URL_GET_CLOCKIN_STATUS);
            //var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            //query["userId"] = Id.ToString();
            //query["dateTime"] = Datetime;
            //uriBuilder.Query = query.ToString();
            //var url = uriBuilder.Uri.PathAndQuery.ToString();
            return await _restClient.MakeApiCall<TimeClockRootList>(ApiUtils.URL_GET_CLOCKIN_STATUS, HttpMethod.Post,request);
        }

        public async Task<DeleteResponse> SaveClockInTime(TimeClockSave ClockInRequest)
        {
            return await _restClient.MakeApiCall<DeleteResponse>(ApiUtils.URL_SAVE_CLOCKIN_TIME, HttpMethod.Post, ClockInRequest);
        }

        public async Task<DealsList> GetAllDeals()
        {
            return await _restClient.MakeApiCall<DealsList>(ApiUtils.URL_GET_ALLDEALS, HttpMethod.Get);
        }

        public async Task<ClientDeals> GetClientDeal(int clientid, string date, int dealid)
        {
            var url = ApiUtils.URL_GET_CLIENT_DEALS + "?ClientId="+clientid+"&Date="+date+"&DealId="+dealid;
            return await _restClient.MakeApiCall<ClientDeals>(url, HttpMethod.Get);

        }


        public async Task<Products> GetAllProducts(ProductSearches searchQuery)
        {
            return await _restClient.MakeApiCall<Products>(ApiUtils.URL_GET_ALL_PRODUCTS, HttpMethod.Post, searchQuery);
        }

        public async Task<Vendors> GetAllVendors()
        {
            return await _restClient.MakeApiCall<Vendors>(ApiUtils.URL_GET_ALL_VENDORS, HttpMethod.Get);
        }

        public async Task<LocationProd> GetAllLocationName()
        {
            return await _restClient.MakeApiCall<LocationProd>(ApiUtils.URL_GET_ALL_LOCATION_NAME, HttpMethod.Get);
        }

        public async Task<ProductType> GetProductType()
        {
            return await _restClient.MakeApiCall<ProductType>(ApiUtils.URL_GET_PRODUCTTYPE, HttpMethod.Get);
        }

        public async Task<ProductType> GetCodes()
        {
            return await _restClient.MakeApiCall<ProductType>(ApiUtils.URL_GET_ALLCODES, HttpMethod.Get);
        }

        public async Task<PostResponse> AddProduct(AddProduct product)
        {
            return await _restClient.MakeApiCall<PostResponse>(ApiUtils.URL_ADD_PRODUCT, HttpMethod.Post,product);
        }

        public async Task<DeleteResponse> DeleteProduct(int Id)
        {
            var uriBuilder = new UriBuilder(ApiUtils.URL_DELETE_PRODUCT);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["productId"] = Id.ToString();
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.Uri.PathAndQuery.ToString();
            return await _restClient.MakeApiCall<DeleteResponse>(url, HttpMethod.Delete);
        }

        public async Task<EditProduct.ProductDetail_Id> GetProductByID(int Id)
        {
            var uriBuilder = new UriBuilder(ApiUtils.URL_GET_PRODUCTDETAIL_BYID);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["productId"] = Id.ToString();
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.Uri.PathAndQuery.ToString();
            var result = await _restClient.MakeApiCall<EditProduct.ProductDetail_Id>(url, HttpMethod.Get);
            return result;
        }

        public async Task<PostResponse> UpdateProduct(AddProduct product)
        {
            return await _restClient.MakeApiCall<PostResponse>(ApiUtils.URL_UPDATE_PRODUCT, HttpMethod.Post, product);
        }

        public async Task<PostResponse> ProductRequest(ProductRequest product)
        {
            return await _restClient.MakeApiCall<PostResponse>(ApiUtils.URL_PRODUCT_REQUEST, HttpMethod.Post, product);
        }

        public async Task<PostResponse> UpdateProdQuantity(int Id, int quantity)
        {
            var uriBuilder = ApiUtils.URL_UPDATE_PRODUCT_QUANTITY;
            var string1 = "?ProductId=" + Id;
            var string2 = "&Quantity=" + quantity;

            var url = uriBuilder + string1 + string2;

            var result = await _restClient.MakeApiCall<PostResponse>(url, HttpMethod.Post);
            return result;
        }
        public async Task<ClientResponse> GetAllClient(ClientRequest clientRequest)
        {
            return await _restClient.MakeApiCall<ClientResponse>(ApiUtils.URL_GET_ALL_CLIENT, HttpMethod.Post, clientRequest);
        }

        public async Task<ProductsSearch> SearchProduct(string productName)
        {
            var data = new { productSearch = productName};
            return await _restClient.MakeApiCall<ProductsSearch>(ApiUtils.URL_SEARCH_PRODUCT, HttpMethod.Post, data);
        }

        public async Task<modelUpchargeResponse> GetModelUpcharge(modelUpcharge request)
        {
            return await _restClient.MakeApiCall<modelUpchargeResponse>(ApiUtils.URL_MODEL_UPCHARGE, HttpMethod.Post, request);
        }

        public async Task<MembershipServiceList> GetMembershipServiceList()
        {
            return await _restClient.MakeApiCall<MembershipServiceList>(ApiUtils.URL_GET_ALL_SERVICE, HttpMethod.Get);
        }

        public async Task<PostResponseBool> SaveVehicleMembership(ClientVehicleRoot clientVehicle)
        {
            return await _restClient.MakeApiCall<PostResponseBool>(ApiUtils.URL_SAVE_VEHICLE_MEMBERSHIP, HttpMethod.Post, clientVehicle);
        }

        public async Task<PostResponse> DeleteVehicleMembership(deleteMembership deleteMembership)
        {
            return await _restClient.MakeApiCall<PostResponse>(ApiUtils.URL_DELETE_VEHICLE_MEMBERSHIP, HttpMethod.Post, deleteMembership);
        }

        public async Task<ClientStatus> GetClientDetail(int ClientId)
        {
            var url = ApiUtils.URL_GET_CLIENT_DETAIL + ClientId;
            return await _restClient.MakeApiCall<ClientStatus>(url, HttpMethod.Get);
        }

        public async Task<VehicleList> GetClientVehicle(int ClientId)
        {
            var url = RestUtils.BuildQuery(ApiUtils.URL_GET_CLIENT_VEHICLE, "id", ClientId);
            return await _restClient.MakeApiCall<VehicleList>(url, HttpMethod.Get);
        }

        public async Task<ClientVehicleRootView> GetVehicleMembership(int VehicleId)
        {
            var url = RestUtils.BuildQuery(ApiUtils.URL_GET_CLIENT_VEHICLE_MEMBERSHIP, "id", VehicleId);
            return await _restClient.MakeApiCall<ClientVehicleRootView>(url, HttpMethod.Get);
        }

        public async Task<ServiceList> GetVehicleServices()
        {
            return await _restClient.MakeApiCall<ServiceList>(ApiUtils.URL_GET_CLIENT_VEHICLE_SERVICES, HttpMethod.Get);
        }

        public async Task<SelectedServiceList> GetSelectedMembershipServices(int MembershipId)
        {
            return await _restClient.MakeApiCall<SelectedServiceList>(ApiUtils.URL_GET_SELECTED_MEMBERSHIP_SERVICES+MembershipId, HttpMethod.Get);
        }

        public async Task<ClientsSearch> SearchClient(string ClientName)
        {
            var data = new { clientName = ClientName};
            return await _restClient.MakeApiCall<ClientsSearch>(ApiUtils.URL_SEARCH_CLIENT, HttpMethod.Post,data);
        }

        public async Task<CustomerPersonalInfo> GetClientById(int Id)
        {
            return await _restClient.MakeApiCall<CustomerPersonalInfo>(string.Format(ApiUtils.URL_GET_CLIENT_BY_ID, Id), HttpMethod.Get, Id);
        }

        public async Task<CustomerResponse> SaveClientInfo(CustomerUpdateInfo infoModel)
        {
            return await _restClient.MakeApiCall<CustomerResponse>(ApiUtils.URL_SAVE_CLIENT_INFO, HttpMethod.Post, infoModel);
        }

        public async Task<VehicleCodes> GetVehicleCodes()
        {
            return await _restClient.MakeApiCall<VehicleCodes>(ApiUtils.URL_GET_VEHICLE_CODES, HttpMethod.Post);
        }

        public async Task<MakeList> GetMakeList()
        {
            return await _restClient.MakeApiCall<MakeList>(ApiUtils.URL_GET_MAKE_LIST, HttpMethod.Get);
        }

        public async Task<ModelList> GetModelList(int Id)
        {
            return await _restClient.MakeApiCall<ModelList>(string.Format(ApiUtils.URL_GET_MODEL_LIST, Id), HttpMethod.Get, Id);
        }

        public async Task<CustomerResponse> SaveClientInfo(CustomerInfoModel infoModel)
        {
            return await _restClient.MakeApiCall<CustomerResponse>(ApiUtils.URL_SAVE_CLIENT_INFO, HttpMethod.Post, infoModel);
        }
        public async Task<GeneralResponse> AddCustomerVehicle(CustomerVehicles addVehicle)
        {
            return await _restClient.MakeApiCall<GeneralResponse>(ApiUtils.URL_ADD_VEHICLE_INFO, HttpMethod.Post, addVehicle);
        }

        public async Task<GeneralResponse> DeleteCustomerVehicle(int VehicleID)
        {
            var url = RestUtils.BuildQuery(ApiUtils.URL_DELETE_VEHICLE_INFO, "id", VehicleID);
            return await _restClient.MakeApiCall<GeneralResponse>(url, HttpMethod.Delete);
        }

        public async Task<CustomerCompleteDetails> GetVehicleCompleteDetails(int VehicleID)
        {
            var url = RestUtils.BuildQuery(ApiUtils.URL_GET_VEHICLE_COMPLETE_DETAILS, "id", VehicleID);
            return await _restClient.MakeApiCall<CustomerCompleteDetails>(url, HttpMethod.Get);
        }

        public async Task<PastClientServices> GetPastClientServices(int ClientID)
        {
            return await _restClient.MakeApiCall<PastClientServices>(string.Format(ApiUtils.URL_PAST_SERVICES_INFO, ClientID), HttpMethod.Get);
        }

        public async Task<PersonalDetails> GetPersonalDetails(int EmployeeID)
        {
            return await _restClient.MakeApiCall<PersonalDetails>(ApiUtils.URL_MESSENGER_PERSONAL_INFO+"?id="+EmployeeID, HttpMethod.Get, EmployeeID);
        }

        public async Task<CommonCodes> GetCommonCodes(string CommonCode)
        {
           return await _restClient.MakeApiCall<CommonCodes>(ApiUtils.URL_COMMON_TYPES + CommonCode,HttpMethod.Get);
        }

        public async Task<PostResponse> AddCollisions(AddCollisions collisionDetails)
        {
            return await _restClient.MakeApiCall<PostResponse>(ApiUtils.URL_ADD_COLLISIONS, HttpMethod.Post, collisionDetails);
        }

        public async Task<PostResponse> UpdateCollisions(AddCollisions collisionDetails)
        {
            return await _restClient.MakeApiCall<PostResponse>(ApiUtils.URL_UPDATE_COLLISIONS, HttpMethod.Post, collisionDetails);
        }

        public async Task<PostResponse> DeleteCollisions(int liabilityID)
        {
            return await _restClient.MakeApiCall<PostResponse>(ApiUtils.URL_DELETE_COLLISIONS+"?id="+liabilityID, HttpMethod.Delete);
        }

        public async Task<GetCollisions> GetCollisions(int liabilityID)
        {
            return await _restClient.MakeApiCall<GetCollisions>(ApiUtils.URL_GET_COLLISIONS + liabilityID, HttpMethod.Get);
        }

        public async Task<PostResponseBool> SaveDocuments(AddDocuments documents)
        {
            return await _restClient.MakeApiCall<PostResponseBool>(ApiUtils.URL_EMPLOYEE_DOCUMENTS_ADD, HttpMethod.Post, documents);
        }

        public async Task<CheckoutDetails> CheckOutVehicleDetails(Models.Employee.Messenger.MessengerContacts.GetAllEmployeeDetail_Request CheckoutInfo)
        {
            return await _restClient.MakeApiCall<CheckoutDetails>(ApiUtils.URL_CHECKOUT_DETAILS, HttpMethod.Post, CheckoutInfo);
        }

        public async Task<holdCheckoutResponse> CheckOutHold(holdCheckoutReq request)
        {
            return await _restClient.MakeApiCall<holdCheckoutResponse>(ApiUtils.URL_CHECKOUT_HOLD, HttpMethod.Post, request);
        }

        public async Task<holdCheckoutResponse> CheckOutComplete(completeCheckoutReq request)
        {
            return await _restClient.MakeApiCall<holdCheckoutResponse>(ApiUtils.URL_CHECKOUT_COMPLETE, HttpMethod.Post, request);
        }

        public async Task<CheckoutResponse> DoCheckout(doCheckoutReq request)
        {
            return await _restClient.MakeApiCall<CheckoutResponse>(ApiUtils.URL_CHECKOUT_UPDATE, HttpMethod.Post, request);
        }

        public async Task<ScheduleModel> GetSchedulePastService(int clientID)
        {
            return await _restClient.MakeApiCall<ScheduleModel>(ApiUtils.URL_SCHEDULE_PAST_SERVICE+ "?ClientId=" +clientID, HttpMethod.Get);
        }

        public async Task<PostResponse> ScheduleDetail(DetailSchedule request)
        {
            Console.WriteLine("ScheduleDetail:" + Newtonsoft.Json.JsonConvert.SerializeObject(request, Formatting.Indented));
            Console.WriteLine("ScheduleDetail:" + "End");

            return await _restClient.MakeApiCall<PostResponse>(ApiUtils.URL_SCHEDULE_DETAILBAY, HttpMethod.Post, request);
        }

        public async Task<AvailableServicesModel> GetScheduleServices(int LocationID)
        {
            return await _restClient.MakeApiCall<AvailableServicesModel>(ApiUtils.URL_GET_CLIENT_VEHICLE_SERVICES_LIST+ "?locationId="+LocationID, HttpMethod.Get);
        }

        public async Task<AvailableScheduleSlots> GetScheduleSlots(ScheduleSlotInfo slotInfo )
        {
            return await _restClient.MakeApiCall<AvailableScheduleSlots>(ApiUtils.URL_SCHEDULE_TIME_SLOTS, HttpMethod.Post, slotInfo);
        }

        public async Task<ticketNumber> GetTicketNumber(int locationId)
        {
            return await _restClient.MakeApiCall<ticketNumber>(string.Format(ApiUtils.URL_GET_TICKET_NUMBER, locationId), HttpMethod.Get, locationId);
        }

        public async Task<DownloadDocuments> DownloadDocuments(int documentID, string documentPassword)
        {
            return await _restClient.MakeApiCall<DownloadDocuments>(ApiUtils.URL_EMPLOYEE_DOCUMENTS_DOWNLOAD+documentID+","+documentPassword, HttpMethod.Get);
        }
        public async Task<TermsDocument> TermsDocuments(int doctypeid, string doctype)
        {
            var path = ApiUtils.URL_GET_TERMS_AND_CONDITIONS + doctypeid + "/" + doctype+ "?documentSubType=34551";
            return await _restClient.MakeApiCall<TermsDocument>(path, HttpMethod.Get);
            
            
        }


        public async Task<DeleteResponse> DeleteDocuments(int documentID)
        {
            return await _restClient.MakeApiCall<DeleteResponse>(ApiUtils.URL_EMPLOYEE_DOCUMENTS_DELETE + documentID, HttpMethod.Delete);
        }

        public async Task<PostResponseBool> UpdateEmployeePersonalDetails(UpdatePersonalDetails employeeInfo)
        {
            return await _restClient.MakeApiCall<PostResponseBool>(ApiUtils.URL_UPDATE_EMPLOYEE_PERSONAL_DETAILS, HttpMethod.Post, employeeInfo);
        }

        public async Task<employeeSchedule> GetScheduleList(ScheduleRequest scheduleRequest)
        {
            return await _restClient.MakeApiCall<employeeSchedule>(ApiUtils.URL_GET_EMPLOYEE_SCHEDULE, HttpMethod.Post, scheduleRequest);
        }

        public async Task<StatisticsData> getDashboardServices(StatisticRequest statisticsRequest)
        {
            return await _restClient.MakeApiCall<StatisticsData>(ApiUtils.URL_GET_DASHBOARD_STATISTICS, HttpMethod.Post, statisticsRequest);
        }


        public async Task<VehicleDiscountDetail> GetVehicleDiscountDetail(int ClientId,int VehicleID)
        {

            return await _restClient.MakeApiCall<VehicleDiscountDetail>(ApiUtils.URL_GET_CLIENT_VEHICLE_SERVICES_DISCOUNT+ "/" + ClientId + "/" + VehicleID, HttpMethod.Get);

        }
        public async Task<PayRollRateViewModel> GetPayRollStatus(string StartDate, string EndDate, int EmpId, int LocationId)
        {
            var url = ApiUtils.URL_GET_PAYROLL_STATUS + "?empid=" + EmpId + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&Locationid=" + LocationId;
            return await _restClient.MakeApiCall<PayRollRateViewModel>(url,HttpMethod.Get);
        }
        public async Task<PayRoll> GetPayRoll(string StartDate, string EndDate, int EmpId, int LocationId)
        {
            var url = ApiUtils.URL_GET_PAYROLL + "?Locationid=" + LocationId + "&StartDate=" + StartDate + "&EndDate=" + EndDate+ "&empid = " + EmpId ;
            return await _restClient.MakeApiCall<PayRoll>(url, HttpMethod.Get);
        }
        public async Task<ScheduleModel> getDashboardSchedule(string jobDate, int locationId)
        {
            var uriBuilder = ApiUtils.URL_SCHEDULE_PAST_SERVICE;
            var string1 = "?JobDate=" + jobDate;
            var string2 = "&LocationId=" + locationId;

            var url = uriBuilder + string1 + string2;

            return await _restClient.MakeApiCall<ScheduleModel>(url, HttpMethod.Get);
        }
        public async Task<UploadedDocument> AddDocumentDetails(AddDocument addDocument)
        {
            return await _restClient.MakeApiCall<UploadedDocument>(ApiUtils.URL_ADD_DOCUMENT, HttpMethod.Post,addDocument);
        }
        public async Task<ProductType> GetCodesByCategory()
        {
            return await _restClient.MakeApiCall<ProductType>(ApiUtils.URL_GET_CODES_BY_CATEGORY, HttpMethod.Get);
        }

        public async Task<AddClientDealResponse> AddClientDeal(AddClientDeal clientDeal)
        {
            return await _restClient.MakeApiCall<AddClientDealResponse>(ApiUtils.URL_ADD_CLIENT_DEAL, HttpMethod.Post,clientDeal);
        }

    }
    public static class RestUtils
    {
        public static string BuildQuery (string uri,string parameter, int value)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[parameter] = value.ToString();
            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri.PathAndQuery.ToString();
        }

       
    }
}

