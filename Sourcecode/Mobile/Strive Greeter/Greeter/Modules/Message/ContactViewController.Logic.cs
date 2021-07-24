using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;

namespace Greeter.Modules.Message
{
    public partial class ContactViewController
    {
        List<ContactEmployee> contacts = new();
        readonly ContactConfigureType configureType;
        List<ContactEmployee> searchedContacts;

        public ContactViewController(ContactConfigureType configureType)
        {
            this.configureType = configureType;
            //contacts.AddRange(new List<Contact>
            //{
            //    new Contact { Name = "William Jones" },
            //    new Contact { Name = "Jimmy Tester" },
            //    new Contact { Name = "Brittany Rose" },
            //    new Contact { Name = "John Rambo" },
            //    new Contact { Name = "Daniel Steel" },
            //    new Contact { Name = "Bruce Wayne" },
            //    new Contact { Name = "Peter Parker" }
            //});

            //searchedContacts = contacts;
            _ = GetContacts();
        }

        async Task GetContacts()
        {
            var req = new GetContactsRequest() {
                LocationID = AppSettings.LocationID,
                SortOrder = "ASC"
            };

            ShowActivityIndicator();
            var response = await SingleTon.MessageApiService.GetContactsList(req);
            HideActivityIndicator();

            HandleResponse(response);

            if (!response.IsSuccess()) return;

            if (response?.ContactListobj?.ContactsList != null)
            {
                contacts = response?.ContactListobj?.ContactsList;
                searchedContacts = response?.ContactListobj?.ContactsList;
                RefreshContactsToUI();
            }
        }

        async Task SearchContact(string keyword)
        {
            searchedContacts = await Task.Run(() => contacts.Where(contact => FullName(contact.FirstName, contact.LastName).ToLower().Contains(keyword.ToLower().Trim())).ToList());
            RefreshContactsToUI();
        }

        string FullName(string firstName, string lastName) => $"{firstName} {lastName}";

        void OnCreateGroup()
        {

        }
    }
}