using System;
using System.Collections.Generic;
using System.Text;
using Strive.BusinessEntities;
using Strive.Common;

namespace Strive.BusinessLogic
{
    public interface IUser
    {
        Result AddUser(User user);
        Result GetUsers();
    }
}
