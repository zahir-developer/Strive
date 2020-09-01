using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO.User;
using Strive.Common;

namespace Strive.BusinessLogic.Client
{
    public interface IClientBpl
    {
        Result SaveClientDetails(ClientView lstClient);
        Result GetAllClient();
        Result DeleteClient(int clientId);
        Result GetClientById(int id);
        Result Signup(UserSignupDto clientSignup);
    }
}
