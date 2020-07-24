using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public static class CustomerOTPInfo
    {
        public static string resetEmail { get; set; }
        public static string OTP { get; set; }

        public static void Clear()
        {
            resetEmail = "";
            OTP = "";
        }
    }
}
