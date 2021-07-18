using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greeter.Modules.Message
{
    public partial class ContactViewController
    {
        List<string> contacts = new();

        public ContactViewController()
        {
            contacts.Add("William Jones");
            contacts.Add("Jimmy Tester");
            contacts.Add("Brittany Rose");
            contacts.Add("John Rambo");
            contacts.Add("Daniel Steel");
            contacts.Add("Bruce Wayne");
            contacts.Add("Peter Parker");
        }

        Task GetContacts()
        {
            return Task.CompletedTask;
        }

        async Task SearchContact(string keyword)
        {
            await Task.CompletedTask;
        }
    }
}