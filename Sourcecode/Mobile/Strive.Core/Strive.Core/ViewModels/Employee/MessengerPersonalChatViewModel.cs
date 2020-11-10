using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerPersonalChatViewModel : BaseViewModel
    {
        #region Properties

        public string Message { get; set; }
        public PersonalChatMessages chatMessages { get; set; }
        public SendChatMessage sendChat { get; set; }

        #endregion Properties

        #region Commands

        public async Task GetAllMessages(ChatDataRequest chatData)
        {
            var result = await MessengerService.GetPersonalChatMessages(chatData);
            if(result == null || result.ChatMessage == null || result.ChatMessage.ChatMessageDetail == null || result.ChatMessage.ChatMessageDetail.Count == 0)
            {
                chatMessages = null;
            }
            else
            {
                chatMessages = new PersonalChatMessages();
                chatMessages.ChatMessage = new ChatMessage();
                chatMessages.ChatMessage.ChatMessageDetail = new List<ChatMessageDetail>();
                chatMessages = result;
            }
        }

        public async Task SendMessage()
        {
           if(!CheckEmptyChat())
            {
                sendChat = new SendChatMessage();
                sendChat.chatMessage = new chatMessage();
                sendChat.chatMessageRecipient = new chatMessageRecipient();
                var result = await MessengerService.SendChatMessage(sendChat);
            }
        }

        public bool CheckEmptyChat()
        {
            bool result = true;
            if(String.IsNullOrEmpty(Message))
            {
                _userDialog.Toast("Enter a message to send");
                return result;
            }
            return result = false;
        }

        public void FillChatDetails()
        {
            sendChat.chatMessage.ChatMessageId = 0;
            sendChat.chatMessage.Subject = null;
            sendChat.chatMessage.Messagebody = Message;
            sendChat.chatMessage.ParentChatMessageId = null;
            sendChat.chatMessage.ExpiryDate = null;
            sendChat.chatMessage.IsReminder = true;
            sendChat.chatMessage.NextRemindDate = null;
            sendChat.chatMessage.ReminderFrequencyId = null;
            sendChat.chatMessage.CreatedBy = 0;
            sendChat.chatMessage.CreatedDate = DateUtils.ConvertDateTimeWithZ();
        }
        #endregion Commands
    }
}
