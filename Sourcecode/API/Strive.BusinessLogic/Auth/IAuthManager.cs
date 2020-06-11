using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Strive.BusinessEntities;

namespace Strive.BusinessLogic.Auth
{
    public interface IAuthManager
    {
        Result Login(Authentication authentication, string secretKey);
    }
}
