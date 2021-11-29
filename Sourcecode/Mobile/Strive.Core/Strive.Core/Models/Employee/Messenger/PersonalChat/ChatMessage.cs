using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;

namespace Strive.Core.Models.Employee.Messenger.PersonalChat
{
    public class ChatMessage : MvxViewModel
    {
        public MvxObservableCollection<ChatMessageDetail> _chatMessageDetail;

        public MvxObservableCollection<ChatMessageDetail> ChatMessageDetail
        {
            get => _chatMessageDetail;
            set
            {
                _chatMessageDetail = value;
                RaisePropertyChanged(() => ChatMessageDetail);
                // take any additional actions here which are required when MyProperty is updated
            }
        }
    }

    public class ChatMessageDetail
    {
        public int SenderId { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        public int ReceipientId { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string MessageBody { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? chatMessageId { get; set; }
    }
}
