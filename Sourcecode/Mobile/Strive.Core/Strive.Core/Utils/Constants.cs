﻿using System;
namespace Strive.Core.Utils
{
    public class ApiUtils
    { 
        public const string BASE_URL = "http://14.141.185.75:5001";

        public const string URL_LOGIN_EMPLOYEE = "/Auth/Login";

        public const string URL_CUST_SIGN_UP = "/Auth/CreateLogin";

        public const string URL_CUST_FORGOT_PASSWORD = "/Auth/SendOTP/{0}";

        public const string URL_CUST_CONFIRM_PASSWORD = "/Auth/ResetPassword";

        public const string URL_CUST_VERIFY_OTP = "/Auth/VerfiyOTP/{0}/{1}";   
    
    }
}
