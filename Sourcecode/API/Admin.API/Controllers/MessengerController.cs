using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessLogic;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.Messenger;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Route("Admin/[controller]")]
    [ApiController]
    public class MessengerController : StriveControllerBase<IMessengerBpl>
    {

        private readonly IHubContext<ChatMessageHub> _hubContext;

        public MessengerController(IMessengerBpl messengerBpl, IHubContext<ChatMessageHub> hubContext) : base(messengerBpl)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// Chat message user logged In
        /// </summary>
        [HttpPost]
        [Route("ChatCommunication")]
        public Result ChatCommunication([FromBody] ChatCommunicationDto chatCommunicationDto)
        {
            return _bplManager.ChatCommunication(chatCommunicationDto);
        }

        /// <summary>
        /// Method to send private chat and group chat message
        /// </summary>
        [HttpPost]
        [Route("SendChatMessage")]
        public async Task<Result> SendChatMessage([FromBody] ChatMessageDto chatMessageDto)
        {
            ChatMessageHub chatHub = new ChatMessageHub();

            Result result = _bplManager.SendMessenge(chatMessageDto);

            if (result.Status.Equals("Success"))
            {
                if (chatMessageDto.ChatMessageRecipient.RecipientGroupId == null && chatMessageDto.ChatMessageRecipient.RecipientId > 0)
                {
                    await _hubContext.Clients.Client(chatMessageDto.ConnectionId).SendAsync("ReceivePrivateMessage", chatMessageDto);
                }
                else if (chatMessageDto.ChatMessageRecipient.RecipientGroupId > 0 && chatMessageDto.ChatMessageRecipient.RecipientId == null)
                {

                    await _hubContext.Clients.Group(chatMessageDto.GroupId).SendAsync("ReceivePrivateMessage", chatMessageDto);

                }
            }
            return result;
        }

        /// <summary>
        /// Method to create/update group with list of users.
        /// </summary>
        [HttpPost]
        [Route("CreateChatGroup")]
        public async Task<Result> CreateGroup([FromBody] ChatGroupDto chatGroupDto)
        {
            CommonBpl commonBpl = new CommonBpl();

            string groupName = "Group" + "_" + commonBpl.RandomString(5);
            if (chatGroupDto.ChatGroup != null)
                chatGroupDto.ChatGroup.GroupId = groupName;
            var result = _bplManager.CreateGroup(chatGroupDto);

            foreach (var user in chatGroupDto.ChatUserGroup)
            {
                if (user.CommunicationId != null)
                {
                    await _hubContext.Groups.AddToGroupAsync(user.CommunicationId, groupName);
                    await _hubContext.Clients.Group(groupName).SendAsync("GroupMessageReceive", user.UserId + " has joined.");
                }
            }
            return result;
        }

        [HttpGet]
        [Route("GetChatEmployeeList/{employeeId}")]
        public Result GetAllEmployeeList(int employeeId)
        {
            return _bplManager.GetChatEmployeeList(employeeId);
        }

        [HttpPost]
        [Route("GetChatMessage")]
        public Result GetChatMessage([FromBody] ChatDto chatDto)
        {
            return _bplManager.GetChatMessage(chatDto);
        }
        [HttpGet]
        [Route("GetUnReadMessageCount/{employeeId}")]
        public Result GetUnReadMessageCount(int employeeId)
        {
            return _bplManager.GetUnReadMessageCount(employeeId);
        }

        [HttpGet]
        [Route("GetChatGroupEmployeelist/{chatGroupId}")]
        public Result GetChatGroupEmployeelist(int chatGroupId)
        {
            return _bplManager.GetChatGroupEmployeelist(chatGroupId);
        }

        [HttpPut]
        [Route("AddEmployeeToGroup/{employeeId}/{communicationId}")]
        public async Task<bool> AddEmployeeToGroup(int employeeId, string communicationId)
        {
            var result = _bplManager.GetChatEmployeeGrouplist(employeeId);

            foreach (var grp in result.ChatGroupList)
            {
                await _hubContext.Groups.AddToGroupAsync(communicationId, grp.GroupId);
                await _hubContext.Clients.Group(grp.GroupId).SendAsync("UserAddedtoGroup", "EmployeeId:" + employeeId + ", CommunicationId: " + communicationId + " added.");
            }

            return true;
        }


    }
}