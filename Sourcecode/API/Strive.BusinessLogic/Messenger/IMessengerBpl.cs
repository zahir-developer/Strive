using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel.Messenger;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Messenger
{
    public interface IMessengerBpl
    {
        Result SendMessenge(ChatMessageDto chatMessageDto);
        int CreateGroup(ChatGroupDto chatGroupDto);
        Result ChatCommunication(ChatCommunicationDto chatCommunicationDto);
        Result GetChatEmployeeList(int employeeId);
        Result GetChatMessage(ChatDto chatDto);
        Result GetUnReadMessageCount(int employeeid);
        Result GetChatGroupEmployeelist(int chatGroupId);
        ChatGroupListViewModel GetChatEmployeeGrouplist(int employeeId);
        Result DeleteChatGroupUser(int chatUserGroupId);
        Result ChangeUnreadMessageState(ChatDto chatDto);
        Result GetAllEmployeeName(SearchDto searchDto);
    }
}
