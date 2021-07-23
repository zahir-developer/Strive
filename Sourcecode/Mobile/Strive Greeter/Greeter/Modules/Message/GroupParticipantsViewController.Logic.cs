using System.Collections.Generic;
using Greeter.DTOs;

namespace Greeter.Modules.Message
{
    public partial class GroupParticipantsViewController
    {
        List<Contact> participants = new();
        readonly bool isCreateGroup;

        public GroupParticipantsViewController(bool isCreateGroup)
        {
            this.isCreateGroup = isCreateGroup;

            participants.AddRange(new List<Contact>
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

        void AddParticipant(object item)
        {
            participants.Add(new Contact { Name = "Jemes Rock" });
        }

        public void RemoveParticipant(Contact contact)
        {
            participants.Remove(contact);
            ReloadParticipantTableView();
        }

        async void SaveParticipant()
        {

        }

        void OnCreateGroup()
        {

        }
    }
}