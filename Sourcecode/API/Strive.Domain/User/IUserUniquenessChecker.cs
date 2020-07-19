using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Domain.User
{
    public interface IUserUniquenessChecker
    {
        bool IsUnique(string userEmail);
    }
}
