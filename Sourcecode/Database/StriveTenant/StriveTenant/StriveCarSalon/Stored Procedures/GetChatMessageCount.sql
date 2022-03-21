CREATE PROCEDURE [StriveCarSalon].[GetChatMessageCount] 
@Employeeid int
as
begin

Select emp.FirstName+' '+emp.LastName as SenderName,
       count(IsNull(chatRecp.IsRead,0)) AS TotalMesUnread,
	   emp.Employeeid
	   INTO
	   #msg
	   From tblChatMessageRecipient chatRecp 
	   INNER JOIN tblChatMessage chatMsg on chatMsg.chatMessageId = chatRecp.chatMessageId
       INNER JOIN tblEmployee emp on emp.EmployeeId = chatRecp.Senderid
	   Where  chatRecp.RecipientId=@Employeeid and isnull(chatRecp.IsRead,0)=0 
	   group by emp.FirstName,emp.LastName,emp.Employeeid

Select * from #msg

Select SUM(CAST(TotalMesUnread AS int)) as AllTotalmesUnread from #msg

End