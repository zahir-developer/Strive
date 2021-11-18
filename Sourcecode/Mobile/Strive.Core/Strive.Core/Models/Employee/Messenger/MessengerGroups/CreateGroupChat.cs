using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Messenger.MessengerGroups
{
    public class CreateGroupChat
    {
        public chatGroup chatGroup { get; set; }
        public List<chatUserGroup> chatUserGroup { get; set; }
        public string GroupId { get; set; } 
    }
    public class chatGroup
    {
        public int chatGroupId { get; set; }
        
        public string groupName { get; set; }
        public string comments { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public int updatedBy { get; set; }
        public string updatedDate { get; set; }

    }
    public class chatUserGroup
    {
        public int chatGroupUserId { get; set; }
        public string CommunicationId { get; set; }
        public int userId { get; set; }
        public int chatGroupId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
    }
}
