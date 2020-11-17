using Strive.Core.Models;
using Strive.Core.Models.Employee;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using employeeLists = Strive.Core.Models.Employee.Messenger.MessengerContacts.EmployeeLists;

namespace Strive.Core.Services.Interfaces
{
    public interface IMessengerService
    {
        Task<EmployeeLists> GetRecentContacts(int employeeId);
        Task<employeeLists> GetContacts(string contactName);
        Task<EmployeeLists> GetParticipants(int GroupID);
        Task<PersonalChatMessages> GetPersonalChatMessages(ChatDataRequest chatData);
        Task<PostResponseBool> SendChatMessage(SendChatMessage chatMessage);
        Task<GroupChatResponse> CreateChatGroup(CreateGroupChat groupInfo);
    }
}
