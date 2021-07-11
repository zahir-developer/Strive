using System.Collections.Generic;

namespace Greeter.Modules.Message
{
    public partial class GroupParticipantsViewController
    {
        List<string> participants = new();

        public GroupParticipantsViewController()
        {
            participants.Add("");
            participants.Add("");
            participants.Add("");
            participants.Add("");
            participants.Add("");
        }

        void AddParticipant(object item)
        {
            participants.Add("");
        }

        public void RemoveParticipant(object item)
        {
            //TODO implement logic later
            if (participants.Count == 0) return;

            participants.RemoveAt(0);
        }

        async void SaveParticipant()
        {

        }
    }
}