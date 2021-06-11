CREATE PROCEDURE [StriveCarSalon].[uspGetHourlyWashReport] 
@locationId INT, @fromDate Date, @endDate Date
AS
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 01-Dec-2020
-- Description:	Returns the Hourly wash report data and Sales data. EXEC [StriveCarSalon].USPGETHOURLYWASHREPORT 2034, '2021-01-03', '2020-01-17' 
--[StriveCarSalon].[uspGetHourlyWashReport] 1,'2021-05-18','2021-05-18'
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


--Select JobDate, DATEPART(HOUR,ActualTimeOut) as [Hour] into #WashHours from strivecarsalon.tblJob 
--where LocationId = @locationId and JobStatus = @JobCompleted and jobDate between @fromDate and @endDate

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

SELECT 
	JobDate,
	ISNULL(SUM(_6AM), 0) as '_6AM',
	ISNULL(SUM(_7AM), 0) as '_7AM',
	ISNULL(SUM(_8AM), 0) as '_8AM',
	ISNULL(SUM(_9AM), 0) as '_9AM',
	ISNULL(SUM(_10AM), 0) as '_10AM',
	ISNULL(SUM(_11AM), 0) as '_11AM',
	ISNULL(SUM(_12PM), 0) as '_12AM',
	ISNULL(SUM(_1PM), 0) as '_1PM',
	ISNULL(SUM(_2PM), 0) as '_2PM',
	ISNULL(SUM(_3PM), 0) as '_3PM',
	ISNULL(SUM(_4PM), 0) as '_4PM',
	ISNULL(SUM(_5PM), 0) as '_5PM',
	ISNULL(SUM(_6PM), 0) as '_6PM',
	ISNULL(SUM(_7PM), 0) as '_7PM',
	ISNULL(SUM(_8PM), 0) as '_8PM',
	ISNULL(SUM(_9PM), 0) as '_9PM'
FROM 
	#WashHourResult 
GROUP BY jobdate

END