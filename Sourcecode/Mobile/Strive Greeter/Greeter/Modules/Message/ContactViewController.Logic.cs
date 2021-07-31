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

            if (response?.ContactListobj?.ContactsList is not null)
            {
                searchedContacts = contacts = response.ContactListobj.ContactsList;
                RefreshContactsToUI();
            }
        }

        async Task SearchContact(string keyword)
        {
            searchedContacts = await Task.Run(() => contacts.Where(contact => Logic.FullName(contact.FirstName, contact.LastName).ToLower().Contains(keyword.ToLower().Trim())).ToList());
            RefreshContactsToUI();
        }

        void OnContactSelectionCompleted()
        {
            if(searchedContacts is not null)
            {
                var selectedContact = searchedContacts.Where(contact => contact.IsSelected);
                if(Delegate is not null && Delegate.TryGetTarget(out IContactViewControllerDelegate @delegate))
                {
                    @delegate.ContactSelectionDidCompleted(new List<ContactEmployee>(selectedContact));
                    DismissViewController(true, null);
                }
            }
        }
    }
}