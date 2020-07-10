using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Employee
{
    public class EmployeeLogin
    {
        public int AuthId { get; set; }
        public string UserGuid { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public int EmailVerified { get; set; }
        public int LockoutEnabled { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
