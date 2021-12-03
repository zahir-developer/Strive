﻿using System.Collections.Generic;
using System.Diagnostics;
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
        List<ChatMessage> Chats;

        readonly ChatType chatType;
        readonly ChatInfo chatInfo;
        public ChatViewController(ChatType chatType, ChatInfo chatInfo)
        {
            this.chatType = chatType;
            this.chatInfo = chatInfo;
            _ = GetChatsAsync();

            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("com.strive.greeter.private_message_received"), notify: async (notification) => { await MessageReceived(notification); });
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

        void MessageReceived()
        {
            if ( is not null)
            {
                var chatMessage = new ChatMessage();
                chatMessage.ReceipientID = ;
                chatMessage.SenderFirstName = ;
                chatMessage.SenderLastName = ;
                chatMessage.MessageBody = ;
                chatMessage.CreatedDate = ;
                Chats.Add()
                ReloadChatTableView();
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
}
