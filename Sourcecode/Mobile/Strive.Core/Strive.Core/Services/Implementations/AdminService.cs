using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MvvmCross;
using Strive.Core.Models;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.Employee.Collisions;
using Strive.Core.Models.Employee.Common;
using Strive.Core.Models.Employee.Documents;
using Strive.Core.Models.Employee.PersonalDetails;
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

        public async Task<EmployeeResultData> CustomerLogin(CustomerLoginRequest loginRequest)
        {
            return await _restClient.MakeApiCall<EmployeeResultData>(ApiUtils.URL_LOGIN_EMPLOYEE, HttpMethod.Post, loginRequest);
        }

        public async Task<CustomerResponse> CustomerSignUp(CustomerSignUp signUpRequest)
        {
            return await _restClient.MakeApiCall<CustomerResponse>(ApiUtils.URL_CUST_SIGN_UP, HttpMethod.Post, signUpRequest);
        }

        public async Task<CustomerResponse> CustomerForgotPassword(string emailID)
        {
            return await _restClient.MakeApiCall<CustomerResponse>(string.Format(ApiUtils.URL_CUST_FORGOT_PASSWORD,emailID),HttpMethod.Put,emailID);
        }

        public async Task<CustomerResponse> CustomerConfirmPassword(CustomerResetPassword resetPasswordRequest)
        {
            return await _restClient.MakeApiCall<CustomerResponse>(ApiUtils.URL_CUST_CONFIRM_PASSWORD, HttpMethod.Post, resetPasswordRequest);
        }

        public async Task<CustomerResponse> CustomerVerifyOTP(CustomerVerifyOTPRequest otpRequest)
        {
            return await _restClient.MakeApiCall<CustomerResponse>(string.Format(ApiUtils.URL_CUST_VERIFY_OTP, otpRequest.emailId,otpRequest.otp), HttpMethod.Get, otpRequest);
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

        public async Task<DeleteResponse> SaveClockInTime(TimeClockRoot ClockInRequest)
        {
            return await _restClient.MakeApiCall<DeleteResponse>(ApiUtils.URL_SAVE_CLOCKIN_TIME, HttpMethod.Post, ClockInRequest);
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

        public async Task<PostResponse> UpdateProdQuantity(int Id, int quantity)
        {
            var uriBuilder = ApiUtils.URL_UPDATE_PRODUCT_QUANTITY;
            var string1 = "?ProductId=" + Id;
            var string2 = "&Quantity=" + quantity;

            var url = uriBuilder + string1 + string2;

            var result = await _restClient.MakeApiCall<PostResponse>(url, HttpMethod.Post);
            return result;
        }
        public async Task<Clients> GetAllClient()
        {
            return await _restClient.MakeApiCall<Clients>(ApiUtils.URL_GET_ALL_CLIENT, HttpMethod.Get);
        }

        public async Task<ProductsSearch> SearchProduct(string productName)
        {
            var data = new { productSearch = productName};
            return await _restClient.MakeApiCall<ProductsSearch>(ApiUtils.URL_SEARCH_PRODUCT, HttpMethod.Post, data);
        }

        public async Task<MembershipServiceList> GetMembershipServiceList()
        {
            return await _restClient.MakeApiCall<MembershipServiceList>(ApiUtils.URL_GET_ALL_SERVICE, HttpMethod.Get);
        }

        public async Task<PostResponseBool> SaveVehicleMembership(ClientVehicleRoot clientVehicle)
        {
            return await _restClient.MakeApiCall<PostResponseBool>(ApiUtils.URL_SAVE_VEHICLE_MEMBERSHIP, HttpMethod.Post, clientVehicle);
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

        public async Task<ScheduleModel> GetSchedulePastService(int clientID)
        {
            return await _restClient.MakeApiCall<ScheduleModel>(ApiUtils.URL_SCHEDULE_PAST_SERVICE+ "?ClientId=" +clientID, HttpMethod.Get);
        }

        public async Task<AvailableServicesModel> GetScheduleServices(int LocationID)
        {
            return await _restClient.MakeApiCall<AvailableServicesModel>(ApiUtils.URL_GET_CLIENT_VEHICLE_SERVICES_LIST+ "?locationId="+LocationID, HttpMethod.Get);
        }

        public async Task<AvailableScheduleSlots> GetScheduleSlots(ScheduleSlotInfo slotInfo )
        {
            return await _restClient.MakeApiCall<AvailableScheduleSlots>(ApiUtils.URL_SCHEDULE_TIME_SLOTS, HttpMethod.Post, slotInfo);
        }

        public async Task<DownloadDocuments> DownloadDocuments(int documentID, string documentPassword)
        {
            return await _restClient.MakeApiCall<DownloadDocuments>(ApiUtils.URL_EMPLOYEE_DOCUMENTS_DOWNLOAD+documentID+","+documentPassword, HttpMethod.Get);
        }

        public async Task<DeleteResponse> DeleteDocuments(int documentID)
        {
            return await _restClient.MakeApiCall<DeleteResponse>(ApiUtils.URL_EMPLOYEE_DOCUMENTS_DELETE + documentID, HttpMethod.Delete);
        }

        public async Task<PostResponseBool> UpdateEmployeePersonalDetails(UpdatePersonalDetails employeeInfo)
        {
            return await _restClient.MakeApiCall<PostResponseBool>(ApiUtils.URL_UPDATE_EMPLOYEE_PERSONAL_DETAILS, HttpMethod.Post, employeeInfo);
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

