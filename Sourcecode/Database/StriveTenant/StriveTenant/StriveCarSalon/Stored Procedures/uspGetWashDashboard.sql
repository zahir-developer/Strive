 
-- =============================================
-- Author:		Vineeth
-- Create date: 20-08-2020
-- Description:	Dashboard detail for Washes
-- =============================================

---------------------History--------------------
-- =============================================
-- 10-09-2020, Vineeth - Added IsActive condition and JobType as params
-- 12-05-2021, Shalini - Added isdeleted condition and removed group by from #JobWashes
-- 19-05-2021, Shalini - Added jobtype filter to #JobWashe
-- 16-06-2021, Shalini -Added nullif conditon for@avgcount
-- 06-07-2021, Vetriselvi -Updated the score calculation
-- 06-07-2021, Vetriselvi -Updated the Average Wash Time calculation
-- 12-07-2021, Vetriselvi -Updated the Forecasted Cars calculation to past date
-- 21-07-2021, Vetriselvi - Washer logged in count was displaying car count
-- 02-08-2021, Vetriselvi - Removed Active and Deleted condition from tblService
-- 06-08-2021, Vetriselvi - Modified the Washcount,Detail count and Employee count as in Dashboard
-- 18-08-2021, Vetriselvi - Modified the Washcount,Detail count and Employee count as per FRS
-- 26-08-2021, Vetriselvi - Fixed Forecast and current count , Wash and detailer count should match current datetime
-- 30-08-2021, Vetriselvi - Washer logged in count
-- 31-08-2021, Vetriselvi - Fixed Average wash count 
-- 07-10-2021, Vetriselvi - Updated average wash time. When no washer clocked in or store is closed wash time is 0  
-- 15-10-2021, Vetriselvi - Updated average wash time. store should be closed on the time updated 
-- 18-10-2021, Vetriselvi - Fixed - if store is closed avg time should be zero 
-- 29-10-2021, Vetriselvi - Open the store only on the current time is greater than store in time
-- 02-11-2021, Vetriselvi - Score should include all employees except detailer and sum of actual minutes
-- 15-11-2021, Vetriselvi - Fixed avg wash time issue time zone issue
-- 29-11-2021, Vetriselvi - Included Off set in wash time calculation
-- 01-12-2021, Vetriselvi - Fixed avg wash time included offset and forecast should be avg of previous wash count
-- 22-12-2021, Vetriselvi - Fixed avg wash time bug
-- 17-01-2022, Vetriselvi - Fixed avg wash time 
------------------------------------------------
--[StriveCarSalon].[uspGetWashDashboard]20,'2021-03-18',57,'2021-03-11','2021-02-18' ,'2017-11-16' 
--[StriveCarSalon].[uspGetWashDashboard]1,'2021-05-01',57,'2021-03-11','2021-02-18' ,'2017-11-16' 
-- [StriveCarSalon].[uspGetWashDashboard]1,'2022-01-17 13:46:00.000',57,'2022-11-24','2021-11-01' ,'2021-09-01' 
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetWashDashboard] 
(@LocationId int, @CurrentDate datetime, @JobType int,@lastweek VARCHAR(10)= NULL,
@lastMonth VARCHAR(10) =NULL,
@lastThirdMonth  VARCHAR(10)= NULL)
as
begin 

DECLARE @DetailId INT = (SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Detail')
Declare @WashRole INT = (Select RoleMasterId from tblRoleMaster WHERE RoleName='Washer')
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Wash package')
Declare @CompletedJobStatus INT = (Select valueid from GetTable('JobStatus') where valuedesc='Completed')
Declare @DetailerRole int =(Select RoleMasterId from tblRoleMaster WHERE RoleName='Detailer')
DECLARE @CompletedPaymentStatus INT = (SELECT valueid FROM GetTable('PaymentStatus') WHERE valuedesc='Success')
DECLARE @DetailServiceId INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Detail Package')
Declare @CashRegisterType INT = (Select top 1 valueid from GetTable('CashRegisterType') where valuedesc='CashIn')

DECLARE @TargetBusiness INT = 0
DECLARE @firstWeek date,@endWeek date,@date datetime,@cDate date

SELECT @date = cast( @CurrentDate as datetime)

select @firstWeek = cast( @date - DATEPART(DW, @date) -6  AS date)

select @endWeek = cast( @date - DATEPART(DW, @date) AS date)

SELECT @cDate = cast(@CurrentDate as date)

DROP TABLE IF EXISTS #tblJob
Select jobId, LocationId, JobType, JobStatus, JobPaymentId, VehicleId, JobDate INTO #tblJob from tblJob tblj
where tblj.JobDate in (@cDate,@lastweek,@lastMonth,@lastThirdMonth)
AND ISNULL(tblj.IsDeleted,0) = 0 AND ISNULL(tblj.IsActive,0) = 1 AND tblj.LocationId = @LocationId

DROP TABLE IF EXISTS #tblJobItem
Select tblji.JobId, tblji.JobItemId, tblji.Price, ServiceId,EmployeeId
,tblji.IsActive , IsDeleted 
INTO #tblJobItem from tblJobItem tblji 
INNER JOIN #tblJob tblj on tblj.JobId = tblji.JobId	
WHERE tblji.IsActive=1 AND IsDeleted =0

DROP TABLE IF EXISTS #tblJobPayment
Select JobPaymentId, PaymentStatus INTO #tblJobPayment from tblJobPayment jp
WHERE jp.IsActive=1 
and ISNULL(jp.IsDeleted,0)=0 
and ISNULL(jp.IsRollBack,0) ! = 1
and jp.JobPaymentId in (select JobPaymentId from #tblJob WHERE JobDate = @cDate)


DROP TABLE IF EXISTS #tblService
Select ServiceId, ServiceType INTO #tblService from tblService tbls


DROP TABLE IF EXISTS #JobWashes
Select tblj.JobDate, tblj.locationId,tblj.JobType,tblj.jobId  into #JobWashes 
from #tblJob tblj 
inner join #tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)	
	WHERE tblj.JobType=@WashId
	AND tbls.ServiceType=@WashServiceId

DROP TABLE IF EXISTS #WashesCount
SELECT distinct 
	w.LocationId,COUNT(JobId) AS WashesCount
	INTO #WashesCount 
	FROM #JobWashes w	
	where w.JobDate=@cDate
	
	GROUP BY w.Locationid

DROP TABLE IF EXISTS #DetailsCount
SELECT  
	tblj.LocationId,COUNT(1) AS DetailsCount
	INTO #DetailsCount 
	FROM #tblJob tblj 
	inner join #tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	WHERE 
	tblj.JobType=@DetailId
	AND tbls.ServiceType=@DetailServiceId
	AND tblj.JobDate=@cDate 
	GROUP BY tblj.LocationId

DROP TABLE IF EXISTS #EmployeeCount
(SELECT
LocationId,COUNT(DISTINCT EmployeeId) EmployeeCount
INTO #EmployeeCount
FROM tblTimeClock 
WHERE RoleId=@WashRole
AND @CurrentDate between InTime 
AND OutTime
--(InTime>=@CurrentDate AND OutTime<=@CurrentDate) -- (EventDate=@CurrentDate) 
AND LocationId = @LocationId AND
IsActive=1 and IsDeleted=0
GROUP BY LocationId)


--TimeClock Details  
DROP TABLE IF EXISTS #TimeClock  
  
SELECT   
   LocationId  
 , TimeClockId  
 ,tblTC.EmployeeId
 , EventDate  
 , tblRM.RoleName   
 ,CASE WHEN ISNULL(OutTime,'') = '' THEN 
	 CASE WHEN @CurrentDate <= InTime THEN '0'
	 ELSE 
	 CASE WHEN 
		CAST(InTime AS DATE) = CAST(@CurrentDate AS DATE) 
		 THEN DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,@CurrentDate))
		 ELSE 0
	 END
	
	 END
 ELSE DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,@CurrentDate)) END  AS TotalMinutes
 --,Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,GETDATE())), 0), 114),':','.')  AS TotH  
 ,CASE WHEN ISNULL(OutTime,'') = '' THEN 
	 CASE WHEN @CurrentDate <= InTime THEN '0'
	 ELSE Replace(CONVERT(VARCHAR(5),
	 CASE WHEN 
		CAST(InTime AS DATE) = CAST(@CurrentDate AS DATE) 
		 THEN DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,@CurrentDate)), 0)
		 ELSE 0
	 END
	 , 114),':','.') 
	  --DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,GETDATE()))
	 
	 END 
 ELSE Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,@CurrentDate)), 0), 114),':','.')  
 END  AS TotH 
INTO  
 #TimeClock  
FROM   
 tblTimeClock tblTC  
 INNER JOIN tblRoleMaster tblRM  
ON  tblRM.RoleMasterId=tblTC.RoleId  
WHERE  LocationId = @LocationId
AND EventDate = @cDate AND RoleName != 'Detailer'
AND  ISNULL(tblTC.IsDeleted,0) = 0  

DROP TABLE  IF EXISTS #TotalCarWashed
(Select tblj.LocationId,Count(distinct tblj.JobId) TotalCarWashed INTO #TotalCarWashed 
from #tblJob tblj 
inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) 
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.JobStatus=@CompletedJobStatus
AND (tblj.JobDate=@cDate) 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalHoursWashed
SELECT LocationId,CAST((TotalWashHours/60.00) AS DECIMAL(18,2)) TotalHoursWashed
INTO #TotalHoursWashed 
FROM (
SELECT    
 LocationId,  
 --CASE WHEN RoleName='Washer' THEN ISNULL(CAST(TotH AS DECIMAL(18,2)),0) ELSE 0 END AS TotalWashHours
 SUM(TotalMinutes) AS TotalWashHours
FROM #TimeClock 
GROUP BY LocationId) a

-- Rate Calculation  
DROP TABLE IF EXISTS #Rate  
SELECT   
   tblED.EmployeeId  
 ,ISNULL(ehr.HourlyRate,0)AS WashRate  
 ,ehr.LocationId
INTO  
 #Rate  
FROM tblEmployeeDetail tblED  
left join  tblEmployeeHourlyRate ehr on tblED.EmployeeId=ehr.EmployeeId  
WHERE  ehr.LocationId = @LocationId  and ehr.IsActive = 1 and ehr.IsDeleted = 0  

--(SELECT tbll.LocationId,SUM(CAST((CAST(DATEDIFF(MINUTE, tbltc.InTime,ISNULL(tbltc.OutTime,tbltc.InTime))AS decimal(9,2))/60) AS decimal(9,2))) TotalHoursWashed
--INTO #TotalHoursWashed 
--FROM tblTimeClock tbltc
--inner join tbllocation tbll on(tbll.LocationId =tbltc.LocationId)
--WHERe tbltc.EventDate =@cDate  
--AND tbltc.LocationId=@LocationId
--GROUP BY tbll.LocationId)
/*
DROP TABLE IF EXISTS #tblJobForeCasted
Select jobId, LocationId, JobType, JobStatus, JobPaymentId, VehicleId 
INTO #tblJobForeCasted 
from [tblJob] tblj
where
	tblj.JobType=@WashId
	AND
	tblj.LocationId=@LocationId
	AND (tblj.JobDate = @date or tblj.JobDate = @firstWeek or tblj.JobDate = @endWeek)
	AND 
	isnull(tblj.IsDeleted,0)=0
	AND
	tblj.IsActive=1 

	
DROP TABLE  IF EXISTS #ForecastedCar
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)/3 ForecastedCar 
into #ForecastedCar
FROM #tblJobForeCasted tblj 
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
WHERE tblj.JobType = @WashId
and tbljp.PaymentStatus=@CompletedPaymentStatus
AND tblj.JobStatus=@CompletedJobStatus
AND tblj.LocationId = @LocationId
and tbljp.JobPaymentId = tblj.JobPaymentId
GROUP BY tblj.LocationId)

*/

DROP TABLE IF EXISTS #ForecastedCar
/*
Select (COUNT(DISTINCT j.JobId)) /3 as ForecastedCar,j.LocationId
into #ForecastedCar 
 FROM tblJob j 
INNER JOIN GetTable('JobType') JT on JT.valueid = j.JobType
INNER join GetTable('JobStatus') GT on GT.valueId = j.JobStatus and GT.valuedesc = 'Completed'
WHERE j.JobDate IN (@lastweek,@lastMonth,@lastThirdMonth) 
AND j.LocationId = @LocationId
GROUP BY LocationId*/

SELECT 
	tblj.LocationId,(COUNT(DISTINCT tblj.JobId))/3 ForecastedCar
	INTO #ForecastedCar
	FROM tblJob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)	
	inner join tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	WHERE tblj.JobType=@WashId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND tblj.JobDate IN (@lastweek,@lastMonth,@lastThirdMonth) AND tblj.IsActive = 1 AND ISNULL(tblj.IsDeleted,0) = 0 AND tblj.LocationId = @LocationId
	GROUP BY tblj.LocationId

--DROP TABLE IF EXISTS #ForecastedCars
--SELECT
--    tbll.LocationId,COUNT(distinct VehicleId) AS ForecastedCars
--	INTO #ForecastedCars 
--	FROM [tblJob] tblj
--	INNER JOIN [tblLocation] tbll ON(tblj.LocationId = tbll.LocationId)
--	WHERE f25
--	tblj.JobType=@WashId
--	AND
--	tblj.LocationId=@LocationId
--	AND
--	JobDate=@lastweek
--	AND 
--	isnull(tblj.IsDeleted,0)=0
--	AND
--	tblj.IsActive=1 GROUP BY tbll.LocationId

--DROP TABLE IF EXISTS #Current
--SELECT
--    tbll.LocationId,COUNT(VehicleId) AS [Current]
--	INTO #Current 
--	FROM [#tblJob] tblj
--	inner join GetTable('JobStatus') js ON(tblj.JobStatus = js.valueid)
--	INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
--	WHERE tblj.JobDate=@CurrentDate
--	AND tblj.JobType=@WashId
--	GROUP BY tbll.LocationId

	
DROP TABLE  IF EXISTS #Current
(SELECT
    tblj.LocationId,COUNT(distinct tblj.JobId) [Current]
into #Current FROM #tblJob tblj 
inner join #tblJobItem tblji on(tblj.JobId=tblji.JobId) 
inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId)
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	 WHERE tblj.JobType = @WashId 
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	and tblj.JobStatus=@CompletedJobStatus 
	and tbls.ServiceType = @WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)

	
--DROP TABLE IF EXISTS #WashRoleCount
--SELECT tblL.LocationId, COUNT(DISTINCT EmployeeId) Washer,
--		  COUNT_BIG (DISTINCT tblJ.JobId) CarCount 
--INTO #WashRoleCount FROM tblTimeClock tblTC Left JOIN
--tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
--left JOIN #tblJob tblJ ON (tblJ.LocationId = tblL.LocationId and tblj.JobDate= @CurrentDate)
--WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
--AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
----AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
--AND tblTC.RoleId =@WashRole AND tblTC.EventDate =@CurrentDate-- GETDATE() 
----AND tblJ.JobType=@WashId 
--and tblL.LocationId = @LocationId
--GROUP BY tblL.LocationId
	
DROP TABLE IF EXISTS #DetailRoleCount
SELECT tblL.LocationId, COUNT(DISTINCT EmployeeId) Detailer
INTO #DetailRoleCount FROM tblTimeClock tblTC Left JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
--left JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
--AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
AND tblTC.RoleId =@DetailerRole 
AND tblTC.EventDate=@cDate
AND (( @CurrentDate BETWEEN tblTC.InTime AND tblTC.OutTime ) or (@CurrentDate >= tblTC.InTime and tblTC.OutTime IS NULL )) 
--AND tblJ.JobType=@WashId 
GROUP BY tblL.LocationId


--DROP TABLE IF EXISTS #CarsCount

--Select tblj.LocationId, count(1) Cars into #CarsCount
--from #tblJob tblj
--INNER join GetTable('JobStatus') GT on GT.valueid = tblj.JobStatus and GT.valuedesc = 'In Progress' and tblj.JobType = @WashId
--WHERE  tblj.JobType = @WashId AND tblj.JobDate = @CurrentDate-- GETDATE()
--GROUP by tblj.LocationId ORDER BY 1 DESC

--Forcasted Cars and Employeehours
DROP TABLE IF EXISTS #WashTime
DROP TABLE IF EXISTS #Forcasted
SELECT 
WP.Weather,
WP.RainProbability,
convert (decimal(18,2),(ISNULL(wc.WashesCount,0)*1.5)) AS WashTimeMinutes,
a.JobDate ,
WP.LocationId,
WP.CreatedDate into #WashTime
FROM [tblWeatherPrediction] WP
LEFT join #JobWashes a on CONVERT(VARCHAR(10), wp.CreatedDate, 120) = a.JobDate
Left Join #washesCount wc on wp.LocationId =wc.LocationId
WHERE
WP.LocationId =@LocationId AND CONVERT(VARCHAR(10), wp.CreatedDate, 120) in (@cDate,@lastweek,@lastMonth,@lastThirdMonth) 
and wp.Weather IS NOT NULL AND WP.RainProbability IS nOT NULL 
ORDER BY CreatedDate DESC
DECLARE @AvgCount INT = (Select count(1) from #WashTime WHERE WashTimeMinutes >0 )
DECLARE @Normal DECIMAL(18,2) = (Select SUM(CONVERT(DECIMAL(18,2),WashTimeMinutes))/nullif(@AvgCount,0) from #WashTime)
DECLARE @Today_RainPrecipitation int = (select top 1 RainProbability from #WashTime where CONVERT(VARCHAR(10), CreatedDate, 120)  =@cDate)
DECLARE @Loc int = (select top 1 LocationId from #WashTime)
DEclare @Formula decimal(18,2) =(select top (1)Formula from tblForcastedRainPercentageMaster fr 
where @Today_RainPrecipitation between fr.PrecipitationRangeFrom and fr.PrecipitationRangeTo)
select @AvgCount as Average,@Normal as Normal, ISNULL(Round(@Normal * @Formula,0),0) as ForcastedemployeeHours,@Loc as LocationId
,ISNULL(Round((@Normal * @Formula) / 1.25,0),0) as ForcastedCars ,@Today_RainPrecipitation as RainPrecipitation into #Forcasted


DROP TABLE IF EXISTS #EventDateForLocation
SELECT  tblL.LocationId,tblTC.EventDate
INTO #EventDateForLocation 
FROM tblTimeClock tblTC INNER JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
INNER JOIN tblJob tblj ON(tblJ.LocationId = tblL.LocationId)
WHERE (tblL.LocationId = @LocationId ) AND
tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole  
AND tblJ.JobType=@WashId 
AND tblj.JobDate=@cDate
AND (tblTC.EventDate=@cDate ) 
GROUP BY tblL.LocationId,tblTC.EventDate


DROP TABLE IF EXISTS #WashRoleCount
SELECT tblL.LocationId, COUNT(DISTINCT EmployeeId) Washer
INTO #WashRoleCount FROM tblTimeClock tblTC 
JOIN tblLocation tblL ON (tblTC.LocationId = tblL.LocationId) 
WHERE  --Cast(tblTC.InTime AS datetime) >= @FromDate and Cast(tblTC.OutTime AS datetime) <=@ToDate and 
 tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole --AND tblTC.EventDate=@cDate
--AND (( @CurrentDate BETWEEN tblTC.InTime AND tblTC.OutTime ) or
--and (@CurrentDate >= tblTC.InTime and (tblTC.OutTime IS NULL  or @CurrentDate <= tblTC.OutTime))
AND (@CurrentDate>= tblTC.InTime and ( @CurrentDate <= tblTC.OutTime) OR tblTC.OutTime IS NULL)
--tblTC.EventDate>=Cast(@CurrentDate AS date) AND (tblTC.EventDate<=Cast(@CurrentDate AS date) or tblTC.EventDate is null)
AND tblTC.EventDate = CAST(@CurrentDate AS DATE)
GROUP BY tblL.LocationId

DROP TABLE IF EXISTS #CarsCount

Select tblj.LocationId, count(DISTINCT tblj.JobId) CarCount into #CarsCount
from tblJob tblj
JOIN tblLocation tblL ON (tblj.LocationId = tblL.LocationId) 
INNER join GetTable('JobStatus') GT on GT.valueid = tblj.JobStatus and GT.valuedesc = 'In Progress' 
WHERE ISNULL(tblj.IsDeleted, 0) = 0
AND tblj.JobType = @WashId AND tblj.JobDate =@cDate
GROUP by tblj.LocationId


--Location Open/Closed status
DROP TABLE IF EXISTS #StoreStatus

Select cr.locationId, cr.CashRegisterId, 
cr.StoreTimeIn,
cr.StoreTimeOut,
cv.CodeValue as StoreStatus into #StoreStatus
from tblCashRegister cr
LEFT JOIN tblCodeValue cv on cv.id = cr.StoreOpenCloseStatus 
WHERE cr.CashRegisterDate = @cDate and cr.CashRegisterType = @CashRegisterType
and ISNULL(cr.IsDeleted,0) = 0


DROP TABLE  IF EXISTS #AvgWashTime
(SELECT tbll.LocationId,
CASE
	   WHEN ISNULL(wt.Washer,0) = 0 THEN 0
	   WHEN (ss.StoreStatus = 'Open'  AND ss.StoreTimeIn > @CurrentDate) THEN  0
	   WHEN ((ss.StoreStatus = 'Closed' OR ss.StoreStatus = 'Closed for Weather') AND ss.StoreTimeOut <= @CurrentDate)  THEN 0
	   WHEN ss.StoreStatus IS NULL AND ss.StoreTimeOut IS NULL THEN 0
	   WHEN ISNULL(wt.Washer,0) <=3 AND car.CarCount <=1 THEN 25
	   WHEN ISNULL(wt.Washer,0) <=3 AND car.CarCount >1 THEN (25+(car.CarCount - 1)*8) + (((car.CarCount - 1)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) <=6 AND car.CarCount <=1 THEN 25
	   WHEN ISNULL(wt.Washer,0) <=6 AND car.CarCount >1 THEN (25+(car.CarCount - 1)*7) + (((car.CarCount - 1)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) <=9 AND car.CarCount <=1 THEN 25
	   WHEN ISNULL(wt.Washer,0) <=9 AND car.CarCount >1 THEN (25+(car.CarCount - 1)*6) + (((car.CarCount - 1)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) <=11 AND car.CarCount <=3 THEN 25
	   WHEN ISNULL(wt.Washer,0) <=11 AND car.CarCount >3 THEN (25+(car.CarCount - 3)*5) + (((car.CarCount - 3)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) >=12 AND ISNULL(wt.Washer,0)<=15 AND car.CarCount <=5 THEN 25
	   WHEN ISNULL(wt.Washer,0) >=12 AND ISNULL(wt.Washer,0)<=15 AND car.CarCount >5  THEN (25+(car.CarCount - 5)*3) + (((car.CarCount - 5)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) >=16 AND ISNULL(wt.Washer,0)<=21 AND car.CarCount <=5 THEN 25
	   WHEN ISNULL(wt.Washer,0) >=16 AND ISNULL(wt.Washer,0)<=21 AND car.CarCount >5  THEN (25+(car.CarCount - 5)*2) + (((car.CarCount - 5)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) >=22 AND ISNULL(wt.Washer,0)<=26 AND car.CarCount <=5 THEN 25
	   WHEN ISNULL(wt.Washer,0) >=22 AND ISNULL(wt.Washer,0)<=26 AND car.CarCount >5  THEN (25+(car.CarCount - 5)*2) + (((car.CarCount - 5)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) >26 AND car.CarCount <=7 THEN 25
	   WHEN ISNULL(wt.Washer,0) >26 AND car.CarCount >7  THEN (25+(car.CarCount - 7)*2) + (((car.CarCount - 7)* ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   ELSE 25

	   END AS WashTimeMinutes,wt.Washer
	   INTO #AvgWashTime
	   FROM tblLocation tbll
LEFT JOIN #WashRoleCount wt ON(tbll.LocationId = wt.LocationId)
LEFT JOIN #CarsCount car ON car.LocationId = tbll.LocationId
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbllo.LocationId)
LEFT JOIN #EventDateForLocation edfl ON(tbll.LocationId = edfl.LocationId)
LEFT JOIN #StoreStatus ss ON ss.LocationId = tbll.LocationId
WHERE isnull(tbllo.IsActive,1) = 1 AND
isnull(tbllo.isDeleted,0) = 0  and tbll.LocationId = @LocationId
	)


	DROP TABLE  IF EXISTS #WashServices
(	
SELECT distinct tbllo.LocationId, SUM(
CASE 
	WHEN tbls.ServiceName ='Wash Upcharges (A)' THEN ( 0 + ISNULL(tbllo.OffSetA,0))*ISNULL(tbllo.OffSet1On,0)
	WHEN tbls.ServiceName ='Wash Upcharges (B)' THEN ( 1 + ISNULL(tbllo.OffSetB,0))*ISNULL(tbllo.OffSet1On,0)
	WHEN tbls.ServiceName ='Wash Upcharges (C)' THEN ( 2 + ISNULL(tbllo.OffSetC,0))*ISNULL(tbllo.OffSet1On,0)
	WHEN tbls.ServiceName ='Wash Upcharges (D)' THEN ( 3 + ISNULL(tbllo.OffSetD,0))*ISNULL(tbllo.OffSet1On,0)
	WHEN tbls.ServiceName ='Wash Upcharges (E)' THEN ( 4 + ISNULL(tbllo.OffSetE,0))*ISNULL(tbllo.OffSet1On,0)
	WHEN tbls.ServiceName ='Wash Upcharges (F)' THEN ( 4 + ISNULL(tbllo.OffSetF,0))*ISNULL(tbllo.OffSet1On,0)
	ELSE 0 END) AS WashTimeMinutes
	INTO #WashServices
FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join GetTable('ServiceType') serviceType on serviceType.valueid = tbls.ServiceType
LEFT JOIN tblLocationOffSet tbllo ON(tblj.LocationId = tbllo.LocationId) and tbllo.IsActive = 1 AND
	tbllo.isDeleted = 0
LEFT JOIN #StoreStatus ss ON ss.LocationId = tblj.LocationId
INNER join GetTable('JobStatus') GT on GT.valueid = tblj.JobStatus and GT.valuedesc = 'In Progress' and tblj.JobType = @WashId
AND tblj.JobDate = @cDate AND ISNULL(tblj.IsDeleted,0) = 0
WHERE serviceType.valuedesc like '%Upcharge%'  AND ISNULL(tblji.IsDeleted,0) = 0 and tblj.LocationId = @LocationId
GROUP BY tbllo.LocationId
	)

	DROP TABLE  IF EXISTS #WashTimeOffSet
(SELECT wt.LocationId,
CASE
	   WHEN ISNULL(wt.Washer,0) = 0 THEN 0
	   WHEN (ss.StoreStatus = 'Open'  AND ss.StoreTimeIn > @CurrentDate) THEN  0
	   WHEN ((ss.StoreStatus = 'Closed' OR ss.StoreStatus = 'Closed for Weather') AND CAST(CONVERT(CHAR(16), ss.StoreTimeOut,20) AS datetime) <= CAST(CONVERT(CHAR(16),@CurrentDate,20) AS datetime)) THEN 0
	   ELSE ISNULL(wt.WashTimeMinutes,0) + ISNULL(ws.WashTimeMinutes,0)
	   END AS WashTimeMinutes
	   INTO #WashTimeOffSet
	  FROM #AvgWashTime wt
LEFT JOIN #WashServices ws  ON wt.LocationId = ws.LocationId
LEFT JOIN tblLocationOffSet tbllo ON(ws.LocationId = tbllo.LocationId) and tbllo.IsActive = 1 AND
tbllo.isDeleted = 0  
LEFT JOIN #StoreStatus ss ON ss.LocationId = ws.LocationId
 

	)

SELECT @TargetBusiness = TargetBusiness FROM [tblWeatherPrediction]
WHERE CAST( CreatedDate AS DATE) = CAST( @CurrentDate AS DATE)
AND LocationId = @LocationId

SELECT DISTINCT
ISNULL(WC.WashesCount,0) WashesCount,
ISNULL(DC.DetailsCount,0) DetailsCount,
ISNULL(wr.Washer,0) EmployeeCount,
IsNull(wr.Washer,0) as WasherCount,
IsNull(dr.Detailer,0) as DetailerCount,
CASE WHEN ISNULL(@TargetBusiness,0) = 0 THEN  ISNULL(FC.ForecastedCar,0) ELSE @TargetBusiness END  ForecastedCars,
ISNULL(cu.[Current],0) [Current],

--ISNULL(fc.ForcastedCars,0) ForecastedCars,
ISNULL(f.ForcastedemployeeHours,0) ForecastedEmployeeHours,
CASE 
	       WHEN thw.TotalHoursWashed=0 THEN '0.00' 
		   WHEN thw.TotalHoursWashed!=0 THEN CAST(((ISNULL(tcw.TotalCarWashed,0)/thw.TotalHoursWashed)*100)as decimal(9,2))
		   END AS Score
,CONVERT(decimal(18,0), wt.WashTimeMinutes) AverageWashTime
FROM [tblLocation] tbll 
LEFT JOIN #WashesCount WC ON(tbll.LocationId = WC.LocationId)
LEFT JOIN #DetailsCount DC ON(tbll.LocationId = DC.LocationId)
LEFT JOIN #EmployeeCount EC ON(tbll.LocationId = EC.LocationId)
LEFT JOIN #TotalCarWashed tcw on(tbll.LocationId = tcw.LocationId)
LEFT JOIN #TotalHoursWashed thw on(tbll.LocationId = thw.LocationId)
--LEFT JOIN #ForecastedCars fc on(tbll.LocationId = fc.LocationId)
LEFT JOIN #Current cu on(tbll.LocationId = cu.LocationId)
LEFT JOIN #WashRoleCount wr ON(tbll.LocationId = wr.LocationId)

LEFT JOIN #DetailRoleCount dr ON(tbll.LocationId = dr.LocationId)
LEFT JOIN #Forcasted f on(tbll.LocationId = f.LocationId)
--LEFT JOIN #CarsCount cc on (tbll.LocationId = cc.LocationId)
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbllo.LocationId)
LEFT JOIN #WashTimeOffSet wt ON (tbll.LocationId = wt.LocationId)
LEFT JOIN #ForecastedCar fc ON(tbll.LocationId = fc.LocationId)
WHERE tbll.LocationId =@LocationId


end
