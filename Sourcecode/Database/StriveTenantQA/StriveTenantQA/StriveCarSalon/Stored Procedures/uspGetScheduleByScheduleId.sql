CREATE PROC [StriveCarSalon].[uspGetScheduleByScheduleId]
@ScheduleId int
AS
BEGIN  

SELECT
	 tblsc.ScheduleId,
	 tblsc.EmployeeId,
	 ISNULL(tblsc.IsAbscent, 0) as IsEmployeeAbscent,
	 tblemp.FirstName +''+tblemp.LastName as EmployeeName,
	 tblsc.LocationId,
	 tblloc.LocationName,
	 tblLoc.ColorCode,
	 tblsc.RoleId,
	 tblsc.ScheduledDate,
	 tblsc.StartTime,
	 tblsc.EndTime,
	 tblsc.ScheduleType,
	 tblsc.Comments,
	 tbler.valuedesc as EmployeeRole,
	 tblsc.IsDeleted
FROM StriveCarSalon.tblSchedule as tblsc 
INNER JOIN [StriveCarSalon].[tblEmployee] tblemp ON (tblsc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [StriveCarSalon].[tblLocation] tblloc ON (tblsc.LocationId = tblloc.LocationId)
LEFT JOIN [StriveCarSalon].[GetTable]('EmployeeRole') tbler ON  (tblsc.RoleId = tbler.valueid)
WHERE ScheduleId=@ScheduleId
AND
ISNULL(tblsc.IsDeleted,0)=0 
AND tblsc.IsActive = 1


END

