using System;
namespace Strive.Core.Models.TimInventory
{
    public class EmployeeLoginRequest
    {
        public EmployeeLoginRequest(string email, string password, string token)
        {
            this.email = email;
            passwordHash = password;
            this.token = token;
        }
        public string email { get; set; }

        public string passwordHash { get; set; }

        public string token { get; set; }

    }
}
