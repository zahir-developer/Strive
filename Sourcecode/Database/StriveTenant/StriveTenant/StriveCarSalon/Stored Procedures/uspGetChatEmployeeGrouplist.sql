CREATE PROCEDURE [StriveCarSalon].[uspGetChatEmployeeGrouplist] 
@EmployeeId INT = NULL
AS
-- =============================================
-- Author:		<Author, Zahir>
-- Create date: 21-10-2010
-- Description:	Returns the list of Group details using employeeId
-- =============================================
BEGIN

SELECT DISTINCT grp.ChatGroupId,GroupName, grp.GroupId
from tblChatGroup grp
JOIN tblChatUserGroup uGrp on uGrp.chatgroupId = grp.chatGroupId
where ugrp.UserId = @EmployeeId OR @EmployeeId is NULL

END