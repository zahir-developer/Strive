using System;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface IMessageApiService
    {
        Task<RecentChatListResponse> GetRecentChatList(long empId);
        Task<ContactListResponse> GetContactsList(GetContactsRequest req);
        Task<ChatMessagesResponse> GetChatMessages(ChatMessageRequest req);
        Task<CreateGroupResponse> CreateGroup(CreategroupRequest req);
        public Task<BaseResponse> AddUserToGroup(long userId, long communicationId);
        Task<RemoveUserFromGroupResponse> RemoveUserFromGroup(long groupUserId);
        Task<GroupUsersResponse> GetGroupUsers(long groupId);
    }

    public class MessageApiService : IMessageApiService
    {
        readonly IApiService apiService = SingleTon.ApiService;

        public Task<RecentChatListResponse> GetRecentChatList(long empId)
        {
            var url = Urls.RECENT_CHAT_LIST + empId;
            return apiService.DoApiCall<RecentChatListResponse>(url);
        }

        public Task<ContactListResponse> GetContactsList(GetContactsRequest req)
        {
            return apiService.DoApiCall<ContactListResponse>(Urls.CONTACTS_LIST, HttpMethod.Post, null, req);
        }

        public Task<CreateGroupResponse> CreateGroup(CreategroupRequest req)
        {
            return apiService.DoApiCall<CreateGroupResponse>(Urls.CREATE_GROUP, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> AddUserToGroup(long userId, long communicationId)
        {
            var url = Urls.ADD_USER_TO_GROUP + userId + "/" + communicationId;
            return apiService.DoApiCall<BaseResponse>(url, HttpMethod.Delete);
        }

        public Task<GroupUsersResponse> GetGroupUsers(long groupId)
        {
            var url = Urls.GROUP_USERS + groupId;
            return apiService.DoApiCall<GroupUsersResponse>(url, HttpMethod.Delete);
        }

        public Task<BaseResponse> GetGroupMembers(long userId, long communicationId)
        {
            var url = Urls.ADD_USER_TO_GROUP + userId + "/" + communicationId;
            return apiService.DoApiCall<BaseResponse>(url, HttpMethod.Delete);
        }

        public Task<RemoveUserFromGroupResponse> RemoveUserFromGroup(long groupUserId)
        {
            var url = Urls.REMOVE_USER_FROM_GROUP + groupUserId;
            return apiService.DoApiCall<RemoveUserFromGroupResponse>(url, HttpMethod.Delete);
        }

        public Task<ChatMessagesResponse> GetChatMessages(ChatMessageRequest req)
        {
            return apiService.DoApiCall<ChatMessagesResponse>(Urls.CHAT_MESSAGES, HttpMethod.Post, null, req);
        }

        //public Task<ChatMessagesResponse> SendMesasge()
        //{
            //return apiService.DoApiCall<ChatMessagesResponse>(Urls.CHAT_MESSAGES, HttpMethod.Post, null, req);
        //}
    }
}
