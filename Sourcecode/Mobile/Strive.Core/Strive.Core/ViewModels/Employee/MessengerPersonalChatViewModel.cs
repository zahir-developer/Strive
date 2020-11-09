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
        #endregion Properties

        #region Commands

        public async Task GetAllMessages(ChatDataRequest chatData)
        {
            var result = await MessengerService.GetPersonalChatMessages(chatData);
            if(result == null)
            {
                //_userDialog.Toast("No messages");
            }
            else
            {

            }
        }

        #endregion Commands
    }
}
