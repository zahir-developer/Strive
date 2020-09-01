using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerVerifyOTPRequest
    {
        public CustomerVerifyOTPRequest(string emailId, string otp)
        {
            this.emailId = emailId;
            this.otp = otp;
        }
    
        public string emailId { get; set; }
        public string otp { get; set; }
    }
}
