using System;
namespace Strive.Core.Models.TimInventory
{
    public class EmployeeLoginRequest
    {
        public EmployeeLoginRequest(string email, string password)
        {
            this.email = email;
            passwordHash = password;
        }
        public string email { get; set; }

        public string passwordHash { get; set; }
    }
}
