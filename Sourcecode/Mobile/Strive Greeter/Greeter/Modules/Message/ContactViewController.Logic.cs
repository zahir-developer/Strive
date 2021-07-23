using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.DTOs;

namespace Greeter.Modules.Message
{
    public partial class ContactViewController
    {
        List<Contact> contacts = new();
        readonly ContactConfigureType configureType;

        public ContactViewController(ContactConfigureType configureType)
        {
            this.configureType = configureType;
            contacts.AddRange(new List<Contact>
            {
                new Contact { Name = "William Jones" },
                new Contact { Name = "Jimmy Tester" },
                new Contact { Name = "Brittany Rose" },
                new Contact { Name = "John Rambo" },
                new Contact { Name = "Daniel Steel" },
                new Contact { Name = "Bruce Wayne" },
                new Contact { Name = "Peter Parker" }
            });
        }

        Task GetContacts()
        {
            return Task.CompletedTask;
        }

        async Task SearchContact(string keyword)
        {
            await Task.CompletedTask;
        }

        void OnCreateGroup()
        {

        }
    }
}