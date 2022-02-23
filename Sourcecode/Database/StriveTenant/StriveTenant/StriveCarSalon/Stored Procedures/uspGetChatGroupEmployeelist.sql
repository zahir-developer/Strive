-- =============================================
-- Author:		<Author, Zahir>
-- Create date: 21-10-2010
-- Description:	Returns the list of Employees using employeeId
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetChatGroupEmployeelist]
@GroupId INT
AS
BEGIN

SELECT emp.EmployeeId as Id, emp.FirstName, emp.LastName, grp.ChatGroupId, grp.GroupId, uGrp.ChatGroupUserId
from StriveCarSalon.tblChatGroup grp
JOIN StriveCarSalon.tblChatUserGroup uGrp on uGrp.chatgroupId = grp.chatGroupId
JOIN StriveCarSalon.tblEmployee emp on emp.EmployeeId = uGrp.UserId
where grp.ChatGroupId = @GroupId and ISNULL(uGrp.IsDeleted,0) = 0 order by emp.FirstName, emp.LastName
END