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
        List<string> recentMessageHistory = new();

        public RecentMessageViewController()
        {
            recentMessageHistory.Add("Brittany Rose");
            recentMessageHistory.Add("OM Detailers");
            recentMessageHistory.Add("Peter Parker");
            recentMessageHistory.Add("Daniel Steel");
            recentMessageHistory.Add("Old Milton Employees");
            _ = GetRecentChatsAsync();
        }

        Task GetRecentsMessageHistory()
        {
            return Task.CompletedTask;
        }

        async Task<List<RecentChat>> GetRecentChatsAsync()
        {
            ShowActivityIndicator();
            var response = await new MessageApiService().GetRecentChatList(AppSettings.UserID);
            HideActivityIndicator();

            List<RecentChat> recentChats = null;

            HandleResponse(response);

            if (response.IsSuccess())
            {
                recentChats = response?.EmployeeList?.RecentChats;
            }

            return recentChats;
        }
    }
}
