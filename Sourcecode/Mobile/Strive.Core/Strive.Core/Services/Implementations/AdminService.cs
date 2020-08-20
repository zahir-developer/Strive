using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MvvmCross;
using Strive.Core.Models;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;

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

        public async Task<TimeClockRoot> GetClockInStatus(TimeClockRequest request)
        {
            //var uriBuilder = new UriBuilder(ApiUtils.URL_GET_CLOCKIN_STATUS);
            //var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            //query["userId"] = Id.ToString();
            //query["dateTime"] = Datetime;
            //uriBuilder.Query = query.ToString();
            //var url = uriBuilder.Uri.PathAndQuery.ToString();
            return await _restClient.MakeApiCall<TimeClockRoot>(ApiUtils.URL_GET_CLOCKIN_STATUS, HttpMethod.Post,request);
        }

        public async Task<TimeClock> SaveClockInTime(TimeClock ClockInRequest)
        {
            return await _restClient.MakeApiCall<TimeClock>(ApiUtils.URL_SAVE_CLOCKIN_TIME, HttpMethod.Post, ClockInRequest);
        }

        public async Task<Products> GetAllProducts()
        {
            return await _restClient.MakeApiCall<Products>(ApiUtils.URL_GET_ALL_PRODUCTS, HttpMethod.Get);
        }

        public async Task<Vendors> GetAllVendors()
        {
            return await _restClient.MakeApiCall<Vendors>(ApiUtils.URL_GET_ALL_VENDORS, HttpMethod.Get);
        }

        public async Task<PostResponse> AddProduct(ProductDetail product)
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

        public async Task<PostResponse> UpdateProduct(ProductDetail product)
        {
            return await _restClient.MakeApiCall<PostResponse>(ApiUtils.URL_UPDATE_PRODUCT, HttpMethod.Post, product);
        }

        public async Task<Clients> GetAllClient()
        {
            return await _restClient.MakeApiCall<Clients>(ApiUtils.URL_GET_ALL_CLIENT, HttpMethod.Get);
        }
    }
}
