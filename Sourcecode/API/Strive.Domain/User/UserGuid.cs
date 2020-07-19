using Strive.Library.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Domain.User
{
    public class UserGuid : TypedIdValueBase
    {
        public UserGuid(Guid value) : base(value)
        {
        }
    }
}
