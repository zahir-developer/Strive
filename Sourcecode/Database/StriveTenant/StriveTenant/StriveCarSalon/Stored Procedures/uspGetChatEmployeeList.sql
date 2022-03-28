

-- =============================================
-- Author:		<Author, Zahir>
-- Create date: 09-10-2010
-- Description:	Returns the list of employee with chat communication details
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetChatEmployeeList] 
@EmployeeId INT = NULL
AS
BEGIN

SELECT Distinct
emp.EmployeeId,
emp.FirstName,
emp.LastName,
ISNULL(chatComm.ChatCommunicationId, 0) as ChatCommunicationId,
ISNULL(chatComm.CommunicationId, 0) as CommunicationId,
isnull(emp.IsActive,1) as Status,
(Select top 1 CreatedDate from tblChatMessageRecipient chatRecp where chatRecp.SenderId = emp.EmployeeId order by CreatedDate desc) as RecentChatDateTime
FROM 
tblEmployee emp 
LEFT JOIN tblChatCommunication chatComm on emp.EmployeeId = chatComm.EmployeeId
INNER JOIN tblChatMessageRecipient chatRecp on chatRecp.RecipientId = emp.EmployeeId
AND (emp.IsDeleted=0 OR emp.IsDeleted IS NULL) 
AND (emp.IsDeleted = 0 OR emp.IsDeleted IS NULL)
WHERE chatRecp.SenderId = @EmployeeId OR @EmployeeId is NULL
ORDER BY 1 DESC




END