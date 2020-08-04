using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Auth
{
    public class ResetPassword
    {
        public string OTP { get; set; }
        public string NewPassword { get; set; }
        public string UserId { get; set; }
    }
}
