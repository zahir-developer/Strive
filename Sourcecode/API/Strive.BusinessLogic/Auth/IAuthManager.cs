using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Auth
{
    public interface IAuthManager
    {
        Result Login(Authentication authentication, string secretKey);
    }
}
