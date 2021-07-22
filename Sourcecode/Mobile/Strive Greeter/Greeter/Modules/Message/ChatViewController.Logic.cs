using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;

namespace Greeter.Modules.Message
{
    public partial class ChatViewController
    {
        //TODO change string to model object;
        readonly List<string> Chats = new();

        readonly ChatType chatType;
        public ChatViewController(ChatType chatType)
        {
            this.chatType = chatType;
            _ = GetChatsAsync();
        }

        async Task GetChatsAsync()
        {
            //TODO get real time data from server and add here
            Chats.Add("");
            Chats.Add("");
            Chats.Add("");
            Chats.Add("");
            Chats.Add("");
            ReloadChatTableView();
            await Task.CompletedTask;
        }

        async void OnSend()
        {
            //TODO Check Connectivity and send data to server

            Chats.Add("");

            var indexPath = NSIndexPath.FromRowSection(Chats.Count - 1, 0);
            InsertRowAtChatTableView(new NSIndexPath[] { indexPath });
        }
    }
}
