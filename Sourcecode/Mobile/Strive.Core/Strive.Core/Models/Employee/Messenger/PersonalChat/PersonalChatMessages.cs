using MvvmCross.ViewModels;

namespace Strive.Core.Models.Employee.Messenger.PersonalChat
{
    public class PersonalChatMessages : MvxViewModel
    {
        private ChatMessage _chatMessage;

        public ChatMessage ChatMessage
        {
            get => _chatMessage;
            set
            {
                _chatMessage = value;
                RaisePropertyChanged(() => ChatMessage);
                // take any additional actions here which are required when MyProperty is updated
            }
        }
    }
}
