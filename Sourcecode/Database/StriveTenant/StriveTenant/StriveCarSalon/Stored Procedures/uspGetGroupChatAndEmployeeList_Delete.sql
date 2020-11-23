
CREATE PROC [StriveCarSalon].[uspGetGroupChatAndEmployeeList_Delete]  
@EmployeeId INT = NULL
AS

-- ==================================================================================
-- Author:		Vineeth
-- Create date: 14-10-2010
-- Description:	Returns the list of employee in group with chat communication details
-- ==================================================================================
BEGIN

SELECT Distinct
emp.EmployeeId,
emp.FirstName,
emp.LastName,
ISNULL(chatComm.ChatCommunicationId, 0) as ChatCommunicationId,
ISNULL(chatComm.CommunicationId, 0) as CommunicationId,
isnull(emp.IsActive,1) as Status,
(Select top 1 emp.CreatedDate from tblChatMessageRecipient chatRecp where chatRecp.SenderId = emp.EmployeeId order by emp.CreatedDate desc) as RecentChatDateTime,
tblcg.GroupName,
tblcug.UserId AS ListOfEmployee
FROM 
StriveCarSalon.tblEmployee emp 
LEFT JOIN StriveCarSalon.tblChatCommunication chatComm ON (emp.EmployeeId = chatComm.EmployeeId)
INNER JOIN tblChatMessageRecipient chatRecp ON (chatRecp.RecipientId = emp.EmployeeId)
INNER JOIN tblChatUserGroup tblcug ON(tblcug.UserId = emp.EmployeeId)
INNER JOIN tblChatGroup tblcg ON(tblcg.ChatGroupId = tblcug.ChatGroupId)
AND ISNULL(emp.IsDeleted,0) =0
AND ISNULL(tblcug.IsDeleted,0) = 0
AND ISNULL(tblcg.IsDeleted,0)= 0
AND tblcug.IsActive=1 AND tblcg.IsActive=1
WHERE (chatRecp.SenderId = @EmployeeId OR @EmployeeId is NULL)
ORDER BY emp.EmployeeId DESC
END