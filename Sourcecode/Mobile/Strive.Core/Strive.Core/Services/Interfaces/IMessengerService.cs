using Strive.Core.Models;
using Strive.Core.Models.Employee;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using employeeLists = Strive.Core.Models.Employee.Messenger.MessengerContacts.EmployeeLists;
using EmployeeLists = Strive.Core.Models.Employee.Messenger.EmployeeLists;

namespace Strive.Core.Services.Interfaces
{
    public interface IMessengerService
    {
        Task<EmployeeLists> GetRecentContacts(int employeeId);
        Task<Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeMessengerContacts> GetContacts(GetAllEmployeeDetail_Request employeeContacts);
        Task<EmployeeLists> GetParticipants(int GroupID);
        Task<PersonalChatMessages> GetPersonalChatMessages(ChatDataRequest chatData);
        Task<PostResponseBool> SendChatMessage(SendChatMessage chatMessage);
        Task<GroupChatResponse> CreateChatGroup(CreateGroupChat groupInfo);
        Task<ChatGroupUserDeleted> DeleteGroupUser(int? GroupUserId);
        Task<PostResponseBool> ChatCommunication(ChatCommunication communicationDetails);
    }
}
