﻿using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Messenger;
using Strive.BusinessEntities.Model;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Messenger
{
    public interface IMessengerBpl
    {
        Result SendMessenge(ChatMessageDto chatMessageDto);
        Result CreateGroup(ChatGroupDto chatGroupDto);
        Result ChatCommunication(ChatCommunicationDto chatCommunicationDto);
        Result GetChatEmployeeList();
        Result GetChatMessage(ChatDto chatDto);
    }
}