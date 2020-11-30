

-- =============================================
-- Author:		<Author, Zahir>
-- Create date: 09-10-2010
-- Description:	Returns the list of employee with chat communication details
-- =============================================

CREATE PROC [StriveCarSalon].[uspGetEmployeeRecentChatHistory] -- [StriveCarSalon].[uspGetEmployeeRecentChatHistory] 1
@EmployeeId INT = NULL
AS
BEGIN

SELECT Distinct
emp.EmployeeId as Id,
emp.FirstName,
emp.LastName,
--ISNULL(chatComm.ChatCommunicationId, 0) as ChatCommunicationId,
ISNULL(chatComm.CommunicationId, 0) as CommunicationId,
isnull(emp.IsActive,1) as Status,
(Select top 1 CONCAT(msg.CreatedDate,',', msg.Messagebody,',',chatRecp.IsRead) from StriveCarSalon.tblChatMessageRecipient chatRecp 
LEFT JOIN StriveCarSalon.tblChatMessage msg on msg.ChatMessageId = chatRecp.ChatMessageId
where (chatRecp.RecipientId = emp.EmployeeId and chatRecp.SenderId = @EmployeeId) OR 
(chatRecp.RecipientId = @EmployeeId and chatRecp.SenderId = emp.EmployeeId)
order by 1 desc) RecentChatMessage
FROM 
StriveCarSalon.tblEmployee emp 
LEFT JOIN StriveCarSalon.tblChatCommunication chatComm on emp.EmployeeId = chatComm.EmployeeId
INNER JOIN tblChatMessageRecipient chatRecp on chatRecp.RecipientId = emp.EmployeeId
AND (emp.IsDeleted=0 OR emp.IsDeleted IS NULL) 
AND (emp.IsDeleted = 0 OR emp.IsDeleted IS NULL)
WHERE emp.EmployeeId != @employeeId and chatRecp.SenderId = @EmployeeId OR @EmployeeId is NULL
ORDER BY 1 DESC

SELECT DISTINCT grp.ChatGroupId,GroupName, grp.GroupId,--msgRecp.CreatedDate,
(Select top 1 CONCAT(chatMsg.CreatedDate,',',chatMsg.Messagebody,',',msgRecp.IsRead) from StriveCarSalon.tblChatMessage chatMsg
JOIN StriveCarSalon.tblChatMessageRecipient msgRecp on msgRecp.RecipientGroupId = grp.ChatGroupId order by 1 desc) as RecentChatMessage from StriveCarSalon.tblChatGroup grp
JOIN StriveCarSalon.tblChatUserGroup uGrp on uGrp.chatgroupId = grp.chatGroupId
LEFT JOIN StriveCarsalon.tblChatMessageRecipient msgRecp on msgRecp.RecipientGroupId = grp.ChatGroupId
where ugrp.UserId = @EmployeeId OR @EmployeeId is NULL


END