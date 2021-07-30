using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;

namespace Greeter.Modules.Message
{
    public partial class RecentMessageViewController
    {
        List<RecentChat> recentMessageHistory = new();

        public RecentMessageViewController()
        {
            _ = GetRecentChatsAsync();
        }

        async Task GetRecentChatsAsync()
        {
            ShowActivityIndicator();

            MessageApiService messageApiService = new MessageApiService();
            var response = await messageApiService.GetRecentChatList(AppSettings.UserID);
            HideActivityIndicator();

            HandleResponse(response);

            if (!response.IsSuccess()) return;

            if (response?.EmployeeList?.RecentChats is not null)
                recentMessageHistory = response?.EmployeeList?.RecentChats;

            RefreshRecentChat();
        }
    }
}
