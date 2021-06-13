CREATE PROCEDURE [StriveCarSalon].[uspGetSchedule] 
@LocationId int,
@EmployeeId int = NULL,
@ScheduledStartDate Date = NULL,
@ScheduledEndDate Date = NULL
AS
BEGIN  

--[StriveCarSalon].[uspGetSchedule] 0, '2020-08-12', '2020-08-12'
--[StriveCarSalon].[uspGetSchedule] 1,null, '2021-06-11', '2021-06-13'
/*
'2021-06-11' - Vetriselvi - Fixed calculation exception for total hours
*/

DROP TABLE IF EXISTS #Schedule

IF @EmployeeId is null
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
	 tblsc.IsDeleted INTO #Schedule
FROM tblSchedule as tblsc 
INNER JOIN [tblEmployee] tblemp ON (tblsc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [tblLocation] tblloc ON (tblsc.LocationId = tblloc.LocationId)
LEFT JOIN [GetTable]('EmployeeRole') tbler ON  (tblsc.RoleId = tbler.valueid)
WHERE 
(ISNULL(tblloc.IsActive,1) = 1 AND tblloc.IsDeleted = 0) AND
(ISNULL(tblsc.IsDeleted,0)=0 AND ISNULL(tblsc.IsActive,1) = 1) AND 
((tblsc.LocationId =@LocationId) OR (@LocationId = 0))
AND
((ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate) OR (@ScheduledStartDate IS NULL AND @ScheduledendDate IS NULL))


Select * from #Schedule


select convert(varchar,(SUM(DATEDIFF(MINUTE,StartTime, EndTime)))/60 )+'.'+ convert(varchar,(SUM(DATEDIFF(MINUTE,StartTime, EndTime)))%60) as Totalhours 
from #Schedule

select count(distinct EmployeeId) as TotalEmployees from #Schedule 
END

END
