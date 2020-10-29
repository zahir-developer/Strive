using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblChatCommunication")]
    public class ChatCommunication
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public long ChatChatCommunicationId { get; set; }

        [Column, PrimaryKey]
        public long? EmployeeId { get; set; }

      
        [Column, PrimaryKey]
        public string CommunicationId { get; set; }
        
    }
}