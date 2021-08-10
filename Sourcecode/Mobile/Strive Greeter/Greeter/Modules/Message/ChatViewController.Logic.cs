using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;

namespace Greeter.Modules.Message
{
    public partial class ChatViewController
    {
        readonly List<ChatMessage> Chats = new();

        readonly ChatType chatType;
        readonly ChatInfo chatInfo;
        public ChatViewController(ChatType chatType, ChatInfo chatInfo)
        {
            this.chatType = chatType;
            this.chatInfo = chatInfo;
            _ = GetChatsAsync();
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
                Chats.AddRange(result.ChatMessageObject.ChatMessageDetail);
                ReloadChatTableView();
            }
        }

        async void OnSendMsg(string text)
        {
            //TODO Check Connectivity and send data to server

            //Chats.Add("");

            //var indexPath = NSIndexPath.FromRowSection(Chats.Count - 1, 0);
            //InsertRowAtChatTableView(new NSIndexPath[] { indexPath });

            ShowActivityIndicator();
            var result = await SingleTon.MessageApiService.SendMesasge(new SendChatMessageReq
            {
                //enderID = chatInfo.SenderId,
                //GroupID = chatInfo.GroupId,
                //RecipientID = chatInfo.RecipientId
            });

            HideActivityIndicator();

            //HandleResponse(result);

            //if (!result.IsSuccess()) return;

            //if (result.ChatMessageObject?.ChatMessageDetail is not null)
            //{
            //    Chats.AddRange(result.ChatMessageObject.ChatMessageDetail);
            //    ReloadChatTableView();
            //}
        }
    }
}
