﻿-- =============================================
-- Author:		Zahir Hussain
-- Create date: 09-10-2020
-- Description:	Retrieves the chat messages 
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[GetChatMessage]
	@SenderId INT = NULL,
	@RecipientId INT = NULL,
	@GroupId INT = NULL
AS
BEGIN

Select chatMsg.ChatMessageId, emp.employeeId as SenderId, emp.firstName as SenderFirstName, emp.lastName as SenderLastName, chatMsg.Messagebody, chatMsg.CreatedDate,
emp2.employeeId as RecipientId, emp2.firstName as RecipientFirstName, emp2.lastName as RecipientLastName
from strivecarsalon.tblChatMessageRecipient chatRecp 
INNER JOIN tblChatMessage chatMsg on chatMsg.chatMessageId = chatRecp.chatMessageId
LEFT JOIN tblEmployee emp on emp.EmployeeId = chatRecp.SenderId
LEFT JOIN tblEmployee emp2 on emp2.EmployeeId = chatRecp.RecipientId
WHERE ( @SenderId = 0 AND @RecipientId = 0 AND chatRecp.RecipientGroupId = @GroupId)
OR (chatRecp.SenderId = @SenderId and chatRecp.RecipientId = @RecipientId)
OR (chatRecp.SenderId = @RecipientId and chatRecp.RecipientId = @SenderId )

END