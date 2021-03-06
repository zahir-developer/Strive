CREATE PROC [StriveCarSalon].[uspGetSchedule] 
@LocationId int,
@ScheduledStartDate Date = NULL,
@ScheduledEndDate Date = NULL
AS
BEGIN  

--[StriveCarSalon].[uspGetSchedule] 0, '2020-08-12', '2020-08-12'
--[StriveCarSalon].[uspGetSchedule] 1, '2020-08-01', '2020-08-30'

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
WHERE 
(ISNULL(tblsc.IsDeleted,0)=0 AND tblsc.IsDeleted = 0) AND ISNULL(tblsc.IsActive,1) = 1
AND
((tblsc.LocationId =@LocationId) OR (@LocationId = 0))
AND
((ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate) OR (@ScheduledStartDate IS NULL AND @ScheduledendDate IS NULL))

select sum((DATEDIFF(hh,tblsc.StartTime, tblsc.EndTime))) as Totalhours 
from StriveCarSalon.tblSchedule as tblsc
INNER JOIN [StriveCarSalon].[tblEmployee] tblemp ON (tblsc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [StriveCarSalon].[tblLocation] tblloc ON (tblsc.LocationId = tblloc.LocationId)
WHERE 
(ISNULL(tblsc.IsDeleted,0)=0 AND tblsc.IsDeleted = 0) AND ISNULL(tblsc.IsActive,1) = 1
AND
((tblsc.LocationId =@LocationId) OR (@LocationId = 0))
AND
((ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate) OR (@ScheduledStartDate IS NULL AND @ScheduledendDate IS NULL))


select count(distinct tblemp.EmployeeId) as TotalEmployees from StriveCarSalon.tblSchedule as tblsc
INNER JOIN [StriveCarSalon].[tblEmployee] tblemp ON (tblsc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [StriveCarSalon].[tblLocation] tblloc ON (tblsc.LocationId = tblloc.LocationId)
WHERE 
(ISNULL(tblsc.IsDeleted,0)=0 AND tblsc.IsDeleted = 0) AND ISNULL(tblsc.IsActive,1) = 1
AND
((tblsc.LocationId =@LocationId) OR (@LocationId = 0))
AND
((ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate) OR (@ScheduledStartDate IS NULL AND @ScheduledendDate IS NULL))

END