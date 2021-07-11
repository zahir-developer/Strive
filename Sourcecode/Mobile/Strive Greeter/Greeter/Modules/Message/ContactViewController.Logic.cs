using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greeter.Modules.Message
{
    public partial class ContactViewController
    {
        List<string> contacts = new();

        public ContactViewController()
        {
            contacts.Add("");
            contacts.Add("");
            contacts.Add("");
            contacts.Add("");
            contacts.Add("");
            contacts.Add("");
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