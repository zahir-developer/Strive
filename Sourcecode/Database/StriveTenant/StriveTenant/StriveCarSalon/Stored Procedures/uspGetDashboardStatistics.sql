-- =============================================
-- Author:		Vineeth B
-- Create date: 03-11-2020
-- Description:	To get Dashboard Details
--  --
/*

[StriveCarSalon].[uspGetDashboardStatistics] 7,'2022-01-17','2022-01-17','2022-01-17 20:56:00.000'

*/
-- =============================================
----------History------------
-- =============================================
--	10-june-10-2021 -- added jobdate filter in #WashRoleCount
--  28-Jun-2021 - Vetriselvi - Added Rollback filter in sales
--  2-Jul-2021 - Vetriselvi - Changed Monthly sales to Membership sales
--  08-Jul-2021 - Vetriselvi - fixed Labor Cost per car and Detail cost per car
-- 12-07-2021, Vetriselvi - Updated the Forecasted Cars calculation to past date
-- 16-07-2021, Vetriselvi - Removed Cash and Card condition in wash sales. (all sale included in wash sales calculation)
-- 					   - Reverted the Membership Client Sales to old code Monthly Client Sales
-- 20-07-2021 Vetriselvi  - Labour Cost Per Car calculation :sum( no of hours worked * com.Amt )/ total wash count completed for the supplied date
-- 						- Detailed Commission Amount calculation :sum( detail commission)/ total detail service for the supplied date
-- 21-07-2021 Vetriselvi  - wash sales calc. removed duplicates
-- 22-07-2021 Vetriselvi  - Membership amount used in sales 
-- 27-07-2021, Zahir - Query Optimized.
--					 - #MonthlyClientSales - tblJobPaymentDetail/#PaymentType, LEFT JOIN changed to INNER JOIN
--					 - #ForecastedCar - tblJob table replaced with #tblJobForecasted (jobdate matching with @date, @firstWeek, @endWeek) 
--					 - #ForecastedCar - tblJobItem and Service table removed only JobType Wash filter applied.
--					 - ISNULL Removed for IsDeleted/IsActive colums of tblLocation/tblTimeClock/tblJob/tblJobItem tables.
-- 02-08-2021, Vetriselvi - Removed Active and Deleted condition from tblService
--				 		  - Included all item price in total price calculation
-- 03-08-2021, Vetriselvi - Removed Distinct keyword from sales calculation
-- 8-08-2021, Vetriselvi - Added Distinct keyword from wash employee count calculation
-- 19-08-2021, Vetriselvi - Fixed Average wash time
-- 19-08-2021 - Merchandize Completed Job condition removed
-- 20-08-2021, Vetriselvi - Include all products in MerchandizeSales
-- 23-08-2021, Vetriselvi - Modified the calculation for average wash time and in total sales included tax amount
-- 23-08-2021, Zahir - Upcharge Added in Total Sales
-- 31-08-2021, Vetriselvi - Fixed Average wash count 
-- 01-09-2021, Vetriselvi - Fixed wash sales issue 
-- 02-09-2021, Vetriselvi - Added Tips to total amount
--14-09-2021 - Vetriselvi - Added Rollback condition
--15-09-2021 - Vetriselvi - Added Quantity in MerchandizeSales
--29-09-2021 - Vetriselvi - Fixed Labor cost issue
--30-09-2021 - Vetriselvi - Labor cost, in emp time clock if user didn't log off and shouldn't carry foward to next day
-- 07-10-2021, Vetriselvi - Updated average wash time. When no washer clocked in or store is closed wash time is 0 
-- 15-10-2021, Vetriselvi - Updated average wash time. store should be closed on the time updated 
-- 18-10-2021, Vetriselvi - Fixed - if store is closed avg time should be zero 
-- 20-10-2021, Vetriselvi - Removed tips frpm total sales Bug id - 1168
-- 25-10-2021, Vetriselvi - 1181 Web app- "total sales" of washes, details, extra services, merchandise paid that day 
						  --1182 Web app- Dashboard showing wrong "ave of total per car" 
						  --1183 Web app- changed score the formula mentioned by Client
-- 27-10-2021, Vetriselvi --1183 Reverted the score formula as in FRS
-- 29-10-2021, Vetriselvi - Open the store only on the current time is greater than store in time
-- 02-11-2021, Vetriselvi - Score should include all employees except detailer and sum of actual minutes
-- 15-11-2021, Vetriselvi - Fixed avg wash time issue time zone issue
-- 01-12-2021, Vetriselvi - Fixed avg wash time included offset and forecast should be avg of previous wash count
-- 22-12-2021, Vetriselvi - Fixed avg wash time bug
-- 17-01-2022, Vetriselvi - Fixed avg wash time and extra sales bug
-- 18-01-2022, Vetriselvi - Fixed bug in average wash and average total per car 
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetDashboardStatistics]
(@LocationId INT,@FromDate Date,@ToDate Date,@CurrentDate DATETIME)
AS
BEGIN

--DECLARE @locationId INT = 1, @FromDate Date = '2022-03-09', @ToDate Date = '2022-03-09', @CurrentDate DateTime = '2022-03-09 23:35:57'

DECLARE @WashId INT = (SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')
DECLARE @WashServiceId INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Wash Package')
DECLARE @DetailServiceId INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Detail Package')
DECLARE @AdditionalServiceId INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Additional Services')
DECLARE @DetailId INT = (SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Detail')
DECLARE @CompletedJobStatus INT = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')
DECLARE @WashRole INT = (SELECT RoleMasterId FROM tblRoleMaster WHERE RoleName='Washer')
DECLARE @CompletedPaymentStatus INT = (SELECT valueid FROM GetTable('PaymentStatus') WHERE valuedesc='Success')
DECLARE @ServiceType INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc ='Additional Services')
DECLARE @MerchandizeId INT =(SELECT valueid FROM GetTable('ProductType') WHERE valuedesc='Merchandize')
DECLARE @AirFresheners INT = (SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc ='Air Fresheners')

Declare @CashRegisterType INT = (Select top 1 valueid from GetTable('CashRegisterType') where valuedesc='CashIn')

DECLARE @firstWeek date,@endWeek date,@date datetime ,@lastweek date,@lastMonth date,@lastThirdMonth date


SELECT @date = cast( @FromDate as datetime)


select @lastweek = cast( DATEADD(DAY, -7, @date)  AS date)
select @lastMonth = cast( DATEADD(month, -1, @date)  AS date)
select @lastThirdMonth = cast( DATEADD(month, -3, @date)  AS date)

select @firstWeek = cast( @date - DATEPART(DW, @date) -6  AS date)


select @endWeek = cast( @date - DATEPART(DW, @date) AS date)

DECLARE @locationCount INT

DROP TABLE IF EXISTS #LocationIds
CREATE TABLE #LocationIds (id INT)
IF(ISNULL(@LocationId,0) =0)
BEGIN
	INSERT INTO #LocationIds
	SELECT LocationId AS ID FROM tbllocation WHERE IsActive = 1
END
ELSE
BEGIN
	INSERT INTO #LocationIds
	Select @LocationId  
END


DROP TABLE  IF EXISTS #PaymentType
Select Cv.id,CodeValue,Category
into #PaymentType
from tblcodevalue cv
join tblCodeCategory CC
ON cc.id=cv.CategoryId
Where CC.[Category]='PaymentType'


SELECT @locationCount = COUNT(1) FROM #LocationIds

DROP TABLE IF EXISTS #tblAllJob
Select jobId, LocationId, JobType, JobStatus, JobPaymentId, VehicleId, JobDate INTO #tblAllJob from tblJob tblj
where (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) Or (tblj.JobDate = @date or tblj.JobDate = @firstWeek or tblj.JobDate = @endWeek)
AND tblj.IsDeleted = 0 AND tblj.LocationId IN (Select ID from #LocationIds)


DROP TABLE IF EXISTS #tblJob
Select jobId, LocationId, JobType, JobStatus, JobPaymentId, VehicleId, JobDate,ClientId INTO #tblJob from tblJob tblj
where (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate)
AND tblj.IsDeleted = 0 AND tblj.LocationId IN (Select ID from #LocationIds)

/*
DROP TABLE IF EXISTS #tblJobForeCasted
Select jobId, LocationId, JobType, JobStatus, JobPaymentId, VehicleId INTO #tblJobForeCasted from #tblAllJob tblj
where (tblj.JobDate = @lastweek or tblj.JobDate = @lastMonth or tblj.JobDate = @lastThirdMonth)
*/

DROP TABLE IF EXISTS #tblJobItem
Select tblji.JobId, tblji.JobItemId, tblji.Price, ServiceId,Quantity INTO #tblJobItem from tblJobItem tblji 
INNER JOIN #tblJob tblj on tblj.JobId = tblji.JobId
WHERE tblji.IsActive=1 AND IsDeleted =0


DROP TABLE IF EXISTS #tblService
Select ServiceId, ServiceType,ServiceName INTO #tblService from tblService tbls


DROP TABLE IF EXISTS #tblJobPayment
Select jp.JobPaymentId, PaymentStatus--,CodeValue 
INTO #tblJobPayment from tblJobPayment jp 
--JOIN tblJobPaymentDetail pd ON jp.JobPaymentId = pd.JobPaymentId
--join tblCodeValue cv on pd.PaymentType = cv.id
WHERE jp.IsActive=1 
and ISNULL(jp.IsDeleted,0)=0 
and ISNULL(jp.IsRollBack,0) ! = 1
and jp.JobPaymentId in (select JobPaymentId from #tblJob)--Is Rollback



DROP TABLE IF EXISTS #tblMemebershipPayment
Select jb.LocationId, SUM(ISNULL(pd.Amount,0)) Amount
INTO #tblMemebershipPayment 
from tblJobPayment jp 
JOIN tblJobPaymentDetail pd ON jp.JobPaymentId = pd.JobPaymentId
join tblCodeValue cv on pd.PaymentType = cv.id
join #tblJob jb on jp.JobPaymentId = jb.JobPaymentId
WHERE jp.IsActive=1 
and ISNULL(jp.IsDeleted,0)=0 
and ISNULL(jp.IsRollBack,0) ! = 1
--and jp.JobPaymentId in (select JobPaymentId from #tblJob)
AND cv.CodeValue = 'Membership'
GROUP BY jb.LocationId

DROP TABLE IF EXISTS #EmpTips
SELECT distinct  p.JobPaymentId,j.LocationId,(ISNULL(pd.Amount,0)) TipsAmount
into #EmpTips
From #tblJob j 
join #tblJobPayment p on j.JobPaymentId = p.JobPaymentId
JOIN tblJobPaymentDetail pd on pd.JobPaymentId = p.JobPaymentId
join tblCodeValue cv on cv.id = pd.PaymentType
WHERE cv.CodeValue = 'Tips'
AND p.PaymentStatus=@CompletedPaymentStatus
AND j.JobStatus=@CompletedJobStatus

--DROP TABLE IF EXISTS #TipsAmount
--SELECT   LocationId,SUM(ISNULL(TipsAmount,0)) TipsAmount
--into #TipsAmount
--From #EmpTips
--group by LocationId


DROP TABLE IF EXISTS #WashRoleCount
SELECT tblL.id AS LocationId, COUNT(DISTINCT EmployeeId) Washer
INTO #WashRoleCount FROM tblTimeClock tblTC 
JOIN #LocationIds tblL ON(tblTC.LocationId = tblL.id) 
--left JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE  --Cast(tblTC.InTime AS datetime) >= @FromDate and Cast(tblTC.OutTime AS datetime) <=@ToDate and 
 tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole AND tblTC.EventDate>=@FromDate AND (tblTC.EventDate<=@ToDate or tblTC.EventDate is null)
GROUP BY tblL.id

DROP TABLE IF EXISTS #CarsCount

Select tblj.LocationId, count(DISTINCT tblj.JobId) CarCount into #CarsCount
from tblJob tblj
INNER JOIN #LocationIds tbll on tbll.id = tblj.LocationId
INNER join GetTable('JobStatus') GT on GT.valueid = tblj.JobStatus and GT.valuedesc = 'In Progress' and tblj.JobType = @WashId
WHERE ISNULL(tblj.IsDeleted, 0) = 0
AND tblj.JobType = @WashId AND tblj.JobDate >=@FromDate AND tblj.JobDate <=@ToDate
GROUP by tblj.LocationId
 
 /*
DROP TABLE IF EXISTS #WashRoleCount
SELECT	  tblL.LocationId,
		  COUNT_BIG (DISTINCT tblTC.EmployeeId) Washer,
		  COUNT_BIG (DISTINCT tblJ.JobId) CarCount 
INTO #WashRoleCount 
FROM tblTimeClock tblTC 
INNER JOIN tblLocation tblL ON (tblTC.LocationId = tblL.LocationId) 
INNER JOIN #tblJob tblJ ON (tblJ.LocationId = tblL.LocationId) 
WHERE (tblL.LocationId IN (select Id from #LocationIds) OR @LocationId = 0)
AND (tblTC.EventDate>=@FromDate AND tblTC.EventDate<=@ToDate)   

AND tblL.IsDeleted = 0 
AND tblL.IsActive = 1 
AND tblTC.IsActive = 1 
AND tblTC.IsDeleted = 0 
AND tblTC.RoleId = @WashRole 
AND tblJ.JobType = @WashId 

GROUP BY tblL.LocationId
*/

DROP TABLE IF EXISTS #EventDateForLocation
SELECT tblL.LocationId,tblTC.EventDate
INTO #EventDateForLocation 
FROM tblTimeClock tblTC INNER JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
INNER JOIN #tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE (tblL.LocationId IN (select Id from #LocationIds) OR @LocationId = 0) AND
tblL.IsActive=1 AND tblL.IsDeleted =0 
AND tblTC.IsActive=1 AND tblTC.IsDeleted =0
AND tblTC.RoleId =@WashRole  
AND tblJ.JobType=@WashId 
  AND (tblTC.EventDate>=@FromDate AND tblTC.EventDate<=@ToDate) 


--WashesCount
DROP TABLE  IF EXISTS #WashesCount
(SELECT 
	tblj.LocationId,COUNT(*) WashesCount
	INTO #WashesCount
	FROM #tblJob tblj 
	inner join #tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId)	
	inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	WHERE tblj.JobType=@WashId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	GROUP BY tblj.LocationId)

	--WashesCount
DROP TABLE  IF EXISTS #NonMembershipWashesCount
(SELECT 
	tblj.LocationId,COUNT(*) NonMembershipWashesCount
	INTO #NonMembershipWashesCount
	FROM #tblJob tblj 
	inner join #tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId)	
	inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	inner join tblJobPaymentDetail tbljpd on(tblj.JobPaymentId = tbljpd.JobPaymentId)
	inner join #PaymentType pt on(pt.id = tbljpd.PaymentType)
	WHERE tblj.JobType=@WashId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND pt.CodeValue != 'Membership'
	GROUP BY tblj.LocationId)

	--DetailCount
DROP TABLE  IF EXISTS #DetailCount
(SELECT 
	tblj.LocationId,COUNT(*) DetailCount
	INTO #DetailCount
	FROM #tblJob tblj 
	inner join #tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	WHERE 
	tblj.JobType=@DetailId
	AND tbls.ServiceType=@DetailServiceId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tblj.JobStatus=@CompletedJobStatus
    
	GROUP BY tblj.LocationId )
	
	--Wash Employees

DROP TABLE  IF EXISTS #EmployeeCount
(SELECT
LocationId,COUNT(DISTINCT EmployeeId) EmployeeCount
INTO #EmployeeCount
FROM tblTimeClock 
WHERE RoleId=@WashRole
AND (EventDate>=@FromDate AND EventDate<=@ToDate) 
AND LocationId IN (select Id from #LocationIds) AND
IsActive=1 and IsDeleted=0
GROUP BY LocationId)
	
	--Score 

DROP TABLE  IF EXISTS #TotalCarWashed
(SELECT tblj.LocationId,Count(distinct tblj.JobId) TotalCarWashed 
INTO #TotalCarWashed 
FROM #tblJob tblj 
inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join #tblService tbls on(tblji.ServiceId=tbls.ServiceId) 
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.JobStatus=@CompletedJobStatus 
and tbljp.PaymentStatus=@CompletedPaymentStatus
GROUP BY tblj.LocationId)


/*
tblTimeClock tbltc
 INNER JOIN tblRoleMaster tblRM  
ON  tblRM.RoleMasterId=tblTC.RoleId  
inner join tbllocation tbll on(tbll.LocationId =tbltc.LocationId)
WHERE (tbltc.EventDate >=@FromDate AND tbltc.EventDate<=@ToDate)   AND RoleName = 'Washer'
GROUP BY tbll.LocationId)*/

--Location Open/Closed status
DROP TABLE IF EXISTS #StoreStatus

Select cr.locationId, cr.CashRegisterId, 
cr.StoreTimeIn,
cr.StoreTimeOut,
cv.CodeValue as StoreStatus into #StoreStatus
from tblCashRegister cr
LEFT JOIN tblCodeValue cv on cv.id = cr.StoreOpenCloseStatus 
WHERE cr.CashRegisterDate = @FromDate and cr.CashRegisterType = @CashRegisterType
and ISNULL(cr.IsDeleted,0) = 0

	--Average Car wash Time
	--select * from #StoreStatus
DROP TABLE  IF EXISTS #WashTime
(SELECT tbll.Id AS LocationId,
CASE
	   WHEN ISNULL(wt.Washer,0) = 0 THEN 0
	   WHEN (ss.StoreStatus = 'Open'  AND ss.StoreTimeIn > @CurrentDate) THEN  0
	   WHEN ((ss.StoreStatus = 'Closed' OR ss.StoreStatus = 'Closed for Weather') AND ss.StoreTimeOut <= @CurrentDate) THEN 0
	   WHEN ss.StoreStatus IS NULL AND ss.StoreTimeOut IS NULL THEN 0
	   --WHEN car.CarCount = 0 THEN 0
	   WHEN ISNULL(wt.Washer,0) <=3 AND car.CarCount <=1 THEN 25
	   WHEN ISNULL(wt.Washer,0) <=3 AND car.CarCount >1  THEN (25+(car.CarCount - 1)*8) + (((car.CarCount - 1)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) <=6 AND car.CarCount <=1 THEN 25
	   WHEN ISNULL(wt.Washer,0) <=6 AND car.CarCount >1  THEN (25+(car.CarCount - 1)*7) + (((car.CarCount - 1)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) <=9 AND car.CarCount <=1 THEN 25
	   WHEN ISNULL(wt.Washer,0) <=9 AND car.CarCount >1  THEN (25+(car.CarCount - 1)*6) + (((car.CarCount - 1)*+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) <=11 AND car.CarCount <=3 THEN 25
	   WHEN ISNULL(wt.Washer,0) <=11 AND car.CarCount >3  THEN (25+(car.CarCount - 3)*5) + (((car.CarCount - 3)*+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) >=12 AND ISNULL(wt.Washer,0)<=15 AND car.CarCount <=5 THEN 25
	   WHEN ISNULL(wt.Washer,0) >=12 AND ISNULL(wt.Washer,0)<=15 AND car.CarCount >5  THEN (25+(car.CarCount - 5)*3) + (((car.CarCount - 5)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) >=16 AND ISNULL(wt.Washer,0)<=21 AND car.CarCount <=5 THEN 25
	   WHEN ISNULL(wt.Washer,0) >=16 AND ISNULL(wt.Washer,0)<=21 AND car.CarCount >5  THEN (25+(car.CarCount - 5)*2) + (((car.CarCount - 5)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) >=22 AND ISNULL(wt.Washer,0)<=26 AND car.CarCount <=5 THEN 25
	   WHEN ISNULL(wt.Washer,0) >=22 AND ISNULL(wt.Washer,0)<=26 AND car.CarCount >5 THEN (25+(car.CarCount - 5)*2) + (((car.CarCount - 5)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN ISNULL(wt.Washer,0) >26 AND car.CarCount <=7 THEN 25
	   WHEN ISNULL(wt.Washer,0) >26 AND car.CarCount >7   THEN (25+(car.CarCount - 7)*2) + (((car.CarCount - 7)*ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   ELSE 25
	   END AS WashTimeMinutes,wt.Washer,car.CarCount
	   INTO #WashTime
	   FROM #LocationIds tbll
LEFT JOIN #WashRoleCount wt ON(tbll.Id = wt.LocationId)
LEFT JOIN #CarsCount car ON car.LocationId = tbll.id
LEFT JOIN tblLocationOffSet tbllo ON(tbll.Id = tbllo.LocationId) and tbllo.IsActive = 1 AND
tbllo.isDeleted = 0  
LEFT JOIN #StoreStatus ss ON ss.LocationId = tbll.id
--LEFT JOIN #EventDateForLocation edfl ON(tbll.Id = edfl.LocationId)
--WHERE 
	--(edfl.EventDate>=@FromDate AND edfl.EventDate<=@ToDate) 

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
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join GetTable('ServiceType') serviceType on serviceType.valueid = tbls.ServiceType
LEFT JOIN tblLocationOffSet tbllo ON(tblj.LocationId = tbllo.LocationId) and tbllo.IsActive = 1 AND
	tbllo.isDeleted = 0
LEFT JOIN #StoreStatus ss ON ss.LocationId = tblj.LocationId
INNER join GetTable('JobStatus') GT on GT.valueid = tblj.JobStatus and GT.valuedesc = 'In Progress' and tblj.JobType = @WashId
AND tblj.JobDate >=@FromDate AND tblj.JobDate <=@ToDate AND ISNULL(tblj.IsDeleted,0) = 0
WHERE serviceType.valuedesc like '%Upcharge%'  AND ISNULL(tblji.IsDeleted,0) != 1
GROUP BY tbllo.LocationId
	)
	
	DROP TABLE  IF EXISTS #AvgWashTime
(SELECT tbll.Id AS LocationId,
CASE
	   WHEN ISNULL(wt.Washer,0) = 0 THEN 0
	   WHEN (ss.StoreStatus = 'Open'  AND ss.StoreTimeIn > @CurrentDate) THEN  0
	   WHEN ((ss.StoreStatus = 'Closed' OR ss.StoreStatus = 'Closed for Weather') AND ss.StoreTimeOut <= @CurrentDate) THEN 0
	   ELSE ISNULL(wt.WashTimeMinutes,0) + ISNULL(ws.WashTimeMinutes,0)
	   END AS WashTimeMinutes
	   INTO #AvgWashTime
	   FROM #LocationIds tbll
LEFT JOIN #WashServices ws ON(tbll.Id = ws.LocationId)
LEFT JOIN #WashTime wt ON wt.LocationId = tbll.id
LEFT JOIN tblLocationOffSet tbllo ON(tbll.Id = tbllo.LocationId) and tbllo.IsActive = 1 AND
tbllo.isDeleted = 0  
LEFT JOIN #StoreStatus ss ON ss.LocationId = tbll.id
 

	)

	--Forecast
	/*
DROP TABLE  IF EXISTS #ForecastedCar
(SELECT tblj.LocationId,COUNT(DISTINCT tblj.JobId)/3 ForecastedCar 
into #ForecastedCar
FROM #tblJobForeCasted tblj 
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
WHERE tblj.JobType = @WashId
and tbljp.PaymentStatus=@CompletedPaymentStatus
AND tblj.JobStatus=@CompletedJobStatus
AND tblj.LocationId IN (Select ID from #LocationIds)
and tbljp.JobPaymentId = tblj.JobPaymentId
GROUP BY tblj.LocationId)
*/
DROP TABLE IF EXISTS #ForecastedCar
/*
Select (COUNT(DISTINCT j.JobId)) /3 as ForecastedCar,j.LocationId
into #ForecastedCar 
 FROM #LocationIds tbll
 JOIN tblJob j ON tbll.id = j.LocationId
INNER JOIN GetTable('JobType') JT on JT.valueid = j.JobType
INNER join GetTable('JobStatus') GT on GT.valueId = j.JobStatus and GT.valuedesc = 'Completed'
WHERE j.JobDate IN (@lastweek,@lastMonth,@lastThirdMonth) 
GROUP BY LocationId
*/
SELECT 
	tblj.LocationId,(COUNT(DISTINCT tblj.JobId))/3 ForecastedCar
	INTO #ForecastedCar
	FROM  #LocationIds tbll
	JOIN tblJob tblj ON tbll.id = tblj.LocationId
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)	
	inner join tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
	WHERE tblj.JobType=@WashId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND tblj.JobDate IN (@lastweek,@lastMonth,@lastThirdMonth) AND tblj.IsActive = 1 AND ISNULL(tblj.IsDeleted,0) = 0
	GROUP BY tblj.LocationId
	--Current

DROP TABLE  IF EXISTS #Current
(SELECT
    tblj.LocationId,COUNT(tblj.JobId) Currents
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


DROP TABLE  IF EXISTS #UpchargeSales
(SELECT distinct tblj.JobId,tblj.LocationId, SUM(tblji.Price) UpchargeSales
,tblj.JobType
into #UpchargeSales FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
inner join GetTable('ServiceType') serviceType on serviceType.valueid = tbls.ServiceType
--LEFT JOIN tblJobPaymentDetail tbljpd ON	tbljp.JobPaymentId = tbljpd.JobPaymentId 
--LEFT JOIN #PaymentType tblpt on	tbljpd.PaymentType = tblpt.id
WHERE valuedesc like '%Upcharge%' and tbljp.PaymentStatus=@CompletedPaymentStatus
AND tblj.JobStatus=@CompletedJobStatus 
--AND tblpt.CodeValue in ('Cash','Card')
GROUP BY tblj.JobId,tblj.LocationId,tblj.JobType)


DROP TABLE  IF EXISTS #WashSales
(SELECT tblj.LocationId,(SUM(tblji.Price) + sum(isnull(us.UpchargeSales,0))
) WashSales into #WashSales FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
JOIN tblJobPaymentDetail tbljpd ON	tbljp.JobPaymentId = tbljpd.JobPaymentId 
JOIN #PaymentType tblpt on	tbljpd.PaymentType = tblpt.id
LEFT JOIN #UpchargeSales us on us.JobType = tblj.JobType AND us.JobId = tblj.JobId 
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tbljp.PaymentStatus=@CompletedPaymentStatus
AND tblj.JobStatus=@CompletedJobStatus 
AND tblpt.CodeValue != 'Membership'
GROUP BY tblj.LocationId)


/*
DROP TABLE  IF EXISTS #UpchargeSales
(SELECT tblj.LocationId, SUM(tblji.Price) UpchargeSales into #UpchargeSales FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
inner join GetTable('ServiceType') serviceType on serviceType.valueid = tbls.ServiceType
--LEFT JOIN tblJobPaymentDetail tbljpd ON	tbljp.JobPaymentId = tbljpd.JobPaymentId 
--LEFT JOIN #PaymentType tblpt on	tbljpd.PaymentType = tblpt.id
WHERE valuedesc like '%Upcharge%' and tbljp.PaymentStatus=@CompletedPaymentStatus
AND tblj.JobStatus=@CompletedJobStatus 
--AND tblpt.CodeValue in ('Cash','Card')
GROUP BY tblj.LocationId)
*/
/*
DROP TABLE  IF EXISTS #TotalWashSales
(SELECT tblj.LocationId,SUM(tblji.Price)WashSales into #TotalWashSales FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tbljp.PaymentStatus=@CompletedPaymentStatus
AND tblj.JobStatus=@CompletedJobStatus 
GROUP BY tblj.LocationId)
*/


DROP TABLE  IF EXISTS #FinalUpchargeSales
(SELECT LocationId,sum(isnull(UpchargeSales,0)) UpchargeSales into #FinalUpchargeSales FROM 
  #UpchargeSales 
  where JobType = @DetailId
GROUP BY LocationId)

DROP TABLE  IF EXISTS #DetailSales
(SELECT tblj.LocationId,(SUM(ISNULL(tblji.Price,0))) DetailSales into #DetailSales FROM #tblJob tblj 
inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
--LEFT JOIN #FinalUpchargeSales us on us.LocationId = tblj.LocationId
WHERE tblj.JobType=@DetailId and tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType=@DetailServiceId
AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #FinalDetailSales
SELECT tblj.LocationId,(SUM(ISNULL(tblj.DetailSales,0))+ SUM(ISNULL(us.UpchargeSales,0))) DetailSales into #FinalDetailSales 
FROM #DetailSales tblj 
left JOIN #FinalUpchargeSales us on us.LocationId = tblj.LocationId
group by tblj.LocationId


DROP TABLE  IF EXISTS #ExtraService
(SELECT tblj.LocationId,SUM(isnull(tblji.Price,0))ExtraService 
into #ExtraService 
FROM #tblJob tblj 
inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
WHERE tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus 
AND tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType IN (@ServiceType ,@AirFresheners)
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #MembershipExtraService
SELECT LocationId,SUM(isnull(Price,0)) Price
into #MembershipExtraService 
FROM (
SELECT DISTINCT tblj.LocationId,tblj.JobId,tblji.jobitemid, tblji.Price
FROM #tblJob tblj 
inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
join [tblClientVehicle] cv oN cv.ClientId = tblj.ClientId
JOIN[tblClientVehicleMembershipDetails] cvmd ON cv.VehicleId = cvmd.ClientVehicleId
JOIN tblClientVehicleMembershipService vms on vms.ClientMembershipId = cvmd.ClientMembershipId and  tblji.ServiceId = vms.ServiceId
join #tblService tbls on(vms.ServiceId = tbls.ServiceId  ) 
 where tblJP.JobPaymentId in (
select tbljpd.JobPaymentId from tblJobPaymentDetail tbljpd
join  #PaymentType tblpt on tbljpd.PaymentType = tblpt.id
join #tblJob tblj ON tblj.JobPaymentId = tbljpd.JobPaymentId
AND tblpt.CodeValue in ('Membership')
) 
AND tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus 
AND tbljp.PaymentStatus=@CompletedPaymentStatus 
AND tbls.ServiceType IN (@ServiceType ,@AirFresheners)
)A
GROUP BY LocationId

DROP TABLE  IF EXISTS #FinalExtraService
(SELECT es.LocationId,(ISNULL(es.ExtraService,0) - ISNULL(mes.Price,0)) ExtraService 
into #FinalExtraService 
FROM #ExtraService es
LEFT JOIN #MembershipExtraService mes ON es.LocationId = mes.LocationId
--GROUP BY es.LocationId
)


--DROP TABLE  IF EXISTS #FinalExtraService
--(SELECT LocationId,SUM(Price) ExtraService 
--into #FinalExtraService 
--FROM #MembershipExtraService
--GROUP BY LocationId)

--MerchandizeSales
DROP TABLE  IF EXISTS #MerchandizeSales
(SELECT tblj.LocationId,SUM((isnull(tbljpi.Price,0))* ISNULL(tbljpi.Quantity,0))
+ SUM((ISNULL(tbljpi.Price,0) * ISNULL(tbljpi.Quantity,0))*((ISNULL(tbp.TaxAmount,0)/100))) as MerchandizeSales 
into #MerchandizeSales 
FROM #tblJob tblj 
inner join tblJobProductItem tbljpi on(tblj.JobId = tbljpi.JobId) 
inner join tblProduct tbp on(tbljpi.ProductId = tbp.ProductId)
INNER JOIN GetTable('ProductType') pt on pt.valueid = tbp.ProductType 
inner join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
--JOIN  tblJobProductItem tbljbP  ON  tblj.JobId = tbljbP.JobId 
--JOIN  tblProduct tblp  ON  tblp.ProductId=tbljbP.ProductId  
WHERE tbljp.PaymentStatus=@CompletedPaymentStatus and tbljpi.IsActive=1 
AND ISNULL(tbljpi.IsDeleted,0)=0 AND tblj.JobStatus=@CompletedJobStatus
--and pt.valuedesc ='Merchandize'
GROUP BY tblj.LocationId)
/*
DROP TABLE  IF EXISTS #MembershipClientSales

(
select cv.LocationId, SUM(ISNULL(cv.MonthlyCharge,0)) MembershipClientSales
INTO #MembershipClientSales
from [tblClientVehicle] cv
JOIN	[tblClientVehicleMembershipDetails] cvmd ON cv.VehicleId = cvmd.ClientVehicleId
WHERE ISNULL(cv.IsDeleted,0)=0 AND cv.IsActive=1
AND ISNULL(cvmd.IsDeleted,0)=0 AND cvmd.IsActive=1
AND cv.LocationId in (Select ID from #LocationIds)
AND cvmd.UpdatedDate between @FromDate and @ToDate
group by cv.LocationId
)
*/
DROP TABLE  IF EXISTS #MonthlyClientSales
(SELECT tblj.LocationId,SUM(isnull(tbljpd.Amount,0)) MonthlyClientSales 
into #MonthlyClientSales 
FROM #tblJob tblj 
--inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
--inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
--inner join tblClientVehicleMembershipDetails tblcvmd on(tblj.VehicleId = tblcvmd.ClientVehicleId)
INNER join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
INNER JOIN tblJobPaymentDetail tbljpd ON tbljp.JobPaymentId = tbljpd.JobPaymentId 
INNER JOIN #PaymentType tblpt on tbljpd.PaymentType = tblpt.id
WHERE tblj.JobType=@WashId 
	AND tblj.LocationId IN (select Id from #LocationIds) 
	AND tblj.JobStatus=@CompletedJobStatus
	AND tblpt.CodeValue in ('Membership')
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #MembershipWashSales
(SELECT tblj.LocationId,SUM(isnull(tblji.price,0)) MembershipWashSales
into #MembershipWashSales
FROM #tblJob tblj
inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
--inner join tblClientVehicleMembershipDetails tblcvmd on(tblj.VehicleId = tblcvmd.ClientVehicleId)
INNER join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
INNER JOIN tblJobPaymentDetail tbljpd ON tbljp.JobPaymentId = tbljpd.JobPaymentId
INNER JOIN #PaymentType tblpt on tbljpd.PaymentType = tblpt.id
WHERE tblj.JobType=@WashId
        AND tblj.LocationId IN (select Id from #LocationIds)
        AND tblj.JobStatus=@CompletedJobStatus
        AND tblpt.CodeValue in ('Membership')
        --and tbls.ServiceType=@WashServiceId
        and (tbls.ServiceType=@WashServiceId OR LOWER(tbls.ServiceName) LIKE '%upcharge%')
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForWash
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForWash
into #TotalCarCountForWash 
FROM #tblJob tblj 
inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId)
INNER join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
--AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetail
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForDetail into #TotalCarCountForDetail FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId)
INNER join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
WHERE tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAdditionalService
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAdditionalService into #TotalCarCountForAdditionalService FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId)
INNER join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId 
GROUP BY tblj.LocationId)
/*
DROP TABLE  IF EXISTS #TotalCarCountForAllService
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAllService into #TotalCarCountForAllService FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId)
INNER join #tblJobPayment tbljp on(tblj.JobPaymentId = tblJP.JobPaymentId)
WHERE tblj.JobType in(@WashId,@DetailId)
GROUP BY tblj.LocationId)*/

DROP TABLE  IF EXISTS #AdditionalServiceSales
(SELECT tblj.LocationId,SUM(tblji.Price)AdditionalServiceSales into #AdditionalServiceSales FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join #tblJobPayment tblJP on(tblj.JobPaymentId = tblJP.JobPaymentId)
JOIN  GetTable('ServiceType') st ON st.valueid = tbls.ServiceType
WHERE tblj.JobType in(@WashId,@DetailId) --and tbls.ServiceType=@AdditionalServiceId 
AND st.valuedesc not in ('Service Discounts','Gift Certificate','Gift Card','Product Options')
AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)

/*
DROP TABLE  IF EXISTS #TotalServiceSales
(SELECT tblj.LocationId,SUM(tblji.Price)TotalServiceSales INTO #TotalServiceSales FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join #tblJobPayment tblJP on(tblj.JobPaymentId = tblJP.JobPaymentId)
WHERE tblj.JobType in(@WashId,@DetailId) 
and tbljp.PaymentStatus=@CompletedPaymentStatus
AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)
*/

DROP TABLE  IF EXISTS #LabourCostForWashAndAdditionalService
(SELECT tblj.LocationId,SUM(tblji.Price)LabourCostForWashAndAdditionalService INTO #LabourCostForWashAndAdditionalService FROM #tblJob tblj inner join #tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
JOIN  GetTable('ServiceType') st ON st.valueid = tbls.ServiceType
WHERE tblj.JobType in(@WashId,@DetailId) --and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) 
AND st.valuedesc not in ('Service Discounts','Gift Certificate','Outside Services','Gift Card','Product Options')
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalCarCountForWashAndAdditionalService
(SELECT tblj.LocationId,COUNT(*)TotalCarCountForWashAndAdditionalService into #TotalCarCountForWashAndAdditionalService 
FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in (@WashServiceId,@AdditionalServiceId)
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForDetailService
(SELECT tblj.LocationId,SUM(tblji.Price)LabourCostForDetailService into #LabourCostForDetailService FROM #tblJob tblj inner join #tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join #tblService tblS on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType =@DetailId and tbls.ServiceType= @DetailServiceId
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetailService
(SELECT tblj.LocationId,COUNT(*)TotalCarCountForDetailService into #TotalCarCountForDetailService FROM #tblJob tblj inner join #tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join #tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
WHERE tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId 
GROUP BY tblj.LocationId)


DROP TABLE IF EXISTS #ServiceSales
SELECT tbl.LocationId,
ISNULL(ws.WashSales,0.00) WashSales,
ISNULL(ds.DetailSales,0.00) DetailSales,
ISNULL(es.ExtraService,0.00) ExtraService,
ISNULL(ms.MerchandizeSales,0) MerchandizeSales,
SUM(ISNULL(ws.WashSales,0.00) 
+ ISNULL(ds.DetailSales,0.00) 
+ ISNULL(es.ExtraService,0.00) 
+ ISNULL(ms.MerchandizeSales,0.00)
- ISNULL(mws.MembershipWashSales,0.00)
--+ ISNULL(us.UpchargeSales,0.00)
) SumOfWashDetailMerchandizeSales
into #ServiceSales
FROM tblLocation tbl 
LEFT JOIN #WashSales ws ON(tbl.LocationId = ws.LocationId) 
LEFT JOIN #FinalDetailSales ds ON(tbl.LocationId = ds.LocationId)
LEFT JOIN #FinalExtraService es ON(tbl.LocationId=es.LocationId) 
LEFT JOIN #MerchandizeSales ms ON(tbl.LocationId=ms.LocationId)
LEFT JOIN #MembershipWashSales mws on (tbl.LocationId=mws.LocationId)
--LEFT JOIN #UpchargeSales us ON(tbl.LocationId=us.LocationId)

GROUP BY tbl.LocationId,WashSales,DetailSales,ExtraService,MerchandizeSales


/*
DROP TABLE IF EXISTS #TotalSales
SELECT tbl.LocationId,
SUM(tji.Price) TotalSales
INTO #TotalSales
FROM tblLocation tbl 
JOIN #MerchandizeSales ms on ms.LocationId = tbl.LocationId  
JOIN #tblJob tj On tj.LocationId = tbl.LocationId
JOIN #tblJobItem tji ON (tji.JobId = tj.JobId) 
JOIN #tblJobPayment tp ON(tp.JobPaymentId = tj.JobPaymentId)
where tj.JobStatus=@CompletedJobStatus
and tp.PaymentStatus=@CompletedPaymentStatus
GROUP BY tbl.LocationId
*/
--TimeClock Details  
DROP TABLE IF EXISTS #TimeClock  
  
SELECT   
   LocationId  
 , TimeClockId  
 ,tblTC.EmployeeId
 , EventDate  
 , tblRM.RoleName   
 --,Replace(CONVERT(VARCHAR(5),DATEADD(minute, DATEDIFF(MI, ISNULL(InTime,OutTime), ISNULL(OutTime,GETDATE())), 0), 114),':','.')  AS TotH  
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
WHERE  LocationId IN (Select ID from #LocationIds) 
AND EventDate BETWEEN @FromDate AND @ToDate  AND RoleName != 'Detailer'
AND  ISNULL(tblTC.IsDeleted,0) = 0  


  
  --select * from #TimeClock

DROP TABLE  IF EXISTS #TotalHoursWashed
SELECT LocationId,CAST(TotalWashHours/60.00 AS DECIMAL(18,6)) --CAST((TotalWashHours/60) AS varchar(18))+'.'+CAST((TotalWashHours%60) AS varchar(18)) 
TotalHoursWashed
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
WHERE  ehr.LocationId in (Select ID from #LocationIds)  and ehr.IsActive = 1 and ehr.IsDeleted = 0  

--Detail Rate  
  
DROP TABLE IF EXISTS #DetailCommission  
  
Select SUM(jse.CommissionAmount)  as CommissionAmount ,tc.LocationId
INTO #DetailCommission 
from tblJobServiceEmployee jse  
INNER JOIN tblTimeClock tc on tc.EmployeeId = jse.EmployeeId   
inner join #tblJobItem JI on jse.JobItemId = JI.JobItemId
--INNER JOIN #tblJob jb on jb.JobId = JI.JobId
JOIN tblRoleMaster tblRM  ON  tblRM.RoleMasterId=tc.RoleId 
where (tc.EventDate BETWEEN @FromDate AND @ToDate) --AND (jse.CreatedDate BETWEEN @FromDate AND @ToDate)  
and RoleName = 'Detailer' and tc.LocationId IN (Select ID from #LocationIds) 
GROUP BY tc.LocationId 

-- Rate Summary  
DROP TABLE IF EXISTS #EmployeeRate  
  
SELECT   
 tbll.LocationId,  
 SUM((TotalWashHours/60.00) * r.WashRate) TotalWashRate
 
INTO  
 #EmployeeRate  
FROM  
(  
SELECT    
 LocationId,  
 EmployeeId,
 SUM(isnull(TotalMinutes,0)) TotalWashHours
FROM #TimeClock  
WHERE RoleName='Washer'
GROUP BY LocationId,  
 EmployeeId
) TOLHours  
LEFT JOIN  
 tblLocation tbll ON tbll.LocationId=TOLHours.locationId  
left join #Rate r on r.EmployeeId = TOLHours.EmployeeId AND r.LocationId=TOLHours.locationId  
WHERE tbll.LocationId IN (Select ID from #LocationIds)
GROUP BY tbll.LocationId,ISNULL(Tbll.WorkhourThreshold,0)  

DROP TABLE IF EXISTS #tblWeatherPrediction 
SELECT tbll.id LocationId, TargetBusiness 
INTO #tblWeatherPrediction
FROM #LocationIds tbll 
LEFT JOIN [tblWeatherPrediction] wp ON wp.LocationId = tbll.id
WHERE CAST( CreatedDate AS DATE) = @FromDate


SELECT 
distinct
ISNULL(tbl.LocationId,0) LocationId,
tbl.LocationName,
ISNULL(wc.WashesCount,0) WashesCount,
ISNULL(dc.DetailCount,0) DetailCount,
ISNULL(ec.EmployeeCount,0) EmployeeCount,
CASE WHEN thw.TotalHoursWashed !='0.0' THEN CAST(((ISNULL(tcw.TotalCarWashed,0)/CAST(thw.TotalHoursWashed AS DECIMAL(18,2)))*100)as decimal(9,2)) ELSE 0
END AS Score,
CONVERT(decimal(18,0), wt.WashTimeMinutes) WashTime,
ISNULL(cu.Currents,0) Currents,
CASE WHEN ISNULL(wp.TargetBusiness,0) = 0 THEN  ISNULL(fc.ForecastedCar,0) ELSE wp.TargetBusiness END ForecastedCar,
CASE WHEN ISNULL(ws.WashSales,0) = 0 THEN 0 ELSE (ISNULL(ws.WashSales,0.00)) END WashSales,
ISNULL(ss.DetailSales,0.00) DetailSales,
ISNULL(ss.ExtraService,0.00) ExtraServiceSales,
ISNULL(ss.MerchandizeSales,0) MerchandizeSales,
ss.SumOfWashDetailMerchandizeSales  AS TotalSales,
ISNULL(mcs.MonthlyClientSales,0) MonthlyClientSales,
CASE WHEN ISNULL(ws.WashSales,0) = 0 THEN 0 ELSE CAST(ISNULL(ws.WashSales,0.00)/nmwc.NonMembershipWashesCount AS Decimal(9,2)) END  AverageWashPerCar,
ISNULL(CAST((ss.DetailSales/tccfd.TotalCarCountForDetail)AS Decimal(9,2)),0) AverageDetailPerCar,
ISNULL(CAST((ss.ExtraService/tccfas.TotalCarCountForAdditionalService) AS DECIMAL(9,2)),0) AverageExtraServicePerCar,
CASE WHEN (ISNULL(wc.WashesCount,0) + ISNULL(dc.DetailCount,0)) = 0 THEN 0 ELSE ISNULL(CAST((ss.SumOfWashDetailMerchandizeSales/(ISNULL(wc.WashesCount,0) + ISNULL(dc.DetailCount,0)))AS DECIMAL(9,2)),0) END AverageTotalPerCar,
ISNULL(CAST((er.TotalWashRate/wc.WashesCount) AS DECIMAL(9,2)),0) LabourCostPerCarMinusDetail,
ISNULL(CAST((LDC.CommissionAmount/dc.DetailCount)AS DECIMAL(9,2)),0)  DetailCostPerCar
FROM tblLocation tbl 
LEFT JOIN #WashesCount wc on(tbl.LocationId = wc.LocationId)
LEFT JOIN #NonMembershipWashesCount nmwc on(tbl.LocationId = nmwc.LocationId)
LEFT JOIN #DetailCount dc on(tbl.LocationId = dc.LocationId)
LEFT JOIN #EmployeeCount ec on(tbl.LocationId = ec.LocationId)
LEFT JOIN #TotalCarWashed tcw on(tbl.LocationId = tcw.LocationId)
LEFT JOIN #TotalHoursWashed thw on(tbl.LocationId = thw.LocationId)
LEFT JOIN #AvgWashTime wt on(tbl.LocationId = wt.LocationId)
LEFT JOIN #ForecastedCar fc ON(tbl.LocationId = fc.LocationId)
LEFT JOIN #Current cu on(tbl.LocationId = cu.LocationId)
LEFT JOIN #ServiceSales ss on(tbl.LocationId = ss.LocationId)
LEFT JOIN #MonthlyClientSales mcs on(tbl.LocationId = mcs.LocationId)
LEFT JOIN #TotalCarCountForWash tccfw on(tbl.LocationId = tccfw.LocationId)
LEFT JOIN #TotalCarCountForDetail tccfd on(tbl.LocationId = tccfd.LocationId)
LEFT JOIN #TotalCarCountForAdditionalService tccfas on(tbl.LocationId = tccfas.LocationId)
--LEFT JOIN #TotalCarCountForAllService tccfals on(tbl.LocationId = tccfals.LocationId)
LEFT JOIN #AdditionalServiceSales ass on(tbl.LocationId = ass.LocationId)
--LEFT JOIN #TotalServiceSales tss on(tbl.LocationId = tss.LocationId)
LEFT JOIN #LabourCostForWashAndAdditionalService lcfwaas on(tbl.LocationId = lcfwaas.LocationId)
--LEFT JOIN #TotalCarCountForWashAndAdditionalService tccfwaas on(tbl.LocationId = tccfwaas.LocationId)
LEFT JOIN #LabourCostForDetailService lcfds on(tbl.LocationId = lcfds.LocationId)
LEFT JOIN #TotalCarCountForDetailService tccfds on(tbl.LocationId = tccfds.LocationId)
LEFT JOIN #WashSales ws on(tbl.LocationId = ws.LocationId)
LEFT JOIN #EmployeeRate er on(tbl.LocationId = er.LocationId)
LEFT JOIN #DetailCommission LDC on(tbl.LocationId = LDC.LocationId)
LEFT JOIN #MembershipWashSales MWS ON MWS.LocationId = tbl.LocationId
LEFT JOIN #tblWeatherPrediction wp on(tbl.LocationId = wp.LocationId)
WHERE tbl.IsActive=1 and ISNULL(tbl.IsDeleted,0)=0 AND tbl.LocationId IN (Select ID from #LocationIds)

END
GO

