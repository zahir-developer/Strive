using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessEntities.Model;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Messenger
{
    public class MessengerBpl : Strivebase, IMessengerBpl
    {
        public MessengerBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }

        public Result ChatCommunication(ChatCommunicationDto chatCommunicationDto)
        {
            return ResultWrap(new MessengerRal(_tenant).ChatCommunication, chatCommunicationDto, "Status");
        }

        public Result SendMessenge(ChatMessageDto chatMessageDto)
        {
           return ResultWrap(new MessengerRal(_tenant).SendMessege, chatMessageDto, "Status");
        }

        public Result CreateGroup(ChatGroupDto chatGroupDto)
        {
            return ResultWrap(new MessengerRal(_tenant).CreateChatGroup, chatGroupDto, "Status");
        }

        public Result GetChatEmployeeList()
        {
            return ResultWrap(new MessengerRal(_tenant).GetChatEmployeeList, "EmployeeList");
        }

        public Result GetChatMessage(ChatDto chatDto)
        {
            return ResultWrap(new MessengerRal(_tenant).GetChatMessage, chatDto, "ChatMessage");
        }
    }
}
