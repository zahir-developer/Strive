using System;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;

namespace Greeter.Services.Api
{
    public class MessageApiService
    {
        readonly IApiService apiService = new ApiService();

        public Task<RecentChatListResponse> GetRecentChatList(long empId)
        {
            var url = Urls.RECENT_CHAT_LIST + empId;
            return apiService.DoApiCall<RecentChatListResponse>(url);
        }
    }
}
