CREATE PROCEDURE [StriveCarSalon].[uspGetScheduleByScheduleId]
@ScheduleId int
AS
-- ==========================================================
-- Author:              Vineeth.B
-- Created date:        2020-08-20
-- LastModified date: 
-- LastModified Author: 
-- Description:         To get Schedule details by ScheduleId
-- ===========================================================
BEGIN  

SELECT
	 tblsc.ScheduleId,
	 tblsc.EmployeeId,
	 ISNULL(tblsc.IsAbscent, 0) as IsEmployeeAbscent,
	 TRIM(tblemp.FirstName) +' '+ TRIM(tblemp.LastName) as EmployeeName,
	 tblsc.LocationId,
	 tblloc.LocationName,
	 tblLoc.ColorCode,
	 tblsc.RoleId,
	 tblsc.ScheduledDate,
	 tblsc.StartTime,
	 tblsc.EndTime,
	 tblsc.ScheduleType,
	 tblsc.Comments,
	 tbler.rolename as EmployeeRole,
	 tblsc.IsDeleted
FROM tblSchedule as tblsc 
INNER JOIN [tblEmployee] tblemp ON (tblsc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [tblLocation] tblloc ON (tblsc.LocationId = tblloc.LocationId)
LEFT JOIN tblRoleMaster tbler ON  (tblsc.RoleId = tbler.rolemasterId)
WHERE ScheduleId=@ScheduleId
AND
ISNULL(tblsc.IsDeleted,0)=0 
AND tblsc.IsActive = 1


END
