CREATE PROCEDURE [StriveCarSalon].[uspGetHourlyWashReport] 
@locationId INT, @fromDate Date, @endDate Date
AS
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 01-Dec-2020
-- Description:	Returns the Hourly wash report data and Sales data. EXEC [StriveCarSalon].USPGETHOURLYWASHREPORT 2034, '2021-01-03', '2020-01-17' 
--[StriveCarSalon].[uspGetHourlyWashReport] 1,'2021-11-21','2021-11-30'
-- =============================================
----------History-------------
-- =============================================
-- 18-05-2021, Shalini - changed the actualtimeout conversion and commented the previous one
-- 30-Jun-2021 - Vetriselvi - Fixed Time slot bug.
-- 02-11-2021, Vetriselvi - Score should include all employees except detailer and sum of actual minutes
-- 26-11-2021, Vetriselvi - Show all records between the selected date

-- =============================================

BEGIN

DROP TABLE IF EXISTS #WashHours
DROP TABLE IF EXISTS #WashHourResult
DROP TABLE IF EXISTS #TotalWorkHours
DROP TABLE IF EXISTS #FinalWeather

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
WHERE  ISNULL(IsActive,0) = 1 AND
ISNULL(IsDeleted,0) = 0 AND
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
WHERE  ISNULL(IsActive,0) = 1 AND
ISNULL(IsDeleted,0) = 0 AND
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
	Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,Intime)), 0), 114),':','.')  as LoginTime
	, DATEDIFF(MI, InTime, ISNULL(OutTime,InTime))  AS TotalMinutes,
	rm.RoleName INTO #Hours_Data
from tblTimeClock TC 
inner join tblEmployee E on TC.EmployeeId = E.EmployeeId
inner join tblRoleMaster rm on rm.RoleMasterId = tc.RoleId
where LocationId = @LocationId and EventDate BETWEEN @fromDate AND @endDate and tc.InTime is Not NULL
AND ISNULL(TC.IsDeleted,0) = 0 AND tc.IsActive = 1  AND RoleName != 'Detailer'
--select * from #Hours_Data

;WITH FinalResult AS (	
SELECT    
 EventDate,  
 SUM(TotalMinutes) AS TotalWashHours
FROM #Hours_Data 
GROUP BY EventDate
)

Select EventDate,CAST((TotalWashHours/60.00) AS DECIMAL(18,2))   TotalHours  
INTO #TotalWorkHours 
from FinalResult

	
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

DROP TABLE IF EXISTS #tolWashCount
SELECT FWC.JobDate,
	
	SUM(ISNULL(FWC._6AM, 0)) as '_6AMTotal',
	SUM(ISNULL(FWC._7AM, 0)) AS '_7AMTotal',
	SUM(ISNULL(FWC._8AM, 0)) AS '_8AMTotal',
	SUM(ISNULL(FWC._9AM, 0)) AS '_9AMTotal',
	SUM(ISNULL(FWC._10AM, 0)) AS '_10AMTotal',
	SUM(ISNULL(FWC._11AM, 0)) AS '_11AMTotal',
	SUM(ISNULL(FWC._12PM, 0)) AS '_12AMTotal',
	SUM(ISNULL(FWC._1PM, 0)) AS '_1PMTotal',
	SUM(ISNULL(FWC._2PM, 0)) AS '_2PMTotal',
	SUM(ISNULL(FWC._3PM, 0)) AS '_3PMTotal',
	SUM(ISNULL(FWC._4PM, 0)) AS '_4PMTotal',
	SUM(ISNULL(FWC._5PM, 0)) AS '_5PMTotal',
	SUM(ISNULL(FWC._6PM, 0)) AS '_6PMTotal',
	SUM(ISNULL(FWC._7PM, 0)) AS '_7PMTotal',
	SUM(ISNULL(FWC._8PM, 0)) AS '_8PMTotal',
	SUM(ISNULL(FWC._9PM, 0)) AS '_9PMTotal'
	INTO #tolWashCount
FROM #FinalWashCount FWC 
GROUP BY 
	FWC.JobDate
ORDER BY CONVERT(date,FWC.JobDate) ASC

DROP TABLE IF EXISTS #TotalDates
SELECT  TOP (DATEDIFF(DAY, @fromDate, @endDate) + 1)
        Date = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @fromDate) 
		into #TotalDates
FROM    sys.all_objects a
        CROSS JOIN sys.all_objects b

SELECT TD.Date AS JobDate,
	--CASE ISNULL(WP.CreatedDate,'') WHEN '' THEN FWC.JobDate ELSE WP.CreatedDate END AS JobDate,
	SUM(ISNULL(c._6AM, 0)) AS '_6AM',
	SUM(ISNULL(c._7AM, 0)) AS '_7AM',
	SUM(ISNULL(c._8AM, 0)) AS '_8AM',
	SUM(ISNULL(c._9AM, 0)) AS '_9AM',
	SUM(ISNULL(c._10AM, 0)) AS '_10AM',
	SUM(ISNULL(c._11AM, 0)) AS '_11AM',
	SUM(ISNULL(c._12PM, 0)) AS '_12AM',
	SUM(ISNULL(c._1PM, 0)) AS '_1PM',
	SUM(ISNULL(c._2PM, 0)) AS '_2PM',
	SUM(ISNULL(c._3PM, 0)) AS '_3PM',
	SUM(ISNULL(c._4PM, 0)) AS '_4PM',
	SUM(ISNULL(c._5PM, 0)) AS '_5PM',
	SUM(ISNULL(c._6PM, 0)) AS '_6PM',
	SUM(ISNULL(c._7PM, 0)) AS '_7PM',
	SUM(ISNULL(c._8PM, 0)) AS '_8PM',
	SUM(ISNULL(c._9PM, 0)) AS '_9PM',
	WP.Weather AS Temperature,
	WP.RainProbability AS Rain,
	WP.TargetBusiness AS Goal,
	ISNULL(WP.WashCount,0) NoOfWashes,
	ISNULL(twh.TotalHours,0) AS TotalWashHours,
	FWC._6AMTotal,
	FWC._7AMTotal,
	FWC._8AMTotal,
	FWC._9AMTotal,
	FWC._10AMTotal,
	FWC._11AMTotal,
	FWC._12AMTotal,
	FWC._1PMTotal,
	FWC._2PMTotal,
	FWC._3PMTotal,
	FWC._4PMTotal,
	FWC._5PMTotal,
	FWC._6PMTotal,
	FWC._7PMTotal,
	FWC._8PMTotal,
	FWC._9PMTotal
FROM #TotalDates TD 
LEFT JOIN #tolWashCount FWC ON FWC.JobDate = TD.Date
LEFT JOIN #WashHourResult c ON TD.Date = c.JobDate
LEFT JOIN #FinalWeather WP on TD.Date = WP.CreatedDate
LEFT JOIN #TotalWorkHours twh on twh.EventDate = TD.Date
--RIGHT JOIN #FinalWashCount FWC ON FWC.JobDate = WP.CreatedDate

GROUP BY 
	WP.Weather,
	WP.RainProbability,
	WP.TargetBusiness,
	WP.LocationId ,
	WP.CreatedDate,
	WP.WashCount,
	twh.TotalHours ,
	TD.Date,
	FWC._6AMTotal,
	FWC._7AMTotal,
	FWC._8AMTotal,
	FWC._9AMTotal,
	FWC._10AMTotal,
	FWC._11AMTotal,
	FWC._12AMTotal,
	FWC._1PMTotal,
	FWC._2PMTotal,
	FWC._3PMTotal,
	FWC._4PMTotal,
	FWC._5PMTotal,
	FWC._6PMTotal,
	FWC._7PMTotal,
	FWC._8PMTotal,
	FWC._9PMTotal
ORDER BY TD.Date ASC

END