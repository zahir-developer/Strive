using Strive.Core.Models.Employee.Messenger.PersonalChat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerPersonalChatViewModel : BaseViewModel
    {
        #region Properties

        public PersonalChatMessages chatMessages { get; set; }

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

        #endregion Commands
    }
}
