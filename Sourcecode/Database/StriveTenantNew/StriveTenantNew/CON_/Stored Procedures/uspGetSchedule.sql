
CREATE PROCEDURE [CON].[uspGetSchedule] 
@LocationId int,
@ScheduledStartDate Date = NULL,
@ScheduledEndDate Date = NULL
AS
BEGIN  

--[CON].[uspGetSchedule] 0, '2020-08-12', '2020-08-12'
--[CON].[uspGetSchedule] 1, '2020-08-01', '2020-08-30'

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
INNER JOIN [CON].[tblEmployee] tblemp ON (tblsc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [CON].[tblLocation] tblloc ON (tblsc.LocationId = tblloc.LocationId)
LEFT JOIN [CON].[GetTable]('EmployeeRole') tbler ON  (tblsc.RoleId = tbler.valueid)
WHERE 
(ISNULL(tblsc.IsDeleted,0)=0 AND tblsc.IsDeleted = 0) AND  tblsc.IsActive = 1
AND
((tblsc.LocationId =@LocationId) OR (@LocationId = 0))
AND
(ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate) OR (@ScheduledStartDate IS NULL AND @ScheduledendDate IS NULL)

/*
select sum((DATEDIFF(hh,StartTime, EndTime))) as Totalhours 
       from StriveCarSalon.tblSchedule tblsc
		 where (ISNULL(tblsc.IsDeleted,0)=0 AND tblsc.IsDeleted = 0) AND  tblsc.IsActive = 1 AND
		  (ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate)

select count(distinct EmployeeId) as TotalEmployees from StriveCarSalon.tblSchedule tblsc
where (ISNULL(tblsc.IsDeleted,0)=0 AND tblsc.IsDeleted = 0) AND  tblsc.IsActive = 1 AND
ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate and locationId=1
*/

END