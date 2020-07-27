using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerResetPassword
    {
        public CustomerResetPassword(string sentOTP,string password,string userId)
        {
            this.otp = sentOTP;
            this.newPassword = password;
            this.userId = userId;
        }
        public string otp { get; set; }

        public string newPassword { get; set; }

        public string userId { get; set; }

    }
}
