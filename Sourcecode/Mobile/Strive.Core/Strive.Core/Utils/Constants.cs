using System;
namespace Strive.Core.Utils
{
    public class ApiUtils
    { 
        public const string BASE_URL = "http://192.168.1.100:60001";

        public static string Token { get; set; }

        public const string URL_LOGIN_EMPLOYEE = "/Auth/Login";

        public const string URL_CUST_SIGN_UP = "/Auth/CreateLogin";

        public const string URL_CUST_FORGOT_PASSWORD = "/Auth/SendOTP/{0}";

        public const string URL_CUST_CONFIRM_PASSWORD = "/Auth/ResetPassword";

        public const string URL_CUST_VERIFY_OTP = "/Auth/VerfiyOTP/{0}/{1}";

        public const string URL_GET_ALL_LOCATION_ADDRESS = "/Admin/Location/GetAllLocationAddress";

        public const string URL_GET_CLOCKIN_STATUS = "/Admin/TimeClock/TimeClock/userId/datetime";

        public const string URL_SAVE_CLOCKIN_TIME = "/Admin/TimeClock/Save";

        public const string URL_GET_ALL_PRODUCTS = "/Admin/Product/GetAll";
    }
}
