using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerResetPassword
    {
        public CustomerResetPassword(string sentOTP,string password,string userId)
        {
            this.sentOTP = sentOTP;
            this.confirmedPassword = password;
            this.userId = userId;
        }
        public string sentOTP { get; set; }

        public string confirmedPassword { get; set; }

        public string userId { get; set; }

    }
}
