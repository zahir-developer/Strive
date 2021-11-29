
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 10-Nov-2020
-- Description:	Returns the list of Recent Chat employee and Chat group informations
-- Example: [StriveCarSalon].[uspGetChatEmployeeAndGroupHistory] 1496
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetChatEmployeeAndGroupHistory] 
	@EmployeeId INT
AS
BEGIN
--DECLARE @EmployeeId INT = 119

DROP TABLE IF EXISTS #CHATHISTORY 
DROP TABLE IF EXISTS #GRPHISTORY
DROP TABLE IF EXISTS #RECENTCHAT

CREATE TABLE #RecentChat
(
Id INT,
FirstName VARCHAR(25),
LastName VARCHAR(25),
CommunicationId VARCHAR(45),
ChatMessageId BIGINT,
RecentChatMessage VARCHAR(1000),
IsRead BIT,
CreatedDate dateTime,
IsGroup BIT,
Selected BIT
)

;WITH CHTMSG
AS
(
SELECT RecipientId,SenderId,Max(ChatMessageId) AS ChatMessageId
FROM 
StriveCarSalon.tblChatMessageRecipient chtrec
Join StriveCarSalon.tblEmployee Emp ON 
(chtrec.SenderId=emp.EmployeeId and ChtRec.RecipientId=@EmployeeId) OR (ChtRec.RecipientId=emp.EmployeeId and ChtRec.SenderId=@EmployeeId)
WHERE ChtRec.SenderID = @EmployeeId OR ChtRec.RecipientId<>@EmployeeId
GROUP BY RecipientId,SenderId
),
MaxCht
AS
(
SELECT 
	SenderId AS ID,Max(ChatMessageId) ChatMessageId 
FROM CHTMSG 
WHERE SenderId<>@EmployeeId
GROUP BY SenderId
UNION
SELECT 
	RecipientId AS ID,Max(ChatMessageId) ChatMessageId 
FROM CHTMSG 
WHERE RecipientId<> @EmployeeId
GROUP BY RecipientId
)


SELECT Distinct
emp.EmployeeId as Id,
emp.FirstName,
emp.LastName,
ISNULL(chatComm.ChatCommunicationId, 0) as ChatCommunicationId,
ISNULL(chatComm.CommunicationId, 0) as CommunicationId,
chatRecp.IsRead,
Msg.CreatedDate,
Msg.ChatMessageId,
msg.Messagebody INTO #CHATHISTORY 
FROM 
StriveCarSalon.tblEmployee emp 
LEFT JOIN StriveCarSalon.tblChatCommunication chatComm on emp.EmployeeId = chatComm.EmployeeId
LEFT JOIN (SELECT ID,MAX(ChatMessageID)ChatMessageID FROM MaxCht 
GROUP BY ID ) MR ON Mr.Id=Emp.EmployeeId 
LEFT JOIN StriveCarSalon.tblChatMessage msg ON MR.ChatMessageId=Msg.ChatMessageId
INNER JOIN StriveCarSalon.tblChatMessageRecipient chatRecp on chatRecp.RecipientId = Mr.ID AND chatrecp.ChatMessageID=Mr.ChatMessageID
AND (emp.IsDeleted=0 OR emp.IsDeleted IS NULL) 
AND (emp.IsDeleted = 0 OR emp.IsDeleted IS NULL)
WHERE emp.EmployeeId != @employeeId and chatRecp.SenderId = @EmployeeId OR @EmployeeId is NULL
ORDER BY emp.EmployeeId DESC

;WITH
CHTGRPMSG
AS
(
SELECT GRP.ChatGroupId as Id, GRP.GroupId, GRP.GroupName, SenderId, Max(MsgRec.ChatMessageId) AS ChatMessageId, grpRec.IsRead
FROM StriveCarSalon.tblChatGroup GRP
JOIN StriveCarSalon.tblChatUserGroup UsrGRP on GRP.ChatGroupId = GRP.ChatGroupId
JOIN StriveCarSalon.tblChatMessageRecipient MsgRec on MsgRec.RecipientGroupId = GRP.ChatGroupId
INNER JOIN StriveCarSalon.tblChatGroupRecipient grpRec on grpRec.ChatGroupId = grp.ChatGroupId and grpRec.RecipientId = @EmployeeId
WHERE UsrGRP.UserId = @EmployeeId
GROUP BY GRP.ChatGroupId, MsgRec.RecipientId, SenderId, GroupId, GroupName, grpRec.IsRead
),
MaxGROUPCHAT
AS
(
SELECT Id, Max(ChatMessageId) AS ChatMessageId
FROM CHTGRPMSG
GROUP BY Id
)

Select maxGrp.Id, GRP.GroupId, GRP.GroupName as FirstName, SenderId, GRP.ChatMessageId, cMsg.Messagebody, IsRead, cMsg.CreatedDate INTO #GRPHISTORY from CHTGRPMSG GRP
JOIN StriveCarSalon.tblChatMessage cMsg on cMsg.ChatMessageId = GRP.ChatMessageId
JOIN MaxGROUPCHAT maxGrp on maxGrp.ChatMessageId = cMsg.ChatMessageId 

INSERT INTO #RECENTCHAT Select Id,
FirstName,
LastName,
CommunicationId,
ChatMessageId,
Messagebody,
IsRead,
CreatedDate,
0 as IsGroup,
0 as IsSelected from #CHATHISTORY


INSERT INTO #RECENTCHAT Select Id, FirstName, '', GroupId, ChatMessageId, Messagebody, ISNULL(IsRead,1), CreatedDate, 1, 0 from #GRPHISTORY

Select * from #RECENTCHAT order by CreatedDate desc



END