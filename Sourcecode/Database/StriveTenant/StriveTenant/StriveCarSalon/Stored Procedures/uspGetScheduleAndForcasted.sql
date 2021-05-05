--[StriveCarSalon].[uspGetScheduleAndForcasted]1,null,'2021-05-01','2021-05-01','2021-04-01','2021-01-01'
CREATE PROC [StriveCarSalon].[uspGetScheduleAndForcasted] 
@LocationId int,
@EmployeeId int = NULL,
@ScheduledStartDate Date = NULL,--lastweek
@ScheduledEndDate Date = NULL,--today
@lastMonth VARCHAR(10),
@lastThirdMonth  VARCHAR(10)
AS
BEGIN  
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
left JOIN [tblEmployee] tblemp ON (tblsc.EmployeeId = tblemp.EmployeeId)
INNER JOIN [tblLocation] tblloc ON (tblsc.LocationId = tblloc.LocationId)
LEFT JOIN [GetTable]('EmployeeRole') tbler ON  (tblsc.RoleId = tbler.valueid)
WHERE 
(ISNULL(tblloc.IsActive,1) = 1 AND tblloc.IsDeleted = 0) AND
(ISNULL(tblsc.IsDeleted,0)=0 AND ISNULL(tblsc.IsActive,1) = 1) AND 
((tblsc.LocationId =@LocationId OR @LocationId is null)
AND
(ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate) OR (@ScheduledStartDate IS NULL AND @ScheduledendDate IS NULL))



select SUM(CONVERT(DECIMAL(4,2),(DATEDIFF(MINUTE,StartTime, EndTime))))/60 as Totalhours ,ScheduledDate
from #Schedule s
Group by s.ScheduledDate

select count(distinct EmployeeId) as TotalEmployees ,ScheduledDate
from #Schedule s
Group by s.ScheduledDate


DROP TABLE IF EXISTS #WashHours

Select count(1) as WashCount,j.JobDate, j.locationId into #WashHours from tblJob j 
INNER JOIN GetTable('JobType') JT on JT.valueid = j.JobType
--INNER join GetTable('JobStatus') GT on GT.valueId = j.JobStatus and GT.valuedesc = 'Completed'
WHERE j.JobDate in (@ScheduledendDate,@ScheduledStartDate,@lastMonth,@lastThirdMonth)  and j.Locationid=@LocationId
GROUP BY JobDate, j.LocationId

DROP TABLE IF EXISTS #WashTime
SELECT 
WP.Weather,
WP.RainProbability,
convert (decimal(18,2),(ISNULL(a.WashCount,0)*1.5)) AS WashTimeMinutes,
a.JobDate ,
WP.CreatedDate into #WashTime
FROM [tblWeatherPrediction] WP
LEFT join #WashHours a on CONVERT(VARCHAR(10), wp.CreatedDate, 120) = a.JobDate
WHERE
WP.LocationId =@LocationId AND CONVERT(VARCHAR(10), wp.CreatedDate, 120) in (@ScheduledendDate,@ScheduledStartDate,@lastMonth,@lastThirdMonth) 
and wp.Weather IS NOT NULL AND WP.RainProbability IS nOT NULL 
ORDER BY CreatedDate DESC


DECLARE @AvgCount INT = (Select count(1) from #WashTime WHERE WashTimeMinutes >0 )

DECLARE @Normal DECIMAL(18,2) = (Select SUM(CONVERT(DECIMAL(18,2),WashTimeMinutes))/@AvgCount from #WashTime)

DECLARE @Today_RainPrecipitation int = (select top 1 RainProbability from #WashTime where (CONVERT(VARCHAR(10), CreatedDate, 120) = @ScheduledendDate))

DEclare @Formula decimal(18,2) =(select top(1) Formula from tblForcastedRainPercentageMaster fr 
where @Today_RainPrecipitation between fr.PrecipitationRangeFrom and fr.PrecipitationRangeTo)

select  Round(@Normal * @Formula,0) as ForcastedEmployeeHours
,Round((@Normal * @Formula) / 1.25,0) as ForcastedCars ,@Today_RainPrecipitation as RainPrecipitation

END

END