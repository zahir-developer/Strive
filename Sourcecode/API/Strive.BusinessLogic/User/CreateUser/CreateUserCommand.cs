using Strive.BusinessEntities.Auth;
using Strive.Library.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.User.CreateUser
{
    public class CreateUserCommand : CommandBase<UserDTO>
    {
        public string Email { get; }

        public string Name { get; }

        public CreateUserCommand(string email, string name)
        {
            this.Email = email;
            this.Name = name;
        }
    }
}
