using System;
namespace Strive.Core.Utils
{
    public class ApiUtils
    { 
        public const string BASE_URL = "http://14.141.185.75:5004";

        public static string Token { get; set; }

        public const string URL_LOGIN_EMPLOYEE = "/Auth/Login";

        public const string URL_CUST_SIGN_UP = "/Auth/CreateLogin";

        public const string URL_CUST_FORGOT_PASSWORD = "/Auth/SendOTP/{0}";

        public const string URL_CUST_CONFIRM_PASSWORD = "/Auth/ResetPassword";

        public const string URL_CUST_VERIFY_OTP = "/Auth/VerfiyOTP/{0}/{1}";

        public const string URL_GET_ALL_LOCATION_ADDRESS = "/Admin/Location/GetAllLocationAddress";

        public const string URL_GET_CLOCKIN_STATUS = "/Admin/TimeClock/TimeClockDetails";

        public const string URL_SAVE_CLOCKIN_TIME = "/Admin/TimeClock/Save";

        public const string URL_GET_ALL_PRODUCTS = "/Admin/Product/GetAll";

        public const string URL_GET_ALL_VENDORS = "/Admin/Vendor/GetAll";

        public const string URL_ADD_PRODUCT = "/Admin/Product/Add";

        public const string URL_DELETE_PRODUCT = "/Admin/Product/Delete";

        public const string URL_UPDATE_PRODUCT = "/Admin/Product/Update";

        public const string URL_SEARCH_PRODUCT = "/Admin/Product/GetProductSearch";

        public const string URL_GET_PAST_SCHEDULE = "/Admin/Schedule/GetSchedule";

        public const string URL_GET_ALL_CLIENT = "/Admin/Client/GetAll";

        public const string URL_SEARCH_CLIENT = "/Admin/Client/GetClientSearch";

        public const string URL_GET_ALL_SERVICE = "/Admin/MembershipSetup/GetAll";

        public const string URL_SAVE_VEHICLE_MEMBERSHIP = "/Admin/Vehicle/SaveClientVehicleMembership";

        public const string URL_GET_CLIENT_DETAIL = "/Admin/Client/GetClientById/";

        public const string URL_GET_CLIENT_VEHICLE = "/Admin/Vehicle/GetVehicleByClientId";

        public const string URL_GET_CLIENT_VEHICLE_MEMBERSHIP = "/Admin/Vehicle/GetVehicleMembershipDetailsByVehicleId";

        public const string URL_GET_CLIENT_VEHICLE_SERVICES = "/Admin/ServiceSetup/GetService";

        public const string URL_GET_SELECTED_MEMBERSHIP_SERVICES = "/Admin/MembershipSetup/GetAllMembershipById/";  

        public const string URL_GET_CLIENT_BY_ID = "/Admin/Client/GetClientById/{0}";

        public const string URL_SAVE_CLIENT_INFO = "/Admin/Client/UpdateClientVehicle";

        public const string URL_GET_VEHICLE_CODES = "/Admin/Vehicle/GetVehicleCodes";

        public const string URL_UPDATE_VEHICLE_INFO = "/Admin/Vehicle/Update";

        public const string URL_ADD_VEHICLE_INFO = "/Admin/Vehicle/AddVehicle";

        public const string URL_GET_VEHICLE_COMPLETE_DETAILS = "/Admin/Vehicle/GetVehicleId";

        public const string URL_DELETE_VEHICLE_INFO = "/Admin/Vehicle/Delete";
    }
}
