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
    }

    public class MessageApiService : IMessageApiService
    {
        public Task<RecentChatListResponse> GetRecentChatList(long empId)
        {
            var url = Urls.RECENT_CHAT_LIST + empId;
            return SingleTon.ApiService.DoApiCall<RecentChatListResponse>(url);
        }

        public Task<ContactListResponse> GetContactsList(GetContactsRequest req)
        {
            return SingleTon.ApiService.DoApiCall<ContactListResponse>(Urls.CONTACTS_LIST, HttpMethod.Post, null, req);
        }
    }
}
