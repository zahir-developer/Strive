using Strive.Domain.User.Rules;
using Strive.Library.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Domain.User
{
    public class User : Entity, IAggregateRoot
    {
        public UserGuid Id { get; private set; }

        private string _email;

        private string _name;

        private bool _welcomeEmailWasSent;

        private User()
        {
        }

        private User(string email, string name)
        {
            this.Id = new UserGuid(Guid.NewGuid());
            _email = email;
            _name = name;
            _welcomeEmailWasSent = false;

            this.AddDomainEvent(new UserCreatedEvent(this.Id));
        }


        public static User CreateRegistered(string email, string name, IUserUniquenessChecker userUniquenessChecker)
        {
            CheckRule(new UserEmailMustBeUniqueRule(userUniquenessChecker, email));
            return new User(email, name);
        }
    }
}
