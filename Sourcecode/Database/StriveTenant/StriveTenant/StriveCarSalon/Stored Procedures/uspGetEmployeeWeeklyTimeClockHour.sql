CREATE PROCEDURE [StriveCarSalon].[uspGetEmployeeWeeklyTimeClockHour] 
@LocationId int,
@EmployeeId int = NULL,
@StartDate Date = NULL,
@EndDate Date = NULL
AS
-- =============================================
-- Author:		Zahir Hussain
-- Create date: 09-10-2020
-- Description:	Retrieves Hours logged In for the given period, 
-- [StriveCarSalon].[uspGetEmployeeWeeklyTimeClockHour] 1 , 144, '2020-11-01' , '2020-12-30'

-- =============================================
BEGIN  

DECLARE @WorkhourThreshold DECIMAL(4,2);

Select top 1 @WorkhourThreshold = WorkhourThreshold from tblLocation where LocationId=@LocationId

SELECT
	 @WorkhourThreshold as LocationWorkHourThreshold,tblloc.LocationName,tblloc.LocationId,
	 --tc.EmployeeId,
	 --tc.InTime,
	 --tc.OutTime
	 SUM(DATEDIFF(hour,InTime,ISNULL(OutTime, InTime))) as EmployeeWorkMinutes
FROM StriveCarSalon.tblTimeClock as tc
INNER JOIN [StriveCarSalon].[tblEmployee] tblemp ON (tc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [StriveCarSalon].[tblLocation] tblloc ON (tc.LocationId = tblloc.LocationId)
WHERE 
--(ISNULL(tblloc.IsActive,1) = 1 AND tblloc.IsDeleted = 0) AND
(ISNULL(tc.IsDeleted,0)=0 AND ISNULL(tc.IsActive,1) = 1) AND 
(tc.LocationId = @LocationId) AND
((tc.EventDate BETWEEN @StartDate AND @EndDate) OR (@StartDate IS NULL AND @EndDate IS NULL)) AND
(tc.EmployeeId = @EmployeeId OR @EmployeeId IS NULL)
AND tc.InTime IS NOT NULL AND tc.OutTime IS NOT NULL
GROUP BY tblloc.LocationName,tblloc.LocationId


END