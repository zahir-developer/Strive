using Strive.Library.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Domain.User
{
    public class UserCreatedEvent : DomainEventBase
    {
        public UserGuid UserGuid { get; }

        public UserCreatedEvent(UserGuid userGuid)
        {
            this.UserGuid = userGuid;
        }
    }
}
