using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Messenger.PersonalChat
{
    public class ChatDataRequest
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public int GroupId { get; set; }
    }
}
