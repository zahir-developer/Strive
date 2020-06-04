using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{
    public class Authentication
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
