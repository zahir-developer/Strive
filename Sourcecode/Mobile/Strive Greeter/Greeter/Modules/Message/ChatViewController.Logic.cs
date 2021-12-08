using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Newtonsoft.Json;

namespace Greeter.Modules.Message
{
    public partial class ChatViewController
    {
        List<ChatMessage> Chats = new();

        readonly ChatType chatType;
        readonly ChatInfo chatInfo;
        public ChatViewController(ChatType chatType, ChatInfo chatInfo)
        {
            this.chatType = chatType;
            this.chatInfo = chatInfo;
            _ = GetChatsAsync();

            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("com.strive.greeter.private_message_received"), notify: (notification) => {
                if (notification.UserInfo is null)
                    return;

                var chatMsgString = notification.UserInfo["chatMsg"] as NSString;
                var chatMsg = JsonConvert.DeserializeObject<SendChatMessage>(chatMsgString);

                InvokeOnMainThread(() => {
                    MessageReceived(chatMsg);
                });
            });

            _ = SingalR.StartConnection(AppSettings.UserID);
        }

        //void SendMsg()
        //{
        //    SendChatMessage sendChatMessage = new SendChatMessage() {
        //        chatMessageRecipient = new chatMessageRecipient() { chatRecipientId = 4321, senderId = 325626 },
        //        firstName = "Karthik",
        //        lastName = "Poornam",
        //        chatMessage = new chatMessage() { messagebody = "Testing Update", createdDate = DateTime.Now },
        //    };

        //    var dict = new NSDictionary(new NSString("chatMsg"), new NSString(JsonConvert.SerializeObject(sendChatMessage)));

        //    //var milliseconds = 15000;
        //    //Thread.Sleep(milliseconds);

        //    Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => {
        //        NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.greeter.private_message_received"), null, dict);
        //    });
        //}

        void MessageReceived(SendChatMessage sendChatMessage)
        {
            if (sendChatMessage is not null)
            {
                var chatMessage = new ChatMessage();
                chatMessage.ReceipientID = sendChatMessage.chatMessageRecipient.chatRecipientId;
                chatMessage.SenderFirstName = sendChatMessage.firstName;
                chatMessage.SenderLastName = sendChatMessage.lastName;
                chatMessage.MessageBody = sendChatMessage.chatMessage.messagebody;
                chatMessage.CreatedDate = sendChatMessage.chatMessage.createdDate;
                Chats.Add(chatMessage);
                ReloadChatTableView();
            }
        }

        async Task GetChatsAsync()
        {
            ShowActivityIndicator();
            var result = await SingleTon.MessageApiService.GetChatMessages(new ChatMessageRequest
            {
                SenderID = chatInfo.SenderId,
                GroupID = chatInfo.GroupId,
                RecipientID = chatInfo.RecipientId
            });

            HideActivityIndicator();

            HandleResponse(result);

            if (!result.IsSuccess()) return;

            if(result.ChatMessageObject?.ChatMessageDetail is not null)
            {
                Chats = result.ChatMessageObject.ChatMessageDetail;
                ReloadChatTableView();
                ScrollToBottom();
            }
        }

        async void OnSendMsg(string text, ChatInfo chatInfo)
        {
            //TODO Check Connectivity and send data to server

            //Chats.Add("");

            //var indexPath = NSIndexPath.FromRowSection(Chats.Count - 1, 0);
            //InsertRowAtChatTableView(new NSIndexPath[] { indexPath });

            ShowActivityIndicator();

            var req = new SendChatMessageReq
            {
                ChatMessage = new ChatMessageBody()
                {
                    //ChatMessageID = 0
                    Messagebody = text
                },
                ChatMessageRecipient = new ChatMessageRecipient()
                {
                    RecipientID = chatInfo.RecipientId
                }
            };

            if (chatType == ChatType.Group)
            {
                req.ChatMessageRecipient.RecipientGroupID = chatInfo.GroupId;
                req.ConnectionID = req.GroupID = chatInfo.CommunicationId.ToString();
                req.FirstName = req.FirstName;
                req.LastName = req.LastName;
            }

            Debug.WriteLine("Send Chat Msg Req : " + JsonConvert.SerializeObject(req));

            var response = await SingleTon.MessageApiService.SendMesasge(req);

            HideActivityIndicator();

            HandleResponse(response);

            if (!response.IsSuccess()) return;

            //if (response.IsSuccess())email
            //{
            //Chats.AddRange(result.ChatMessageObject.ChatMessageDetail);
                messageTextView.Text = string.Empty;
                _ = GetChatsAsync();

            NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.greeter.update_recent"), null);
                //ReloadChatTableView();
            //}
        }
    }

    public class chatMessage
    {
        public int chatMessageId { get; set; }
        public string subject { get; set; }
        public string messagebody { get; set; }
        public int? parentChatMessageId { get; set; }
        public string expiryDate { get; set; }
        public bool? isReminder { get; set; }
        public string nextRemindDate { get; set; }
        public int? reminderFrequencyId { get; set; }
        public int? createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class chatMessageRecipient
    {
        public long chatRecipientId { get; set; }
        public long? chatMessageId { get; set; }
        public int? senderId { get; set; }
        public int? recipientId { get; set; }
        public int? recipientGroupId { get; set; }
        public string createdDate { get; set; }
        public bool? isRead { get; set; }
    }

    public class chatGroupRecipient
    {
        public int chatGroupRecipientId { get; set; }
        public int chatGroupId { get; set; }
        public int? recipientId { get; set; }
        public bool isRead { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
    }


    public class SendChatMessage
    {
        public chatMessage chatMessage { get; set; }
        public chatMessageRecipient chatMessageRecipient { get; set; }
        public List<chatGroupRecipient> chatGroupRecipient { get; set; }
        public string connectionId { get; set; }
        public string fullName { get; set; }
        public string groupId { get; set; }
        public string groupName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
