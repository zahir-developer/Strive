CREATE PROCEDURE [StriveCarSalon].[GetChatMessagePrevious] --1,100,0
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
WHERE chatRecp.SenderId = @SenderId and chatRecp.RecipientId = @RecipientId OR (chatRecp.SenderId = @RecipientId and chatRecp.RecipientId = @SenderId )
OR (@SenderId = NULL AND @RecipientId = NULL AND chatRecp.RecipientId = @GroupId)

END