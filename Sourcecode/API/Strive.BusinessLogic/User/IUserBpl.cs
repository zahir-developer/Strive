using System;
using System.Collections.Generic;
using System.Text;
using Strive.BusinessEntities;
using Strive.Common;

namespace Strive.BusinessLogic
{
    public interface IUserBpl
    {
        Result AddUser(User user);
        Result GetUsers();
    }
}
