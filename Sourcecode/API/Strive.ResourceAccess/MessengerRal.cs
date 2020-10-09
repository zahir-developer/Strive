using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class MessengerRal : RalBase
    {
        public MessengerRal(ITenantHelper tenant) : base(tenant) { }

        public bool SendMessege(ChatMessageDto chatMessegeDto)
        {
            return dbRepo.InsertPc(chatMessegeDto, "ChatMessageId");
        }

        public int CreateChatGroup(ChatGroupDto chatGroupDto)
        {
            return dbRepo.InsertPK(chatGroupDto, "ChatGroupId");
        }

        public bool ChatCommunication(ChatCommunicationDto chatCommunicationDto)
        {
            return dbRepo.InsertPc(chatCommunicationDto, "ChatCommunicationId");
        }
    }
}
