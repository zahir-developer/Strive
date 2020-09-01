
CREATE PROCEDURE [CON].[uspGetChatMessage]
as
begin
SELECT subject, sent_to, 
       createddate
	   -- ,(summ / countt) * 100 AS Read_Per
FROM (
SELECT msg.subject, grp.groupname as sent_to,  msg.createddate 
     -- ,SUM (isread) AS summ, COUNT (isread) AS countt
      FROM strivecarsalon.tblchatmessagerecipient msgrec,  strivecarsalon.tblchatMESSAGE msg,  
           strivecarsalon.tblchatUserGroup ug,  strivecarsalon.tblchatgroup grp
      WHERE  msgrec.chatmessageid = msg.chatmessageid
      AND msgrec.recipientgroupid = ug.chatgroupuserid
      AND ug.GROUPID = grp.chatgroupid
      AND msgrec.recipientgroupid IS NOT NULL
      GROUP BY msg.subject, grp.groupname, msg.createddate
      UNION
      SELECT msg.subject, u.FirstName as sent_to,
      msg.createddate
	  --, SUM (isread) AS summ, 
	  --COUNT (isread) AS countt
      FROM strivecarsalon.tblchatmessagerecipient msgrec, strivecarsalon.tblchatMESSAGE msg,  
	  strivecarsalon.tblEmployee u
      WHERE msgrec.chatmessageid = msg.chatmessageid
      AND msgrec.chatrecipientid = u.EmployeeId
      AND msgrec.recipientgroupid IS NULL
      GROUP BY msg.subject, FirstName, msg.createddate) as table1
	  end