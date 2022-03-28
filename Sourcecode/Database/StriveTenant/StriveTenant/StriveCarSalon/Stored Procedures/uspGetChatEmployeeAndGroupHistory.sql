-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 10-Nov-2020
-- Description:	Returns the list of Recent Chat employee and Chat group informations
-- Example: [StriveCarSalon].[uspGetChatEmployeeAndGroupHistory] 1138
-- =============================================
-- ============-======History===================
-- =============================================
-- 2021-09-06 - Recent chat history issue fixes
CREATE PROCEDURE [StriveCarSalon].[uspGetChatEmployeeAndGroupHistory] 
@EmployeeId INT
AS
BEGIN
--DECLARE @EmployeeId INT = 5

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

DROP TABLE IF EXISTS #AllChatMsg 
DROP TABLE IF EXISTS #CHTMSG 
DROP TABLE IF EXISTS #ReadMsg 

SELECT emp.EmployeeId, ChatMessageId AS ChatMessageId, chtrec.ChatRecipientId, chtrec.SenderId, chtrec.RecipientId, chtrec.IsRead into #AllChatMsg
FROM 
tblChatMessageRecipient chtrec
Join tblEmployee Emp ON 
(chtrec.SenderId = emp.EmployeeId AND ChtRec.RecipientId = @EmployeeId) OR (ChtRec.RecipientId=emp.EmployeeId and ChtRec.SenderId=@EmployeeId)
WHERE (ChtRec.SenderID = @EmployeeId OR ChtRec.RecipientId = @EmployeeId)

SELECT EmployeeId, MAX(ChatMessageId) AS ChatMessageId into #CHTMSG
FROM #AllChatMsg
GROUP By EmployeeId

SELECT allc.IsRead, allc.ChatRecipientId, allc.ChatMessageId into #ReadMsg
FROM #CHTMSG cm
INNER JOIN #AllChatMsg allc on allc.ChatMessageId = cm.ChatMessageId
where RecipientId = @EmployeeId

--Select * from #ReadMsg

--,
--MaxCht
--AS
--(
--SELECT 
--	EmployeeId AS ID,Max(ChatMessageId) ChatMessageId 
--FROM CHTMSG 
--WHERE EmployeeId<>@EmployeeId
--GROUP BY EmployeeId
--UNION
--SELECT 
--	EmployeeId AS ID,Max(ChatMessageId) ChatMessageId 
--FROM CHTMSG 
--WHERE EmployeeId<> @EmployeeId
--GROUP BY EmployeeId
--)

SELECT Distinct
emp.EmployeeId as Id,
emp.FirstName,
emp.LastName,
ISNULL(chatComm.ChatCommunicationId, 0) as ChatCommunicationId,
ISNULL(chatComm.CommunicationId, 0) as CommunicationId,
Rm.IsRead as IsRead,
Msg.CreatedDate,
Msg.ChatMessageId,
msg.Messagebody INTO #CHATHISTORY 
FROM 
tblEmployee emp 
LEFT JOIN tblChatCommunication chatComm on emp.EmployeeId = chatComm.EmployeeId
LEFT JOIN #CHTMSG cMsg on cMsg.EmployeeId = Emp.EmployeeId 
LEFT JOIN tblChatMessage msg ON cMsg.ChatMessageId=Msg.ChatMessageId
INNER JOIN tblChatMessageRecipient chatRecp on chatRecp.RecipientId = cMsg.EmployeeId OR chatrecp.ChatMessageID = cMsg.ChatMessageID
LEFT JOIN #ReadMsg Rm on Rm.ChatMessageId = msg.ChatMessageId 
AND (emp.IsDeleted=0 OR emp.IsDeleted IS NULL) 
AND (emp.IsDeleted = 0 OR emp.IsDeleted IS NULL)
WHERE chatRecp.SenderId = @EmployeeId OR chatRecp.RecipientId = @EmployeeId
ORDER BY emp.EmployeeId DESC

;WITH
CHTGRPMSG
AS
(
SELECT GRP.ChatGroupId as Id, 
GRP.GroupId, 
GRP.GroupName, 
SenderId,
Max(MsgRec.ChatMessageId) AS ChatMessageId, 
grpRec.IsRead
FROM tblChatGroup GRP
JOIN tblChatUserGroup UsrGRP on GRP.ChatGroupId = UsrGRP.ChatGroupId
LEFT JOIN tblChatMessageRecipient MsgRec on MsgRec.RecipientGroupId = GRP.ChatGroupId
LEFT JOIN tblChatGroupRecipient grpRec on grpRec.ChatGroupId = grp.ChatGroupId and grpRec.RecipientId = @EmployeeId
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


Select GRP.Id, GRP.GroupId, GRP.GroupName as FirstName, SenderId, GRP.ChatMessageId, cMsg.Messagebody, IsRead, cMsg.CreatedDate 
INTO #GRPHISTORY 
from CHTGRPMSG GRP
INNER JOIN tblChatMessage cMsg on cMsg.ChatMessageId = GRP.ChatMessageId and cMsg.ChatMessageId is NOT NULL
INNER JOIN MaxGROUPCHAT maxGrp on maxGrp.ChatMessageId = cMsg.ChatMessageId and cMsg.ChatMessageId is NOT NULL


INSERT INTO #RECENTCHAT Select Id,
FirstName,
LastName,
CommunicationId,
ChatMessageId,
Messagebody,
IsRead,
CreatedDate,
0 as IsGroup,
0 as IsSelected from #CHATHISTORY where id != @EmployeeId

INSERT INTO #RECENTCHAT Select Id, FirstName, '', GroupId, ChatMessageId, Messagebody, ISNULL(IsRead,1), CreatedDate, 1, 0 from #GRPHISTORY

INSERT INTO #RECENTCHAT
Select cg.chatGroupId as Id, GroupName as FirstName, '', cg.GroupId, '', NULL, NULL, cg.CreatedDate, 1, 0 from tblChatGroup cg
JOIN tblChatUserGroup ug on cg.ChatGroupId = ug.ChatGroupId
WHERE ug.UserId = @EmployeeId and cg.ChatGroupId not in (Select id from #GRPHISTORY gh) and cg.IsDeleted = 0 and ug.IsDeleted = 0


Select * from #RECENTCHAT order by CreatedDate desc



END