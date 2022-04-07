﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;

namespace Greeter.Modules.Message
{
    public partial class GroupViewController
    {
        List<RecentChat> groups = new();
        List<RecentChat> searchedGroups;

        public GroupViewController()
        {
            //groups.Add("OM Detailers");
            //groups.Add("OM Washers");
            //groups.Add("MS Washers");
            //groups.Add("MS Detailers");
            //groups.Add("Old Milton Employees");
            //groups.Add("Main Street Employees");
            //groups.Add("Holcomb Bridge Employees");

            //searchedGroups = groups;
        }

        async Task GetGroupChatsFromApi()
        {
            var response = await SingleTon.MessageApiService.GetRecentChatList(AppSettings.UserID);

            HandleResponse(response);

            if (!response.IsSuccess()) return;

            if (response?.EmployeeList?.RecentChats is not null)
                searchedGroups = groups = response?.EmployeeList?.RecentChats?.Where(x => x.IsGroup).ToList();

            RefreshGroupsToUI();
        }

        async Task GetGroupChats()
        {
            ShowActivityIndicator();
            await GetGroupChatsFromApi();
            HideActivityIndicator();
        }

        async Task SearchGroup(string keyword)
        {
            searchedGroups = await Task.Run(() => groups.Where(group => Logic.FullName(group.FirstName, group.LastName).ToLower().Contains(keyword.ToLower().Trim())).ToList());
            RefreshGroupsToUI();
        }

        async Task OnRefersh()
        {
            await GetGroupChatsFromApi();
            refreshControl.EndRefreshing();
        }
    }
}