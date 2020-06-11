using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Strive.BusinessEntities;
using Newtonsoft.Json.Linq;
using System.Net;
using Strive.ResourceAccess;

namespace Strive.BusinessLogic
{
    public class UserManagerBpl : IUser
    {
        public Result AddUser(User user)
        {
            Result result;
            JObject resultContent = new JObject();
            try
            {
                //var userdetails = new User() { FirstName = "Mamooth", LastName = "Strive", LoginId = "Mamooth", Role = "Admin" };
                //var token = GetToken(userdetails, secretKey);
                //resultContent.Add(token.WithName("Token"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;

        }

        public Result GetUsers()
        {
            Result result;
            JObject resultContent = new JObject();
            var users = new UserRal().GetUsers();
            resultContent.Add(users.WithName("Users"));
            result = Helper.BindSuccessResult(resultContent);
            return result;
        }
    }
}
