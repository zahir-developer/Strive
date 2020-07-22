using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerSignUp
    {
        public CustomerSignUp()
        {

        }
        public int authId { get; set; }
        public string userGuid { get; set; } = "";
        public string emailId { get; set; }
        public string mobileNumber { get; set;}
        public int emailVerified { get; set; }
        public int lockoutEnabled { get; set; }
        public string passwordHash { get; set; }
        public int securityStamp { get; set; }
        public string createdDate { get; set; }


    }
}
