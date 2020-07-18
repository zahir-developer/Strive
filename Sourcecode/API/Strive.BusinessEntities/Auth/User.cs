using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Auth
{
    public class UserLogin
    {
        public int? AuthId { get; set; }
        public Guid? UserGuid { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public int EmailVerified { get; set; }
        public int LockoutEnabled { get; set; }
        public string PasswordHash { get; set; }
        public int SecurityStamp { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
