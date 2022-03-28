-- =============================================
-- Author:		Shalini
-- Create date: To get past week and month weather prediction
 --[StriveCarSalon].[uspGetPastWeatherInfo] 1,'2021-12-01', '2021-11-24', '2021-11-01','2021-09-01'
-- =============================================
-- =============================================
----------History------------

-- =============================================

--13-Jul-2021 -- Added UpdatedDate and CreatedDate filter 
-- 01-12-2021, Vetriselvi - Modified the wash count as in main dashboard
 --[StriveCarSalon].[uspGetPastWeatherInfo] 1,'2021-09-07','2021-08-31','2021-07-08','2021-06-07'
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetPastWeatherInfo]
	(
@LocationId int,
@date date,
----@today VARCHAR(10),
@lastweek VARCHAR(10),
@lastMonth VARCHAR(10),
@lastThirdMonth  VARCHAR(10)
)



AS
BEGIN

DECLARE @WashId INT = (SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')
DECLARE @CompletedPaymentStatus INT = (SELECT valueid FROM GetTable('PaymentStatus') WHERE valuedesc='Success')
DECLARE @WashServiceId INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Wash Package')
DECLARE @CompletedJobStatus INT = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')
DECLARE @week Date
SELECT @week = DATEADD(wk, DATEDIFF(wk, 6, @date), 0)

DROP TABLE IF EXISTS #WashHours

--Select count(1) as WashCount,LocationId,j.JobDate into #WashHours from tblJob j 
--INNER JOIN GetTable('JobType') JT on JT.valueid = j.JobType
--INNER join GetTable('JobStatus') GT on GT.valueId = j.JobStatus and GT.valuedesc = 'Completed'
--WHERE j.JobDate IN (@date,@lastweek,@lastMonth,@lastThirdMonth)  AND LocationId = @LocationId
-- AND j.JobType=@WashId
--GROUP BY LocationId,j.JobDate
SELECT 
	tblj.LocationId,COUNT(DISTINCT tblj.JobId) WashCount,
	CASE when tblj.JobDate = @date then @date else @lastweek End as JobDate
	INTO #WashHours
	FROM tblJob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)	
	inner join tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	WHERE tblj.JobType=@WashId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND tblj.JobDate IN (@date,@week) AND tblj.IsActive = 1 AND ISNULL(tblj.IsDeleted,0) = 0
	AND tblj.LocationId =@LocationId
	GROUP BY tblj.LocationId,tblj.JobDate


DROP TABLE IF EXISTS #AllDates
CREATE TABLE #AllDates (JobDate DATE)
INSERT INTO #AllDates
SELECT @date
UNION 
SELECT @lastweek
UNION 
SELECT @lastMonth
UNION 
SELECT @lastThirdMonth

declare @dt int = (select datepart ("dw", @date))
DROP TABLE IF EXISTS #PreMonth
DECLARE @DateFrom DateTime = (select DATEADD(MONTH, DATEDIFF(MONTH, 0, @date)-1, 0)),
@DateTo DateTime = (select DATEADD(MONTH, DATEDIFF(MONTH, -1, @date)-1, -1))

;WITH CTE(dt)
AS
(
      SELECT @DateFrom
      UNION ALL
      SELECT DATEADD(d, 1, dt) FROM CTE
      WHERE dt < @DateTo
)
SELECT dt into #PreMonth FROM CTE  where datepart ("dw", dt) = @dt;


DROP TABLE IF EXISTS #LastMonthWashHours

SELECT 
	tblj.LocationId,COUNT(DISTINCT tblj.JobId) WashCount,tblj.JobDate
	INTO #LastMonthWashHours
	FROM tblJob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)	
	inner join tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	WHERE tblj.JobType=@WashId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND tblj.JobDate IN (SELECT dt FROM #PreMonth) AND tblj.IsActive = 1 AND ISNULL(tblj.IsDeleted,0) = 0
	AND tblj.LocationId =@LocationId
	GROUP BY tblj.LocationId,tblj.JobDate
	
	if((select count(1) from #LastMonthWashHours where WashCount > 0) > 0)
	BEGIN
		insert into #WashHours
		select locationId,SUM(WashCount)/COUNT(JobDate),@lastMonth from #LastMonthWashHours
		where WashCount > 0
		GROUP BY locationId
	END

	DROP TABLE IF EXISTS #PreThreeMonth
DECLARE @From DateTime = (select DATEADD(MONTH, DATEDIFF(MONTH, 0, @date)-3, 0)),
@To DateTime = @lastweek

;WITH CTE(dt)
AS
(
      SELECT @From
      UNION ALL
      SELECT DATEADD(d, 1, dt) FROM CTE
      WHERE dt < @To
)
(SELECT dt into #PreThreeMonth FROM CTE  where datepart ("dw", dt) = @dt) option(maxrecursion 0);


DROP TABLE IF EXISTS #LastThreeMonthWashHours

SELECT 
	tblj.LocationId,COUNT(DISTINCT tblj.JobId) WashCount,tblj.JobDate
	INTO #LastThreeMonthWashHours
	FROM tblJob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)	
	inner join tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	WHERE tblj.JobType=@WashId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND tblj.JobDate IN (SELECT dt FROM #PreThreeMonth) AND tblj.IsActive = 1 AND ISNULL(tblj.IsDeleted,0) = 0
	AND tblj.LocationId =@LocationId
	GROUP BY tblj.LocationId,tblj.JobDate
	
	if((select count(1) from #LastThreeMonthWashHours where WashCount > 0) > 0)
	BEGIN
		insert into #WashHours
		select locationId,SUM(WashCount)/COUNT(JobDate),@lastThirdMonth from #LastThreeMonthWashHours
		where WashCount > 0
		GROUP BY locationId
	END

SELECT 
WP.WeatherId,
WP.Weather,
WP.RainProbability,
WP.PredictedBusiness,
WP.TargetBusiness,
w.LocationId ,
AD.JobDate CreatedDate,
w.WashCount
FROM #AllDates AD
LEFT JOIN [tblWeatherPrediction] WP ON (CAST( wp.UpdatedDate AS DATE) = AD.JobDate OR CAST( wp.CreatedDate AS DATE) = AD.JobDate) and WP.LocationId =@LocationId
LEFT join #WashHours w on AD.JobDate = w.JobDate and W.LocationId =@LocationId
ORDER BY 1 DESC

END
