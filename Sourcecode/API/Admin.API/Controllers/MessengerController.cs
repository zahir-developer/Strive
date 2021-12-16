using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessEntities.ViewModel.Messenger;
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
        public async Task<Result> ChatCommunication([FromBody] ChatCommunicationDto chatCommunicationDto)
        {

            var result = _bplManager.GetChatEmployeeGrouplist(chatCommunicationDto.EmployeeId.GetValueOrDefault());

            if (result.ChatGroupList != null)
            {
                foreach (var grp in result.ChatGroupList)
                {
                    if (grp.GroupId != null)
                    {
                        await _hubContext.Groups.AddToGroupAsync(chatCommunicationDto.CommunicationId, grp.GroupId);
                        await _hubContext.Clients.Group(grp.GroupId).SendAsync("UserAddedtoGroup", "EmployeeId:" + chatCommunicationDto.EmployeeId + "GroupName" + grp.GroupName + ", GroupID " + grp.GroupId + ", CommunicationId: " + chatCommunicationDto.CommunicationId + " added.");
                    }
                }
            }

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
                    if(_hubContext != null)
                        if(_hubContext.Clients != null && chatMessageDto.ConnectionId != null)
                            await _hubContext.Clients.Client(chatMessageDto.ConnectionId).SendAsync("ReceivePrivateMessage", chatMessageDto);
                }
                else if (chatMessageDto.ChatMessageRecipient.RecipientGroupId > 0 && chatMessageDto.ChatMessageRecipient.RecipientId == null)
                {
                    if (chatMessageDto.GroupId != null)
                    {
                        if (_hubContext != null)
                            if (_hubContext.Clients != null && chatMessageDto.GroupId != null)
                                await _hubContext.Clients.Group(chatMessageDto.GroupId).SendAsync("ReceiveGroupMessage", chatMessageDto);
                                //await _hubContext.Clients.All.SendAsync("ReceiveGroupMessage", chatMessageDto);

                    }
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
            JObject _resultContent = new JObject();
            Result _result = new Result();

            CommonBpl commonBpl = new CommonBpl();

            ChatGroupViewModel chatGroupViewModel = new ChatGroupViewModel();


            if (chatGroupDto.ChatGroup != null)
            {
                string groupId = "Group" + "_" + commonBpl.RandomString(5);
                chatGroupDto.ChatGroup.GroupId = groupId;
                chatGroupDto.GroupId = groupId;
            }

            var result = _bplManager.CreateGroup(chatGroupDto);

            foreach (var user in chatGroupDto.ChatUserGroup)
            {
                if (user.CommunicationId != null && !string.IsNullOrEmpty(chatGroupDto.GroupId))
                {
                    await _hubContext.Groups.AddToGroupAsync(user.CommunicationId, chatGroupDto.GroupId);
                    await _hubContext.Clients.Group(chatGroupDto.GroupId).SendAsync("GroupMessageReceive", user.UserId + " has joined.");
                }
            }
            chatGroupViewModel.GroupId = chatGroupDto.GroupId;
            chatGroupViewModel.ChatGroupId = result;

            _resultContent.Add(chatGroupViewModel.WithName("Result"));
            _result = Helper.BindSuccessResult(_resultContent);
            return _result;
        }

        /// <summary>
        /// Returns the recent private and group chat contact list.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChatEmployeeList/{employeeId}")]
        public Result GetAllEmployeeList(int employeeId)
        {
            return _bplManager.GetChatEmployeeList(employeeId);
        }

        /// <summary>
        /// Returns the private chat and group chat information.
        /// </summary>
        /// <param name="chatDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatMessage")]
        public Result GetChatMessage([FromBody] ChatDto chatDto)
        {
            return _bplManager.GetChatMessage(chatDto);
        }

        /// <summary>
        /// Retreives list of unread messae count from each sen.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUnReadMessageCount/{employeeId}")]
        public Result GetUnReadMessageCount(int employeeId)
        {
            return _bplManager.GetUnReadMessageCount(employeeId);
        }

        /// <summary>
        /// Method to retrieve list of user belongs to a group.
        /// </summary>
        /// <param name="chatGroupId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Method to remove user from group
        /// </summary>
        /// <param name="chatGroupUserId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteChatGroupUser/{chatGroupUserId}")]
        public Result DeleteChatGroupUser(int chatGroupUserId) => _bplManager.DeleteChatGroupUser(chatGroupUserId);


        /// <summary>
        /// Changes the status of unread messages.
        /// </summary>
        /// <param name="chatDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeUnreadMessageState")]
        public Result ChangeUnreadMessageState([FromBody] ChatDto chatDto) => _bplManager.ChangeUnreadMessageState(chatDto);


        [HttpPost]
        [Route("GetAllEmployeeName")]
        public Result GetAllEmployee([FromBody]SearchDto searchDto) => _bplManager.GetAllEmployeeName(searchDto);
    }
}