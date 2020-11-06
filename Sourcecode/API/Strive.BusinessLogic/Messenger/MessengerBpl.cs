﻿using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel.Messenger;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Messenger
{
    public class MessengerBpl : Strivebase, IMessengerBpl
    {
        public MessengerBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }

        public Result ChatCommunication(ChatCommunicationDto chatCommunicationDto)
        {
            return ResultWrap(new MessengerRal(_tenant).ChatCommunication, chatCommunicationDto, "Status");
        }

        public Result SendMessenge(ChatMessageDto chatMessageDto)
        {
           return ResultWrap(new MessengerRal(_tenant).SendMessege, chatMessageDto, "Status");
        }

        public int CreateGroup(ChatGroupDto chatGroupDto)
        {
            return new MessengerRal(_tenant).CreateChatGroup(chatGroupDto);
        }

        public Result GetChatEmployeeList(int employeeId)
        {
            var result = new MessengerRal(_tenant).GetChatEmployeeList(employeeId);

            EmployeeChatHistoryViewModel chatHistory = new EmployeeChatHistoryViewModel();
            chatHistory.ChatEmployeeList = new List<ChatEmployeeList>();
            ChatEmployeeList list;

            if (result.ChatEmployeeList != null)
            {
                foreach (var item in result.ChatEmployeeList)
                {
                    string[] msgDetail = item.RecentChatMessage != null ? item.RecentChatMessage.Split(','): new string[3];
                    string dateTime = msgDetail[0];
                    string msg = msgDetail[1];
                    string isRead = msgDetail[2];

                    list = new ChatEmployeeList()
                    {

                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        CommunicationId = item.CommunicationId,
                        RecentChatMessage = msg,
                        IsRead = isRead == "1" ? true : false,
                        CreatedDate = dateTime,
                        IsGroup = false,
                        Selected = chatHistory.ChatEmployeeList.Count == 0 ? true : false
                };
                    chatHistory.ChatEmployeeList.Add(list);
                }
            }

            if (result.GroupList != null)
            {
                foreach (var item in result.GroupList)
                {
                    string[] msgDetail = item.RecentChatMessage != null ? item.RecentChatMessage.Split(',') : new string[2];
                    string dateTime = msgDetail[0];
                    string msg = msgDetail[1];

                    list = new ChatEmployeeList()
                    {
                        Id = item.ChatGroupId,
                        FirstName = item.GroupName,
                        RecentChatMessage = msg,
                        CommunicationId = item.GroupId,
                        IsGroup = true,
                        GroupId = item.GroupId,
                        CreatedDate = dateTime,
                        IsRead = item.IsRead,
                        Selected = chatHistory.ChatEmployeeList.Count == 0 ? true : false
                    };
                    chatHistory.ChatEmployeeList.Add(list);
                }
            }
            return ResultWrap(chatHistory, "EmployeeList");
        }

        public Result GetChatMessage(ChatDto chatDto)
        {
            return ResultWrap(new MessengerRal(_tenant).GetChatMessage, chatDto, "ChatMessage");
        }
        public Result GetUnReadMessageCount(int employeeid)
        {
            return ResultWrap(new MessengerRal(_tenant).GetUnReadMessageCount, employeeid, "UnreadMessage");
        }

        public Result GetChatGroupEmployeelist(int chatGroupId)
        {
            return ResultWrap(new MessengerRal(_tenant).GetChatGroupEmployeelist, chatGroupId, "EmployeeList");
        }

        public ChatGroupListViewModel GetChatEmployeeGrouplist(int employeeId)
        {
            return new MessengerRal(_tenant).GetChatEmployeeGrouplist(employeeId);
        }

        public Result DeleteChatGroupUser(int chatGroupUserId)
        {
            return ResultWrap(new MessengerRal(_tenant).DeleteChatGroupUser, chatGroupUserId, "ChatGroupUserDelete");
        }
    }
}
