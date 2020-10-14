using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessLogic.Messenger;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Route("Admin/[controller]")]
    [ApiController]
    public class MessengerController : StriveControllerBase<IMessengerBpl>
    {

        public MessengerController(IMessengerBpl messengerBpl) : base(messengerBpl)
        {

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

            /*await chatHub.SendPrivateMessage(
                       chatMessageDto.ConnectionId,
                       chatMessageDto.ChatMessageRecipient?.SenderId.GetValueOrDefault().ToString(),
                       chatMessageDto.FullName,
                       chatMessageDto.ChatMessage?.Messagebody);*/

            
            if(result.Status.ToLower().Equals("true"))
            {
                if (chatMessageDto.ChatMessageRecipient.RecipientGroupId == null && chatMessageDto.ChatMessageRecipient.RecipientId > 0)
                {
                    /*await chatHub.SendPrivateMessage(
                        chatMessageDto.ConnectionId,
                        chatMessageDto.ChatMessageRecipient.SenderId.GetValueOrDefault().ToString(),
                        chatMessageDto.FullName,
                        chatMessageDto.ChatMessage.Messagebody);*/
                }
                else if (chatMessageDto.ChatMessageRecipient.RecipientGroupId > 0 && chatMessageDto.ChatMessageRecipient.RecipientId == null)
                    await chatHub.SendMessageToGroup(
                        chatMessageDto.GroupName,
                        chatMessageDto.ChatMessageRecipient.SenderId.GetValueOrDefault().ToString(),
                        chatMessageDto.FullName,
                        chatMessageDto.ChatMessage.Messagebody);
            }
            return result;
        }

        /// <summary>
        /// Method to create/update group with list of users.
        /// </summary>
        [HttpPost]
        [Route("CreateChatGroup")]
        public Result CreateGroup([FromBody] ChatGroupDto chatGroupDto)
        {
            return _bplManager.CreateGroup(chatGroupDto);
        }

        [HttpGet]
        [Route("GetChatEmployeeList")]
        public Result GetAllEmployeeList() => _bplManager.GetChatEmployeeList();

        [HttpPost]
        [Route("GetChatMessage")]
        public Result GetChatMessage([FromBody] ChatDto chatDto)
        {
            return _bplManager.GetChatMessage(chatDto);
        }
    }
}