CREATE PROCEDURE [StriveCarSalon].[uspGetSchedule]
@LocationId int,
@EmployeeId int = NULL,
@ScheduledStartDate Date = NULL,
@ScheduledEndDate Date = NULL
AS
BEGIN  

SET NOCOUNT ON;

--[StriveCarSalon].[uspGetSchedule] 0, 0, '2021-12-01', '2021-12-31'
--[StriveCarSalon].[uspGetSchedule] 1,null, '2021-06-11', '2021-06-13'
/*
2021-06-11 - Vetriselvi - Fixed calculation exception for total hours
2021-07-23 - Zahir - Added employeeId optional filter
*/

DROP TABLE IF EXISTS #Schedule


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
	 tbler.RoleName as EmployeeRole,
	 tblsc.IsDeleted INTO #Schedule
FROM tblSchedule as tblsc 
INNER JOIN [tblEmployee] tblemp ON (tblsc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [tblLocation] tblloc ON (tblsc.LocationId = tblloc.LocationId)
LEFT JOIN tblRoleMaster tbler ON  (tblsc.RoleId = tbler.RoleMasterId)
WHERE 
(ISNULL(tblloc.IsActive,1) = 1 AND tblloc.IsDeleted = 0) AND
(ISNULL(tblsc.IsDeleted,0)=0 AND ISNULL(tblsc.IsActive,1) = 1) AND 
((tblsc.LocationId = @locationId) OR (@LocationId = 0)) AND
((tblsc.EmployeeId = @EmployeeId ) OR (@EmployeeId  is NULL)) AND
((ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledEndDate) OR (@ScheduledStartDate IS NULL AND @ScheduledEndDate IS NULL))

Select * from #Schedule

--Select *from StriveCarSalon.tblSchedule

select convert(varchar,(SUM(DATEDIFF(MINUTE,StartTime, EndTime)))/60 )+'.'+ convert(varchar,(SUM(DATEDIFF(MINUTE,StartTime, EndTime)))%60) as Totalhours 
from #Schedule

select count(distinct EmployeeId) as TotalEmployees from #Schedule 


END
