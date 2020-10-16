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

        public ChatEmployeeListViewModel GetChatEmployeeList()
        {
            return db.FetchMultiResult<ChatEmployeeListViewModel>(EnumSP.Messenger.USPGETChatEMPLOYEELIST.ToString(), _prm);
        }

        public ChatMessageDetailViewModel GetChatMessage(ChatDto chatDto)
        {
            _prm.Add("@SenderId", chatDto.SenderId);
            _prm.Add("@RecipientId", chatDto.RecipientId);
            _prm.Add("@GroupId", chatDto.GroupId);

            return db.FetchMultiResult<ChatMessageDetailViewModel>(EnumSP.Messenger.GETCHATMESSAGE.ToString(), _prm);
        }

        public GetUnReadMessageViewModel GetUnReadMessageCount(int employeeid)
        {
            _prm.Add("@Employeeid", employeeid);
            return db.FetchMultiResult<GetUnReadMessageViewModel>(EnumSP.Messenger.GETCHATMESSAGECOUNT.ToString(), _prm);
        }

    }
}
