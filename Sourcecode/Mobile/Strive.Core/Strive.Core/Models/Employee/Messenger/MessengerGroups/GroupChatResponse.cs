using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Messenger.MessengerGroups
{
   public class GroupChatResponse
    {
        public Result Result { get; set; }
    }
    public class Result
    {
        public int ChatGroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupId { get; set; }
    }
}
