-- =============================================
-- Author:		Vineeth B
-- Create date: 03-11-2020
-- Description:	To get Dashboard Details
--  --
/*

[StriveCarSalon].[uspGetDashboardStatistics] 1,'2021-06-10','2021-06-10'

*/
-- =============================================
----------History------------

-- =============================================
/*
1.june-10-2021 -- added jobdate filter in ##WashRoleCount
*/ 
-- =============================================



CREATE PROCEDURE [StriveCarSalon].[uspGetDashboardStatistics]
(@LocationId INT,@FromDate Date,@ToDate Date)
AS
BEGIN
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


DROP TABLE IF EXISTS #WashRoleCount
SELECT	  tblL.LocationId,
		  COUNT_BIG (DISTINCT tblTC.EmployeeId) Washer,
		  COUNT_BIG (DISTINCT tblJ.JobId) CarCount 
INTO #WashRoleCount 
FROM	  tblTimeClock tblTC 
INNER JOIN    tblLocation tblL ON (tblTC.LocationId = tblL.LocationId) 
INNER JOIN    tblJob tblJ ON (tblJ.LocationId = tblL.LocationId) 
WHERE
  tblL.IsActive = 1 
  AND (tblTC.EventDate>=@FromDate AND tblTC.EventDate<=@ToDate)   
  AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
  AND ISNULL(tblL.IsDeleted, 0) = 0 
  AND tblTC.IsActive = 1 
  AND ISNULL(tblTC.IsDeleted, 0) = 0 
  AND tblJ.IsActive = 1 
  AND ISNULL(tblJ.IsDeleted, 0) = 0 
  AND tblTC.RoleId = @WashRole 
  AND tblJ.JobType = @WashId 
GROUP BY tblL.LocationId


DROP TABLE IF EXISTS #EventDateForLocation
SELECT tblL.LocationId,tblTC.EventDate
INTO #EventDateForLocation 
FROM tblTimeClock tblTC INNER JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
INNER JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole  
AND tblJ.JobType=@WashId 
  AND (tblTC.EventDate>=@FromDate AND tblTC.EventDate<=@ToDate) 

IF(@LocationId != 0)
BEGIN

--WashesCount
DROP TABLE  IF EXISTS #WashesCount
(SELECT 
	tblj.LocationId,COUNT(*) WashesCount
	INTO #WashesCount
	FROM tbljob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)	
	inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
	WHERE tblj.JobType=@WashId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId AND
    tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0
	and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0
	GROUP BY tblj.LocationId)

	--DetailCount
DROP TABLE  IF EXISTS #DetailCount
(SELECT 
	tblj.LocationId,COUNT(*) DetailCount
	INTO #DetailCount
	FROM tbljob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
	WHERE tblj.JobType=@DetailId
	AND tbls.ServiceType=@DetailServiceId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tblj.JobStatus=@CompletedJobStatus
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId AND
    tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0
	and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 
	GROUP BY tblj.LocationId )
	
	--Wash Employees

DROP TABLE  IF EXISTS #EmployeeCount
(SELECT
    LocationId,COUNT(*) EmployeeCount
	INTO #EmployeeCount
	FROM tblTimeClock 
	WHERE RoleId=@WashRole
    AND (EventDate>=@FromDate AND EventDate<=@ToDate) 
	AND LocationId=@LocationId AND
    IsActive=1 and ISNULL(IsDeleted,0)=0
	GROUP BY LocationId)
	
	--Score 

DROP TABLE  IF EXISTS #TotalCarWashed
(SELECT tblj.LocationId,Count(*) TotalCarWashed 
INTO #TotalCarWashed 
FROM tblJob tblj 
inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) 
inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
	
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.JobStatus=@CompletedJobStatus 
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	 GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalHoursWashed
(SELECT tblj.LocationId,SUM(CAST((CAST(DATEDIFF(MINUTE, TimeIn,ActualTimeOut)AS decimal(9,2))/60) AS decimal(9,2))) TotalHoursWashed
INTO #TotalHoursWashed 
FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) 
inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 
    AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tblj.JobStatus=@CompletedJobStatus
	GROUP BY tblj.LocationId)

	--Average Car wash Time


DROP TABLE  IF EXISTS #WashTime
(SELECT tbll.LocationId,
CASE
	   WHEN wt.Washer <=3 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=3 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*8) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer <=6 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=6 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*7) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer <=9 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=9 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*6) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer <=11 AND wt.CarCount <=3 THEN 25
	   WHEN wt.Washer <=11 AND wt.CarCount >3 THEN (25+(wt.CarCount - 3)*5) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*3) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount >5  THEN (25+(wt.CarCount - 6)*2) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*2) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >26 AND wt.CarCount <=7 THEN 25
	   WHEN wt.Washer >26 AND wt.CarCount >7  THEN (25+(wt.CarCount - 7)*2) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   ELSE 0
	   END AS WashTimeMinutes
	   INTO #WashTime
	   FROM tblLocation tbll
LEFT JOIN #WashRoleCount wt ON(tbll.LocationId = wt.LocationId)
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbllo.LocationId)
LEFT JOIN #EventDateForLocation edfl ON(tbll.LocationId = edfl.LocationId)
WHERE isnull(tbllo.IsActive,1) = 1 AND
isnull(tbllo.isDeleted,0) = 0  AND
tbll.LocationId = @LocationId
	AND (edfl.EventDate>=@FromDate AND edfl.EventDate<=@ToDate) 

	)

	--Forecast
DROP TABLE  IF EXISTS #ForecastedCar
(SELECT tblj.LocationId,COUNT(VehicleId) ForecastedCar 
into #ForecastedCar
	 FROM tblJob tblj 
	 inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) 
	 inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId)
	inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
	 WHERE tblj.JobType in(@WashId,@DetailId) 
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	 and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	AND tblj.JobStatus=@CompletedJobStatus
	GROUP BY tblj.LocationId)

	--Current

DROP TABLE  IF EXISTS #Current
(SELECT
    tblj.LocationId,COUNT(VehicleId) Currents
into #Current FROM tblJob tblj 
inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
	 WHERE tblj.JobType in(@WashId,@DetailId) 
	and tbljp.PaymentStatus=@CompletedPaymentStatus
	and tblj.JobStatus=@CompletedJobStatus 
	and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	AND tblj.JobStatus=@CompletedJobStatus
	 GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #WashSales
(SELECT tblj.LocationId,SUM(tblji.Price)WashSales into #WashSales FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tbljp.PaymentStatus=@CompletedPaymentStatus
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and tbljp.IsActive=1 and ISNULL(tbljp.IsDeleted,0)=0 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)



DROP TABLE  IF EXISTS #DetailSales
(SELECT tblj.LocationId,SUM(tblji.Price) DetailSales into #DetailSales FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId)
WHERE tblj.JobType=@DetailId and tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType=@DetailServiceId and 
tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and tbljp.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 
and ISNULL(tbls.IsDeleted,0) =0 and ISNULL(tbljp.IsDeleted,0)=0 AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #ExtraService
(SELECT tblj.LocationId,SUM(isnull(tblji.Price,0))ExtraService 
into #ExtraService 
FROM tblJob tblj 
inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId) 
WHERE tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus 
and tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType=@ServiceType 
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1
and tbljp.IsActive=1
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0
and ISNULL(tbljp.IsDeleted,0)=0
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
GROUP BY tblj.LocationId)

--MerchandizeSales
DROP TABLE  IF EXISTS #MerchandizeSales
(SELECT tblj.LocationId,SUM(isnull(tbljpi.Price,0))MerchandizeSales 
into #MerchandizeSales 
FROM tblJob tblj 
inner join tblJobProductItem tbljpi on(tblj.JobId = tbljpi.JobId) 
inner join tblProduct tbp on(tbljpi.ProductId = tbp.ProductId)
INNER JOIN GetTable('ProductType') pt on pt.valueid = tbp.ProductType and pt.valuedesc ='Merchandize'
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId)
WHERE tbljp.PaymentStatus=@CompletedPaymentStatus and tblj.IsActive=1 and tbljpi.IsActive=1 and tbljp.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tbljpi.IsDeleted,0)=0 and ISNULL(tbljp.IsDeleted,0)=0 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #MonthlyClientSales
(SELECT tblj.LocationId,SUM(isnull(tblcvmd.TotalPrice,0)) MonthlyClientSales 
into #MonthlyClientSales FROM tblJob tblj 
inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) inner join tblClientVehicleMembershipDetails tblcvmd
on(tblj.VehicleId = tblcvmd.ClientVehicleId)
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForWash
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForWash
into #TotalCarCountForWash 
FROM tblJob tblj 
inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	--AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetail
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForDetail into #TotalCarCountForDetail FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	--AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAdditionalService
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAdditionalService into #TotalCarCountForAdditionalService FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAllService
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAllService into #TotalCarCountForAllService FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType in(@WashId,@DetailId) and tblj.IsActive=1 and 
tblji.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #AdditionalServiceSales
(SELECT tblj.LocationId,SUM(tblji.Price)AdditionalServiceSales into #AdditionalServiceSales FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	AND tblj.JobStatus=@CompletedJobStatus
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalServiceSales
(SELECT tblj.LocationId,SUM(tblji.Price)TotalServiceSales INTO #TotalServiceSales FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tblJP on(tblj.JobId = tblJP.JobId)
WHERE tblj.JobType in(@WashId,@DetailId) and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
and tbljp.PaymentStatus=@CompletedPaymentStatus
	AND tblj.JobStatus=@CompletedJobStatus
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForWashAndAdditionalService
(SELECT tblj.LocationId,SUM(tblji.Price)LabourCostForWashAndAdditionalService INTO #LabourCostForWashAndAdditionalService FROM tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalCarCountForWashAndAdditionalService
(SELECT tblj.LocationId,COUNT(*)TotalCarCountForWashAndAdditionalService into #TotalCarCountForWashAndAdditionalService FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForDetailService
(SELECT tblj.LocationId,SUM(tblji.Price)LabourCostForDetailService into #LabourCostForDetailService FROM tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType =@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetailService
(SELECT tblj.LocationId,COUNT(*)TotalCarCountForDetailService into #TotalCarCountForDetailService FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
WHERE tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
GROUP BY tblj.LocationId)


DROP TABLE IF EXISTS #ServiceSales
SELECT 
distinct
tbl.LocationId,
ISNULL(ws.WashSales,0.00) WashSales,
ISNULL(ds.DetailSales,0.00) DetailSales,
ISNULL(es.ExtraService,0.00) ExtraService,
ISNULL(ms.MerchandizeSales,0) MerchandizeSales,
SUM(ISNULL(ws.WashSales,0.00) + ISNULL(ds.DetailSales,0.00) + ISNULL(es.ExtraService,0.00) + ISNULL(ms.MerchandizeSales,0.00)) SumOfWashDetailMerchandizeSales
into #ServiceSales
FROM tblLocation tbl LEFT JOIN #WashSales ws ON(tbl.LocationId = ws.LocationId) LEFT JOIN #DetailSales ds ON(tbl.LocationId = ds.LocationId)
LEFT JOIN #ExtraService es ON(tbl.LocationId=es.LocationId) LEFT JOIN #MerchandizeSales ms ON(tbl.LocationId=ms.LocationId)
GROUP BY tbl.LocationId,WashSales,DetailSales,ExtraService,MerchandizeSales

SELECT 
distinct
ISNULL(tbl.LocationId,0) LocationId,
tbl.LocationName,
ISNULL(wc.WashesCount,0) WashesCount,
ISNULL(dc.DetailCount,0) DetailCount,
ISNULL(ec.EmployeeCount,0) EmployeeCount,
CASE 
	       WHEN thw.TotalHoursWashed='0.00' THEN ISNULL(tcw.TotalCarWashed,0)
		   WHEN thw.TotalHoursWashed!='0.00' THEN CAST((ISNULL(tcw.TotalCarWashed,0)/thw.TotalHoursWashed)as decimal(9,2))
		   END AS Score ,
ISNULL( CONVERT(decimal(18,0), wt.WashTimeMinutes),25) WashTime,
ISNULL(cu.Currents,0) Currents,
ISNULL(fc.ForecastedCar,0)ForecastedCar,
ISNULL(ss.WashSales,0.00) WashSales,
ISNULL(ss.DetailSales,0.00) DetailSales,
ISNULL(ss.ExtraService,0.00) ExtraServiceSales,
ISNULL(ss.MerchandizeSales,0) MerchandizeSales,
ss.SumOfWashDetailMerchandizeSales AS TotalSales,
ISNULL(mcs.MonthlyClientSales,0) MonthlyClientSales,
ISNULL(CAST((ss.WashSales/tccfw.TotalCarCountForWash) AS Decimal(9,2)),0) AverageWashPerCar,
ISNULL(CAST((ss.DetailSales/tccfd.TotalCarCountForDetail)AS Decimal(9,2)),0) AverageDetailPerCar,
ISNULL(CAST((ass.AdditionalServiceSales/tccfas.TotalCarCountForAdditionalService) AS DECIMAL(9,2)),0) AverageExtraServicePerCar,
ISNULL(CAST((tss.TotalServiceSales/tccfals.TotalCarCountForAllService)AS DECIMAL(9,2)),0)  AverageTotalPerCar,
ISNULL(CAST((lcfwaas.LabourCostForWashAndAdditionalService/tccfwaas.TotalCarCountForWashAndAdditionalService) AS DECIMAL(9,2)),0) LabourCostPerCarMinusDetail,
ISNULL(CAST((lcfds.LabourCostForDetailService/tccfds.TotalCarCountForDetailService)AS DECIMAL(9,2)),0)  DetailCostPerCar
FROM tblLocation tbl 
LEFT JOIN #WashesCount wc on(tbl.LocationId = wc.LocationId)
LEFT JOIN #DetailCount dc on(tbl.LocationId = dc.LocationId)
LEFT JOIN #EmployeeCount ec on(tbl.LocationId = ec.LocationId)
LEFT JOIN #TotalCarWashed tcw on(tbl.LocationId = tcw.LocationId)
LEFT JOIN #TotalHoursWashed thw on(tbl.LocationId = thw.LocationId)
LEFT JOIN #WashTime wt on(tbl.LocationId = wt.LocationId)
LEFT JOIN #ForecastedCar fc ON(tbl.LocationId = fc.LocationId)
LEFT JOIN #Current cu on(tbl.LocationId = cu.LocationId)
LEFT JOIN #ServiceSales ss on(tbl.LocationId = ss.LocationId)
LEFT JOIN #MonthlyClientSales mcs on(tbl.LocationId = mcs.LocationId)
LEFT JOIN #TotalCarCountForWash tccfw on(tbl.LocationId = tccfw.LocationId)
LEFT JOIN #TotalCarCountForDetail tccfd on(tbl.LocationId = tccfd.LocationId)
LEFT JOIN #TotalCarCountForAdditionalService tccfas on(tbl.LocationId = tccfas.LocationId)
LEFT JOIN #TotalCarCountForAllService tccfals on(tbl.LocationId = tccfals.LocationId)
LEFT JOIN #AdditionalServiceSales ass on(tbl.LocationId = ass.LocationId)
LEFT JOIN #TotalServiceSales tss on(tbl.LocationId = tss.LocationId)
LEFT JOIN #LabourCostForWashAndAdditionalService lcfwaas on(tbl.LocationId = lcfwaas.LocationId)
LEFT JOIN #TotalCarCountForWashAndAdditionalService tccfwaas on(tbl.LocationId = tccfwaas.LocationId)
LEFT JOIN #LabourCostForDetailService lcfds on(tbl.LocationId = lcfds.LocationId)
LEFT JOIN #TotalCarCountForDetailService tccfds on(tbl.LocationId = tccfds.LocationId)
WHERE tbl.LocationId=@LocationId and tbl.IsActive=1 and ISNULL(tbl.IsDeleted,0)=0
END
ELSE
BEGIN
DROP TABLE  IF EXISTS #WashesCount1
(SELECT 
	tblj.LocationId,COUNT(*) WashesCount
	INTO #WashesCount1
	FROM tbljob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	WHERE tblj.JobType=@WashId
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND
    tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0
	and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0
	--AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #DetailCount1
(SELECT 
	tblj.LocationId,COUNT(*) DetailCount
	INTO #DetailCount1
	FROM tbljob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	WHERE tblj.JobType=@DetailId
	AND tbls.ServiceType=@DetailServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
    AND
    tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0
	and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --AND 
	--(@LocationId IS NULL or tblj.LocationId=@LocationId)
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId )
	
DROP TABLE  IF EXISTS #EmployeeCount1
(SELECT
    LocationId,COUNT(*) EmployeeCount
	INTO #EmployeeCount1
	FROM tblTimeClock WHERE 
	--EventDate='2020-09-29'
	RoleId=@WashRole
    AND (EventDate>=@FromDate AND EventDate<=@ToDate) 
	AND
	--and (@LocationId IS NULL or LocationId=@LocationId)
    IsActive=1 and ISNULL(IsDeleted,0)=0
	GROUP BY LocationId)
	

DROP TABLE  IF EXISTS #TotalCarWashed1
(SELECT tblj.LocationId,Count(*) TotalCarWashed INTO #TotalCarWashed1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.JobStatus=@CompletedJobStatus and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
--AND tblj.JobDate='2020-09-29' 
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	 GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalHoursWashed1
(SELECT tblj.LocationId,SUM(CAST((CAST(DATEDIFF(MINUTE, TimeIn,ActualTimeOut)AS decimal(9,2))/60) AS decimal(9,2))) TotalHoursWashed
INTO #TotalHoursWashed1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
    AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #WashTime1
(SELECT tbll.LocationId,
CASE
	   WHEN wt.Washer <=3 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=3 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*8) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer <=6 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=6 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*7) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer <=9 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=9 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*6) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer <=11 AND wt.CarCount <=3 THEN 25
	   WHEN wt.Washer <=11 AND wt.CarCount >3 THEN (25+(wt.CarCount - 3)*5) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*3) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount >5  THEN (25+(wt.CarCount - 6)*2) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*2) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   WHEN wt.Washer >26 AND wt.CarCount <=7 THEN 25
	   WHEN wt.Washer >26 AND wt.CarCount >7  THEN (25+(wt.CarCount - 7)*2) + ((wt.CarCount+ISNULL(tbllo.OffSet1,0))*ISNULL(tbllo.OffSet1On,0))
	   ELSE 0
	   END AS WashTimeMinutes
	   INTO #WashTime1
	   FROM tblLocation tbll
LEFT JOIN #WashRoleCount wt ON(tbll.LocationId = wt.LocationId)
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbllo.LocationId)
LEFT JOIN #EventDateForLocation edfl ON(tbll.LocationId = edfl.LocationId)
WHERE --isnull(IsActive,1) = 1 AND
--isnull(isDeleted,0) = 0  --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	--AND 
	(edfl.EventDate>=@FromDate AND edfl.EventDate<=@ToDate) 

	--AND tblj.JobDate='2020-09-29'
	)

DROP TABLE  IF EXISTS #ForecastedCar1
(SELECT tblj.LocationId,COUNT(VehicleId) ForecastedCar into #ForecastedCar1
	 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId)
	 WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #Current1
(SELECT
    tblj.LocationId,COUNT(VehicleId) Currents
into #Current1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	 WHERE tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	--AND tblj.JobDate='2020-09-29'
	 GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #WashSales1
(SELECT tblj.LocationId,SUM(tblji.Price)WashSales into #WashSales1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tbljp.PaymentStatus=@CompletedPaymentStatus
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and tbljp.IsActive=1 and ISNULL(tbljp.IsDeleted,0)=0 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and 
--(@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)



DROP TABLE  IF EXISTS #DetailSales1
(SELECT tblj.LocationId,SUM(tblji.Price) DetailSales into #DetailSales1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId)
WHERE tblj.JobType=@DetailId and tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType=@DetailServiceId and 
tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and tbljp.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
and ISNULL(tbls.IsDeleted,0) =0 and ISNULL(tbljp.IsDeleted,0)=0 AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #ExtraService1
(SELECT tblj.LocationId,SUM(tblji.Price)ExtraService into #ExtraService1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId) WHERE tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus 
and tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType=@ServiceType and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1
and tbljp.IsActive=1
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0
and ISNULL(tbljp.IsDeleted,0)=0-- and 
--(@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #MerchandizeSales1
(SELECT tblj.LocationId,SUM(tbljpi.Price)MerchandizeSales into #MerchandizeSales1 FROM tblJob tblj inner join tblJobProductItem tbljpi 
on(tblj.JobId = tbljpi.JobId) inner join tblProduct tbp on(tbljpi.ProductId = tbp.ProductId)
INNER JOIN GetTable('ProductType') pt on pt.valueid = tbp.ProductType and pt.valuedesc ='Merchandize' 
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId)
WHERE tbljp.PaymentStatus=@CompletedPaymentStatus and tblj.IsActive=1 and tbljpi.IsActive=1 and tbljp.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tbljpi.IsDeleted,0)=0 and ISNULL(tbljp.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #MonthlyClientSales1
(SELECT tblj.LocationId,ISNULL(SUM(tblcvmd.TotalPrice),0)MonthlyClientSales into #MonthlyClientSales1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) inner join tblClientVehicleMembershipDetails tblcvmd
on(tblj.VehicleId = tblcvmd.ClientVehicleId)
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForWash1
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForWash into #TotalCarCountForWash1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetail1
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForDetail into #TotalCarCountForDetail1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAdditionalService1
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAdditionalService into #TotalCarCountForAdditionalService1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate)  
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAllService1
(SELECT tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAllService into #TotalCarCountForAllService1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType in(@WashId,@DetailId) and tblj.IsActive=1 and 
tblji.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #AdditionalServiceSales1
(SELECT tblj.LocationId,SUM(tblji.Price)AdditionalServiceSales into #AdditionalServiceSales1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalServiceSales1
(SELECT tblj.LocationId,SUM(tblji.Price)TotalServiceSales INTO #TotalServiceSales1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tblJP on(tblj.JobId = tblJP.JobId)
WHERE tblj.JobType in(@WashId,@DetailId) and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
AND tblJP.PaymentStatus=@CompletedPaymentStatus
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForWashAndAdditionalService1
(SELECT tblj.LocationId,SUM(tblji.Price)LabourCostForWashAndAdditionalService INTO #LabourCostForWashAndAdditionalService1 FROM tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalCarCountForWashAndAdditionalService1
(SELECT tblj.LocationId,COUNT(*)TotalCarCountForWashAndAdditionalService into #TotalCarCountForWashAndAdditionalService1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
WHERE tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForDetailService1
(SELECT tblj.LocationId,SUM(tblji.Price)LabourCostForDetailService into #LabourCostForDetailService1 FROM tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
WHERE tblj.JobType =@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetailService1
(SELECT tblj.LocationId,COUNT(*)TotalCarCountForDetailService into #TotalCarCountForDetailService1 FROM tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
WHERE tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE IF EXISTS #ServiceSales1
SELECT 
tbl.LocationId,
ISNULL(ws.WashSales,0.00) WashSales,
ISNULL(ds.DetailSales,0.00) DetailSales,
ISNULL(es.ExtraService,0.00) ExtraService,
ISNULL(ms.MerchandizeSales,0) MerchandizeSales,
SUM(ISNULL(ws.WashSales,0.00) + ISNULL(ds.DetailSales,0.00) + ISNULL(es.ExtraService,0.00) + ISNULL(ms.MerchandizeSales,0.00)) SumOfWashDetailMerchandizeSales
into #ServiceSales1
FROM tblLocation tbl LEFT JOIN #WashSales1 ws ON(tbl.LocationId = ws.LocationId) LEFT JOIN #DetailSales1 ds ON(tbl.LocationId = ds.LocationId)
LEFT JOIN #ExtraService1 es ON(tbl.LocationId=es.LocationId) LEFT JOIN #MerchandizeSales1 ms ON(tbl.LocationId=ms.LocationId)
GROUP BY tbl.LocationId,WashSales,DetailSales,ExtraService,MerchandizeSales

SELECT 
distinct
ISNULL(tbl.LocationId,0) LocationId,
tbl.LocationName,
ISNULL(wc.WashesCount,0) WashesCount,
ISNULL(dc.DetailCount,0) DetailCount,
ISNULL(ec.EmployeeCount,0) EmployeeCount,
CASE 
	       WHEN thw.TotalHoursWashed='0.00' THEN ISNULL(tcw.TotalCarWashed,0) 
		   WHEN thw.TotalHoursWashed!='0.00' THEN CAST((ISNULL(tcw.TotalCarWashed,0)/thw.TotalHoursWashed)as decimal(9,2))
		   END AS Score ,
		   CASE 
		   WHEN wt.WashTimeMinutes != '0.0000' THEN 25 
		   WHEN wt.WashTimeMinutes != 0 and wt.WashTimeMinutes IS NOT NULL THEN wt.WashTimeMinutes
		   END as WashTime,
ISNULL(cu.Currents,0) Currents,
ISNULL(fc.ForecastedCar,0)ForecastedCar,
ISNULL(ss.WashSales,0.00) WashSales,
ISNULL(ss.DetailSales,0.00) DetailSales,
ISNULL(ss.ExtraService,0.00) ExtraServiceSales,
ISNULL(ss.MerchandizeSales,0) MerchandizeSales,
ss.SumOfWashDetailMerchandizeSales AS TotalSales,
ISNULL(mcs.MonthlyClientSales,0) MonthlyClientSales,
ISNULL(CAST((ss.WashSales/tccfw.TotalCarCountForWash) AS Decimal(9,2)),0) AverageWashPerCar,
ISNULL(CAST((ss.DetailSales/tccfd.TotalCarCountForDetail)AS Decimal(9,2)),0) AverageDetailPerCar,
ISNULL(CAST((ass.AdditionalServiceSales/tccfas.TotalCarCountForAdditionalService) AS DECIMAL(9,2)),0) AverageExtraServicePerCar,
ISNULL(CAST((tss.TotalServiceSales/tccfals.TotalCarCountForAllService)AS DECIMAL(9,2)),0)  AverageTotalPerCar,
ISNULL(CAST((lcfwaas.LabourCostForWashAndAdditionalService/tccfwaas.TotalCarCountForWashAndAdditionalService) AS DECIMAL(9,2)),0) LabourCostPerCarMinusDetail,
ISNULL(CAST((lcfds.LabourCostForDetailService/tccfds.TotalCarCountForDetailService)AS DECIMAL(9,2)),0)  DetailCostPerCar
FROM tblLocation tbl 
LEFT JOIN #WashesCount1 wc on(tbl.LocationId = wc.LocationId)
LEFT JOIN #DetailCount1 dc on(tbl.LocationId = dc.LocationId)
LEFT JOIN #EmployeeCount1 ec on(tbl.LocationId = ec.LocationId)
LEFT JOIN #TotalCarWashed1 tcw on(tbl.LocationId = tcw.LocationId)
LEFT JOIN #TotalHoursWashed1 thw on(tbl.LocationId = thw.LocationId)
LEFT JOIN #WashTime1 wt on(tbl.LocationId = wt.LocationId)
LEFT JOIN #ForecastedCar1 fc ON(tbl.LocationId = fc.LocationId)
LEFT JOIN #Current1 cu on(tbl.LocationId = cu.LocationId)
LEFT JOIN #ServiceSales1 ss on(tbl.LocationId = ss.LocationId)
LEFT JOIN #MonthlyClientSales1 mcs on(tbl.LocationId = mcs.LocationId)
LEFT JOIN #TotalCarCountForWash1 tccfw on(tbl.LocationId = tccfw.LocationId)
LEFT JOIN #TotalCarCountForDetail1 tccfd on(tbl.LocationId = tccfd.LocationId)
LEFT JOIN #TotalCarCountForAdditionalService1 tccfas on(tbl.LocationId = tccfas.LocationId)
LEFT JOIN #TotalCarCountForAllService1 tccfals on(tbl.LocationId = tccfals.LocationId)
LEFT JOIN #AdditionalServiceSales1 ass on(tbl.LocationId = ass.LocationId)
LEFT JOIN #TotalServiceSales1 tss on(tbl.LocationId = tss.LocationId)
LEFT JOIN #LabourCostForWashAndAdditionalService1 lcfwaas on(tbl.LocationId = lcfwaas.LocationId)
LEFT JOIN #TotalCarCountForWashAndAdditionalService1 tccfwaas on(tbl.LocationId = tccfwaas.LocationId)
LEFT JOIN #LabourCostForDetailService1 lcfds on(tbl.LocationId = lcfds.LocationId)
LEFT JOIN #TotalCarCountForDetailService1 tccfds on(tbl.LocationId = tccfds.LocationId)
WHERE tbl.IsActive=1 and ISNULL(tbl.IsDeleted,0)=0

END


END