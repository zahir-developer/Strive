using Strive.Library.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Domain.User.Rules
{
    public class UserEmailMustBeUniqueRule : IBusinessRule
    {
        private readonly IUserUniquenessChecker _userUniquenessChecker;
        private readonly string _email;

        public UserEmailMustBeUniqueRule(IUserUniquenessChecker userUniquenessChecker, string email)
        {
            _userUniquenessChecker = userUniquenessChecker;
            _email = email;
        }

        public bool IsBroken() => !_userUniquenessChecker.IsUnique(_email);

        public string Message => "User with this email already exists.";
    }
}
