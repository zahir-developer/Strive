using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
   public class CustomerLoginRequest
    {
        public CustomerLoginRequest(string email, string passwordHash)
            {
                this.email = email;
                this.passwordHash = passwordHash;
            }
        public string email { get; set; }

        public string passwordHash { get; set; }
    }
}
