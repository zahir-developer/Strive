using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel.Messenger;
using Strive.BusinessLogic.Common;
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

        public int CreateGroup(ChatGroupDto chatGroupDto)
        {
            return new MessengerRal(_tenant).CreateChatGroup(chatGroupDto);
        }

        public Result GetChatEmployeeList(int employeeId)
         {
            var result = new MessengerRal(_tenant).GetChatEmployeeList(employeeId);

            return ResultWrap(result, "EmployeeList");

        }

        public Result GetChatMessage(ChatDto chatDto)
        {
            return ResultWrap(new MessengerRal(_tenant).GetChatMessage, chatDto, "ChatMessage");
        }
        public Result GetUnReadMessageCount(int employeeid)
        {
            return ResultWrap(new MessengerRal(_tenant).GetUnReadMessageCount, employeeid, "UnreadMessage");
        }

        public Result GetChatGroupEmployeelist(int chatGroupId)
        {
            return ResultWrap(new MessengerRal(_tenant).GetChatGroupEmployeelist, chatGroupId, "EmployeeList");
        }

        public ChatGroupListViewModel GetChatEmployeeGrouplist(int employeeId)
        {
            return new MessengerRal(_tenant).GetChatEmployeeGrouplist(employeeId);
        }

        public Result DeleteChatGroupUser(int chatGroupUserId)
        {
            return ResultWrap(new MessengerRal(_tenant).DeleteChatGroupUser, chatGroupUserId, "ChatGroupUserDelete");
        }

        public Result ChangeUnreadMessageState(ChatDto chatDto)
        {
            return ResultWrap(new MessengerRal(_tenant).ChangeUnreadMessageState, chatDto, "ChatGroupUserDelete");
        }

        public Result GetAllEmployeeName(SearchDto searchDto)
        {
            return ResultWrap(new MessengerRal(_tenant).GetAllEmployeeName, searchDto, "EmployeeList");
        }
    }
}
