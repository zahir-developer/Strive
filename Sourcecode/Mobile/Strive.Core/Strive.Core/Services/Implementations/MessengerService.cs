using MvvmCross;
using Strive.Core.Models;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;
using System.Net.Http;
using System.Threading.Tasks;

namespace Strive.Core.Services.Implementations
{
    public class MessengerService : IMessengerService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();

        public async Task<Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeMessengerContacts> GetContacts(Models.Employee.Messenger.MessengerContacts.GetAllEmployeeDetail_Request employeeContacts)
        {
            var data = await _restClient.MakeApiCall<Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeMessengerContacts>(ApiUtils.URL_MESSENGER_CONTACTS, HttpMethod.Post, employeeContacts);
            return data;//await _restClient.MakeApiCall<Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList>(ApiUtils.URL_MESSENGER_CONTACTS, HttpMethod.Post, employeeContacts);
        }

        public async Task<PersonalChatMessages> GetPersonalChatMessages(ChatDataRequest chatData)
        {
            return await _restClient.MakeApiCall<PersonalChatMessages>(ApiUtils.URL_MESSENGER_PERSONAL_CHATS, HttpMethod.Post, chatData);
        }

        public async Task<Models.Employee.Messenger.EmployeeLists> GetRecentContacts(int employeeId)
        {
            return await _restClient.MakeApiCall<Models.Employee.Messenger.EmployeeLists>(string.Format(ApiUtils.URL_MESSENGER_RECENT_CONTACTS, employeeId), HttpMethod.Get);
        }

        public async Task<PostResponseBool> SendChatMessage(SendChatMessage chatMessage)
        {
            return await _restClient.MakeApiCall<PostResponseBool>(ApiUtils.URL_MESSENGER_SEND_CHAT_MESSAGE,  HttpMethod.Post, chatMessage);
        }

        public async Task<GroupChatResponse> CreateChatGroup(CreateGroupChat groupInfo)
        {
            return await _restClient.MakeApiCall<GroupChatResponse>(ApiUtils.URL_MESSENGER_CREATE_GROUP_CHAT, HttpMethod.Post, groupInfo);
        }

        public async Task<Models.Employee.Messenger.EmployeeLists> GetParticipants(int groupID)
        {
            return await _restClient.MakeApiCall<Models.Employee.Messenger.EmployeeLists>(string.Format(ApiUtils.URL_GET_GROUP_PARTICIPANTS, groupID), HttpMethod.Get);
        }

        public async Task<ChatGroupUserDeleted> DeleteGroupUser(int? GroupUserId)
        {
            return await _restClient.MakeApiCall<ChatGroupUserDeleted>(string.Format(ApiUtils.URL_GROUP_PARTICIPANT_DELETE, GroupUserId), HttpMethod.Delete);
        }

        public async Task<PostResponseBool> ChatCommunication(ChatCommunication communicationDetails)
        {
            return await _restClient.MakeApiCall<PostResponseBool>(ApiUtils.URL_MESSENGER_CHAT_COMMUNICATION, HttpMethod.Post, communicationDetails);
        }
    }
}
