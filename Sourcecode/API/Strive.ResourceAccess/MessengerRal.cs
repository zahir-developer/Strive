using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.ViewModel.Employee;
using Strive.BusinessEntities.ViewModel.Messenger;
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
            return dbRepo.InsertInt64(chatMessegeDto, "ChatMessageId");
        }

        public int CreateChatGroup(ChatGroupDto chatGroupDto)
        {
            return dbRepo.InsertPK(chatGroupDto, "ChatGroupId");
        }

        public bool ChatCommunication(ChatCommunicationDto chatCommunicationDto)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@EmployeeId", chatCommunicationDto.EmployeeId);
            dynParams.Add("@CommunicationId", chatCommunicationDto.CommunicationId);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Messenger.UPDATECHATCOMMUNICATIONDETAIL.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public EmployeeChatHistoryViewModel GetChatEmployeeList(int employeeId)
        {
            _prm.Add("@EmployeeId", employeeId);
            return db.FetchMultiResult<EmployeeChatHistoryViewModel>(EnumSP.Messenger.USPGETCHATEMPLOYEEANDGROUPHISTORY.ToString(), _prm);
        }

        public ChatMessageDetailViewModel GetChatMessage(ChatDto chatDto)
        {
            _prm.Add("@SenderId", chatDto.SenderId);
            _prm.Add("@RecipientId", chatDto.RecipientId);
            _prm.Add("@GroupId", chatDto.GroupId);

            return db.FetchMultiResult<ChatMessageDetailViewModel>(EnumSP.Messenger.USPGETCHATMESSAGE.ToString(), _prm);
        }

        public GetUnReadMessageViewModel GetUnReadMessageCount(int employeeid)
        {
            _prm.Add("@Employeeid", employeeid);
            return db.FetchMultiResult<GetUnReadMessageViewModel>(EnumSP.Messenger.USPGETCHATMESSAGECOUNT.ToString(), _prm);
        }

        public EmployeeChatHistoryViewModel GetChatGroupEmployeelist(int chatGroupId)
        {
            _prm.Add("@GroupId", chatGroupId);
            return db.FetchMultiResult<EmployeeChatHistoryViewModel>(EnumSP.Messenger.USPGETCHATGROUPEMPLOYEELIST.ToString(), _prm);
        }

        public ChatGroupListViewModel GetChatEmployeeGrouplist(int employeeId)
        {
            _prm.Add("@EmployeeId", employeeId);
            return db.FetchMultiResult<ChatGroupListViewModel>(EnumSP.Messenger.USPGETCHATEMPLOYEEGROUPLIST.ToString(), _prm);
        }

        public bool DeleteChatGroupUser(int chatGroupUserId)
        {
            _prm.Add("ChatGroupUserId", chatGroupUserId);
            db.Save(EnumSP.Messenger.USPDELETECHATUSERGROUP.ToString(), _prm);
            return true;
        }

        public bool ChangeUnreadMessageState(ChatDto chatDto)
        {
            _prm.Add("senderId", chatDto.SenderId);
            _prm.Add("recipientId", chatDto.RecipientId);
            _prm.Add("groupId", chatDto.GroupId);

            db.Save(EnumSP.Messenger.USPUPDATECHATUNREADMESSAGESTATE.ToString(), _prm);
            return true;
        }

        public EmployeeNameViewModel GetAllEmployeeName(SearchDto searchDto)
        {
            _prm.Add("@PageNo", searchDto.PageNo);
            _prm.Add("@PageSize", searchDto.PageSize);
            _prm.Add("@Query", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            return db.FetchMultiResult<EmployeeNameViewModel>(EnumSP.Messenger.USPGETALLEMPLOYEENAME.ToString(), _prm);
        }
    }
}
