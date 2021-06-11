CREATE PROCEDURE [StriveCarSalon].[uspGetHourlyWashReport] 
@locationId INT, @fromDate Date, @endDate Date
AS
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 01-Dec-2020
-- Description:	Returns the Hourly wash report data and Sales data. EXEC [StriveCarSalon].USPGETHOURLYWASHREPORT 2034, '2021-01-03', '2020-01-17' 
--[StriveCarSalon].[uspGetHourlyWashReport] 1,'2021-04-09','2021-06-22'
-- =============================================
----------History-------------
-- =============================================
-- 18-05-2021, Shalini - changed the actualtimeout conversion and commented the previous one
-- =============================================

BEGIN

DROP TABLE IF EXISTS #WashHours
DROP TABLE IF EXISTS #WashHourResult

DECLARE @JobCompleted INT = (Select top 1 valueid from GetTable('JobStatus') where valuedesc='Completed')
DECLARE @WashJobType INT = (Select top 1 valueid from GetTable('JobType') where valuedesc='Wash')


/* Completed Hours */
SELECT 
	JobDate,datepart(hour,cast(ActualTimeOut as time)) as [Hour]
	--CONVERT(varchar(15), DATEPART(HOUR,CONVERT(datetime, SWITCHOFFSET(CONVERT(datetimeoffset, ActualTimeOut), 
 --                           DATENAME(TzOffset, SYSDATETIMEOFFSET()))) ),100)as [Hour] 
INTO 
	#WashHours
FROM 
	tblJob J
WHERE 
	LocationId = @locationId AND JobType=@WashJobType AND JobStatus = @JobCompleted 
		AND JobDate BETWEEN @fromDate AND @endDate

	
Select 
	JobDate, 
	CASE [Hour] WHEN 6 THEN 1 END as '_6AM',
	CASE [Hour] WHEN 7 THEN 1 END as '_7AM',
	CASE [Hour] WHEN 8 THEN 1 END as '_8AM',
	CASE [Hour] WHEN 9 THEN 1 END as '_9AM',
	CASE [Hour] WHEN 10 THEN 1 END as '_10AM',
	CASE [Hour] WHEN 11 THEN 1 END as '_11AM',
	CASE [Hour] WHEN 12 THEN 1 END as '_12PM',
	CASE [Hour] WHEN 13 THEN 1 END as '_1PM',
	CASE [Hour] WHEN 14 THEN 1 END as '_2PM',
	CASE [Hour] WHEN 15 THEN 1 END as '_3PM',
	CASE [Hour] WHEN 16 THEN 1 END as '_4PM',
	CASE [Hour] WHEN 17 THEN 1 END as '_5PM',
	CASE [Hour] WHEN 18 THEN 1 END as '_6PM',
	CASE [Hour] WHEN 19 THEN 1 END as '_7PM',
	CASE [Hour] WHEN 20 THEN 1 END as '_8PM',
	CASE [Hour] WHEN 21 THEN 1 END as '_9PM'
INTO 
	#WashHourResult
FROM 
	#WashHours 

DROP TABLE IF EXISTS #TotalWash
	
/* Total Hours */
SELECT 
	JobDate,datepart(hour,cast(ActualTimeOut as time)) as [Hour]
INTO 
	#TotalWash
FROM 
	tblJob J
WHERE 
	LocationId = @locationId AND JobType=@WashJobType 
		AND JobDate BETWEEN @fromDate AND @endDate
		

	
Select 
	JobDate, 
	CASE [Hour] WHEN 6 THEN 1 END as '_6AM',
	CASE [Hour] WHEN 7 THEN 1 END as '_7AM',
	CASE [Hour] WHEN 8 THEN 1 END as '_8AM',
	CASE [Hour] WHEN 9 THEN 1 END as '_9AM',
	CASE [Hour] WHEN 10 THEN 1 END as '_10AM',
	CASE [Hour] WHEN 11 THEN 1 END as '_11AM',
	CASE [Hour] WHEN 12 THEN 1 END as '_12PM',
	CASE [Hour] WHEN 13 THEN 1 END as '_1PM',
	CASE [Hour] WHEN 14 THEN 1 END as '_2PM',
	CASE [Hour] WHEN 15 THEN 1 END as '_3PM',
	CASE [Hour] WHEN 16 THEN 1 END as '_4PM',
	CASE [Hour] WHEN 17 THEN 1 END as '_5PM',
	CASE [Hour] WHEN 18 THEN 1 END as '_6PM',
	CASE [Hour] WHEN 19 THEN 1 END as '_7PM',
	CASE [Hour] WHEN 20 THEN 1 END as '_8PM',
	CASE [Hour] WHEN 21 THEN 1 END as '_9PM'
INTO 
	#FinalWashCount
FROM 
	#TotalWash
	
DROP TABLE IF EXISTS #Hours_Data

select 
	EventDate,
	Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)), 0), 114),':','.')  as LoginTime, 
	rm.RoleName INTO #Hours_Data
from tblTimeClock TC 
inner join tblEmployee E on TC.EmployeeId = E.EmployeeId
inner join tblRoleMaster rm on rm.RoleMasterId = tc.RoleId
where LocationId = @LocationId and EventDate BETWEEN @fromDate AND @endDate and tc.InTime is Not NULL
AND ISNULL(TC.IsDeleted,0) = 0 AND tc.IsActive = 1
--select * from #Hours_Data

;WITH FinalResult AS (	
SELECT EventDate,
CASE WHEN RoleName='Washer' THEN ISNULL(cast(LoginTime as DECIMAL(18,2)),0) ELSE 0 END AS TotalWashHours
FROM #Hours_Data
)

Select EventDate,Sum(ISNULL(TotalWashHours,0))   TotalHours  
INTO #TotalWorkHours 
from FinalResult
GROUP By EventDate

	
DROP TABLE IF EXISTS #WashCount

Select count(1) as WashCount,LocationId,j.JobDate 
into #WashCount from tblJob j 
INNER JOIN GetTable('JobType') JT on JT.valueid = j.JobType
INNER join GetTable('JobStatus') GT on GT.valueId = j.JobStatus and GT.valuedesc = 'Completed'
WHERE j.JobDate BETWEEN @fromDate AND @endDate
GROUP BY LocationId,j.JobDate


SELECT DISTINCT 
WP.Weather,
WP.RainProbability,
WP.TargetBusiness,
WP.LocationId ,
CONVERT(VARCHAR(10), wp.CreatedDate, 120) AS CreatedDate,
w.WashCount
INTO #FinalWeather
FROM [tblWeatherPrediction] WP
LEFT join #WashCount w on WP.LocationId=w.LocationId AND CONVERT(VARCHAR(10), wp.CreatedDate, 120) = w.JobDate
WHERE
WP.LocationId =@LocationId AND CONVERT(VARCHAR(10), wp.CreatedDate, 120) BETWEEN @fromDate AND @endDate

SELECT FWC.JobDate,
	--CASE ISNULL(WP.CreatedDate,'') WHEN '' THEN FWC.JobDate ELSE WP.CreatedDate END AS JobDate,
	ISNULL(SUM(c._6AM), 0) as '_6AM',
	ISNULL(SUM(c._7AM), 0) as '_7AM',
	ISNULL(SUM(c._8AM), 0) as '_8AM',
	ISNULL(SUM(c._9AM), 0) as '_9AM',
	ISNULL(SUM(c._10AM), 0) as '_10AM',
	ISNULL(SUM(c._11AM), 0) as '_11AM',
	ISNULL(SUM(c._12PM), 0) as '_12AM',
	ISNULL(SUM(c._1PM), 0) as '_1PM',
	ISNULL(SUM(c._2PM), 0) as '_2PM',
	ISNULL(SUM(c._3PM), 0) as '_3PM',
	ISNULL(SUM(c._4PM), 0) as '_4PM',
	ISNULL(SUM(c._5PM), 0) as '_5PM',
	ISNULL(SUM(c._6PM), 0) as '_6PM',
	ISNULL(SUM(c._7PM), 0) as '_7PM',
	ISNULL(SUM(c._8PM), 0) as '_8PM',
	ISNULL(SUM(c._9PM), 0) as '_9PM',
	WP.Weather AS Temperature,
	WP.RainProbability AS Rain,
	WP.TargetBusiness AS Goal,
	ISNULL(WP.WashCount,0) NoOfWashes,
	ISNULL(twh.TotalHours,0) AS TotalWashHours,
	ISNULL(SUM(FWC._6AM), 0) as '_6AMTotal',
	ISNULL(SUM(FWC._7AM), 0) as '_7AMTotal',
	ISNULL(SUM(FWC._8AM), 0) as '_8AMTotal',
	ISNULL(SUM(FWC._9AM), 0) as '_9AMTotal',
	ISNULL(SUM(FWC._10AM), 0) as '_10AMTotal',
	ISNULL(SUM(FWC._11AM), 0) as '_11AMTotal',
	ISNULL(SUM(FWC._12PM), 0) as '_12AMTotal',
	ISNULL(SUM(FWC._1PM), 0) as '_1PMTotal',
	ISNULL(SUM(FWC._2PM), 0) as '_2PMTotal',
	ISNULL(SUM(FWC._3PM), 0) as '_3PMTotal',
	ISNULL(SUM(FWC._4PM), 0) as '_4PMTotal',
	ISNULL(SUM(FWC._5PM), 0) as '_5PMTotal',
	ISNULL(SUM(FWC._6PM), 0) as '_6PMTotal',
	ISNULL(SUM(FWC._7PM), 0) as '_7PMTotal',
	ISNULL(SUM(FWC._8PM), 0) as '_8PMTotal',
	ISNULL(SUM(FWC._9PM), 0) as '_9PMTotal'
FROM #FinalWashCount FWC
JOIN	#WashHourResult c ON FWC.JobDate = c.JobDate
LEFT JOIN #FinalWeather WP on FWC.JobDate = WP.CreatedDate
LEFT JOIN #TotalWorkHours twh on twh.EventDate = FWC.JobDate
--RIGHT JOIN #FinalWashCount FWC ON FWC.JobDate = WP.CreatedDate

GROUP BY 
	WP.Weather,
	WP.RainProbability,
	WP.TargetBusiness,
	WP.LocationId ,
	WP.CreatedDate,
	WP.WashCount,
	twh.TotalHours ,
	FWC.JobDate
ORDER BY CONVERT(date,FWC.JobDate) ASC

END