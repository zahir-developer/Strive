﻿



-- =============================================
-- Author:		Vineeth B
-- Create date: 03-11-2020
-- Description:	To get Dashboard Details
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetDashboardStatistics] --0,'2020-11-01','2020-11-20'
(@LocationId INT,@FromDate Date,@ToDate Date)
AS
BEGIN
Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Washes')
Declare @DetailServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Details')
Declare @AdditionalServiceId INT = (Select valueid from GetTable('ServiceType') where valuedesc='Additional Services')
Declare @DetailId INT = (Select valueid from GetTable('JobType') where valuedesc='Detail')
Declare @CompletedJobStatus INT = (Select valueid from GetTable('JobStatus') where valuedesc='Completed')
Declare @WashRole INT = (Select RoleMasterId from tblRoleMaster WHERE RoleName='Wash')
DECLARE @CompletedPaymentStatus INT = (Select valueid from GetTable('PaymentStatus') where valuedesc='Success')
Declare @ServiceType INT = (Select valueid from GetTable('ServiceType') where valuedesc ='Additional Services')
Declare @MerchandizeId INT =(select valueid from GetTable('ProductType') where valuedesc='Merchandize')

DROP TABLE IF EXISTS #WashRoleCount
SELECT tblL.LocationId,COUNT(tblTC.EmployeeId) Washer,COUNT(tblJ.JobId) CarCount
INTO #WashRoleCount FROM tblTimeClock tblTC INNER JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
INNER JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole  
AND tblJ.JobType=@WashId GROUP BY tblL.LocationId

DROP TABLE IF EXISTS #EventDateForLocation
SELECT tblL.LocationId,tblTC.EventDate
INTO #EventDateForLocation FROM tblTimeClock tblTC INNER JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
INNER JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole  
AND tblJ.JobType=@WashId 
IF(@LocationId != 0)
BEGIN
DROP TABLE  IF EXISTS #WashesCount
(SELECT 
	tblj.LocationId,COUNT(*) WashesCount
	INTO #WashesCount
	FROM tbljob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	WHERE tblj.JobType=@WashId
	AND tbls.ServiceType=@WashServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId AND
    tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0
	and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0
	--AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #DetailCount
(SELECT 
	tblj.LocationId,COUNT(*) DetailCount
	INTO #DetailCount
	FROM tbljob tblj 
	inner join tblJobItem tblji on(tblj.JobId=tblji.JobId)
	inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	WHERE tblj.JobType=@DetailId
	AND tbls.ServiceType=@DetailServiceId
	AND tblj.JobStatus=@CompletedJobStatus
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId AND
    tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0
	and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --AND 
	--(@LocationId IS NULL or tblj.LocationId=@LocationId)
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId )
	
DROP TABLE  IF EXISTS #EmployeeCount
(SELECT
    LocationId,COUNT(*) EmployeeCount
	INTO #EmployeeCount
	from tblTimeClock where 
	--EventDate='2020-09-29'
	RoleId=@WashRole
    AND (EventDate>=@FromDate AND EventDate<=@ToDate) 
	AND LocationId=@LocationId AND
	--and (@LocationId IS NULL or LocationId=@LocationId)
    IsActive=1 and ISNULL(IsDeleted,0)=0
	GROUP BY LocationId)
	

DROP TABLE  IF EXISTS #TotalCarWashed
(Select tblj.LocationId,Count(*) TotalCarWashed INTO #TotalCarWashed from tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.JobStatus=@CompletedJobStatus and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
--AND tblj.JobDate='2020-09-29' 
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalHoursWashed
(Select tblj.LocationId,SUM(CAST((CAST(DATEDIFF(MINUTE, TimeIn,ActualTimeOut)AS decimal(9,2))/60) AS decimal(9,2))) TotalHoursWashed
INTO #TotalHoursWashed from tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
    AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #WashTime
(SELECT tbll.LocationId,
CASE
	   WHEN wt.Washer <=3 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=3 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*8) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer <=6 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=6 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*7) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer <=9 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=9 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*6) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer <=11 AND wt.CarCount <=3 THEN 25
	   WHEN wt.Washer <=11 AND wt.CarCount >3 THEN (25+(wt.CarCount - 3)*5) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*3) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount >5  THEN (25+(wt.CarCount - 6)*2) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*2) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer >26 AND wt.CarCount <=7 THEN 25
	   WHEN wt.Washer >26 AND wt.CarCount >7  THEN (25+(wt.CarCount - 7)*2) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   ELSE 0
	   END AS WashTimeMinutes
	   INTO #WashTime
	   FROM tblLocation tbll
LEFT JOIN #WashRoleCount wt ON(tbll.LocationId = wt.LocationId)
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbllo.LocationId)
LEFT JOIN #EventDateForLocation edfl ON(tbll.LocationId = edfl.LocationId)
WHERE isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0  AND
tbll.LocationId = @LocationId--AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND (edfl.EventDate>=@FromDate AND edfl.EventDate<=@ToDate) 

	--AND tblj.JobDate='2020-09-29'
	)

DROP TABLE  IF EXISTS #ForecastedCar
(SELECT tblj.LocationId,COUNT(VehicleId) ForecastedCar into #ForecastedCar
	 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId)
	 where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #Current
(SELECT
    tblj.LocationId,COUNT(VehicleId) Currents
into #Current from tblJob tblj inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	 where tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
	--AND tblj.JobDate='2020-09-29'
	 GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #WashSales
(Select tblj.LocationId,SUM(tblji.Price)WashSales into #WashSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tbljp.PaymentStatus=@CompletedPaymentStatus
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and tbljp.IsActive=1 and ISNULL(tbljp.IsDeleted,0)=0 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and 
--(@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)



DROP TABLE  IF EXISTS #DetailSales
(Select tblj.LocationId,SUM(tblji.Price) DetailSales into #DetailSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId)
where tblj.JobType=@DetailId and tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType=@DetailServiceId and 
tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and tbljp.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
and ISNULL(tbls.IsDeleted,0) =0 and ISNULL(tbljp.IsDeleted,0)=0 AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #ExtraService
(Select tblj.LocationId,SUM(tblji.Price)ExtraService into #ExtraService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId) where tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus 
and tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType=@ServiceType and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1
and tbljp.IsActive=1
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0
and ISNULL(tbljp.IsDeleted,0)=0-- and 
--(@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #MerchandizeSales
(Select tblj.LocationId,SUM(tbljpi.Price)MerchandizeSales into #MerchandizeSales from tblJob tblj inner join tblJobProductItem tbljpi 
on(tblj.JobId = tbljpi.JobId) inner join tblProduct tbp on(tbljpi.ProductId = tbp.ProductId)
INNER JOIN GetTable('ProductType') pt on pt.valueid = tbp.ProductType and pt.valuedesc ='Merchandize'
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId)
where tbljp.PaymentStatus=@CompletedPaymentStatus and tblj.IsActive=1 and tbljpi.IsActive=1 and tbljp.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tbljpi.IsDeleted,0)=0 and ISNULL(tbljp.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #MonthlyClientSales
(Select tblj.LocationId,ISNULL(SUM(tblcvmd.TotalPrice),0)MonthlyClientSales into #MonthlyClientSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) inner join tblClientVehicleMembershipDetails tblcvmd
on(tblj.VehicleId = tblcvmd.ClientVehicleId)
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForWash
(Select tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForWash into #TotalCarCountForWash from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetail
(Select tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForDetail into #TotalCarCountForDetail from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAdditionalService
(Select tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAdditionalService into #TotalCarCountForAdditionalService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAllService
(Select tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAllService into #TotalCarCountForAllService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tblj.IsActive=1 and 
tblji.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #AdditionalServiceSales
(Select tblj.LocationId,SUM(tblji.Price)AdditionalServiceSales into #AdditionalServiceSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalServiceSales
(Select tblj.LocationId,SUM(tblji.Price)TotalServiceSales INTO #TotalServiceSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForWashAndAdditionalService
(Select tblj.LocationId,SUM(tblji.Price)LabourCostForWashAndAdditionalService INTO #LabourCostForWashAndAdditionalService from tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalCarCountForWashAndAdditionalService
(Select tblj.LocationId,COUNT(*)TotalCarCountForWashAndAdditionalService into #TotalCarCountForWashAndAdditionalService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForDetailService
(Select tblj.LocationId,SUM(tblji.Price)LabourCostForDetailService into #LabourCostForDetailService from tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType =@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetailService
(Select tblj.LocationId,COUNT(*)TotalCarCountForDetailService into #TotalCarCountForDetailService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
where tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	AND tblj.LocationId=@LocationId 
--tblj.JobDate='2020-09-29' 
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
ISNULL(wt.WashTimeMinutes,0) WashTime,
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
	from tblTimeClock where 
	--EventDate='2020-09-29'
	RoleId=@WashRole
    AND (EventDate>=@FromDate AND EventDate<=@ToDate) 
	AND
	--and (@LocationId IS NULL or LocationId=@LocationId)
    IsActive=1 and ISNULL(IsDeleted,0)=0
	GROUP BY LocationId)
	

DROP TABLE  IF EXISTS #TotalCarWashed1
(Select tblj.LocationId,Count(*) TotalCarWashed INTO #TotalCarWashed1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.JobStatus=@CompletedJobStatus and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
--AND tblj.JobDate='2020-09-29' 
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	 GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalHoursWashed1
(Select tblj.LocationId,SUM(CAST((CAST(DATEDIFF(MINUTE, TimeIn,ActualTimeOut)AS decimal(9,2))/60) AS decimal(9,2))) TotalHoursWashed
INTO #TotalHoursWashed1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
    AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #WashTime1
(SELECT tbll.LocationId,
CASE
	   WHEN wt.Washer <=3 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=3 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*8) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer <=6 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=6 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*7) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer <=9 AND wt.CarCount <=1 THEN 25
	   WHEN wt.Washer <=9 AND wt.CarCount >1 THEN (25+(wt.CarCount - 1)*6) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer <=11 AND wt.CarCount <=3 THEN 25
	   WHEN wt.Washer <=11 AND wt.CarCount >3 THEN (25+(wt.CarCount - 3)*5) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=12 AND wt.Washer<=15 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*3) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=16 AND wt.Washer<=21 AND wt.CarCount >5  THEN (25+(wt.CarCount - 6)*2) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount <=5 THEN 25
	   WHEN wt.Washer >=22 AND wt.Washer<=26 AND wt.CarCount >5  THEN (25+(wt.CarCount - 5)*2) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wt.Washer >26 AND wt.CarCount <=7 THEN 25
	   WHEN wt.Washer >26 AND wt.CarCount >7  THEN (25+(wt.CarCount - 7)*2) + ((wt.CarCount+tbllo.OffSet1)*tbllo.OffSet1On)
	   ELSE 0
	   END AS WashTimeMinutes
	   INTO #WashTime1
	   FROM tblLocation tbll
LEFT JOIN #WashRoleCount wt ON(tbll.LocationId = wt.LocationId)
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbllo.LocationId)
LEFT JOIN #EventDateForLocation edfl ON(tbll.LocationId = edfl.LocationId)
WHERE isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0  --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND (edfl.EventDate>=@FromDate AND edfl.EventDate<=@ToDate) 

	--AND tblj.JobDate='2020-09-29'
	)

DROP TABLE  IF EXISTS #ForecastedCar1
(SELECT tblj.LocationId,COUNT(VehicleId) ForecastedCar into #ForecastedCar1
	 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId)
	 where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	--AND tblj.JobDate='2020-09-29' 
	GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #Current1
(SELECT
    tblj.LocationId,COUNT(VehicleId) Currents
into #Current1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	 where tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 --AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
	--AND tblj.JobDate='2020-09-29'
	 GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #WashSales1
(Select tblj.LocationId,SUM(tblji.Price)WashSales into #WashSales1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tbljp on(tblj.JobId = tblJP.JobId)
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tbljp.PaymentStatus=@CompletedPaymentStatus
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and tbljp.IsActive=1 and ISNULL(tbljp.IsDeleted,0)=0 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and 
--(@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)



DROP TABLE  IF EXISTS #DetailSales1
(Select tblj.LocationId,SUM(tblji.Price) DetailSales into #DetailSales1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId)
where tblj.JobType=@DetailId and tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType=@DetailServiceId and 
tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and tbljp.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
and ISNULL(tbls.IsDeleted,0) =0 and ISNULL(tbljp.IsDeleted,0)=0 AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #ExtraService1
(Select tblj.LocationId,SUM(tblji.Price)ExtraService into #ExtraService1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId) where tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus 
and tbljp.PaymentStatus=@CompletedPaymentStatus and tbls.ServiceType=@ServiceType and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1
and tbljp.IsActive=1
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0
and ISNULL(tbljp.IsDeleted,0)=0-- and 
--(@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #MerchandizeSales1
(Select tblj.LocationId,SUM(tbljpi.Price)MerchandizeSales into #MerchandizeSales1 from tblJob tblj inner join tblJobProductItem tbljpi 
on(tblj.JobId = tbljpi.JobId) inner join tblProduct tbp on(tbljpi.ProductId = tbp.ProductId)
INNER JOIN GetTable('ProductType') pt on pt.valueid = tbp.ProductType and pt.valuedesc ='Merchandize' 
inner join tblJobPayment tbljp on(tblj.JobId = tbljp.JobId)
where tbljp.PaymentStatus=@CompletedPaymentStatus and tblj.IsActive=1 and tbljpi.IsActive=1 and tbljp.IsActive=1 and
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tbljpi.IsDeleted,0)=0 and ISNULL(tbljp.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #MonthlyClientSales1
(Select tblj.LocationId,ISNULL(SUM(tblcvmd.TotalPrice),0)MonthlyClientSales into #MonthlyClientSales1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) inner join tblClientVehicleMembershipDetails tblcvmd
on(tblj.VehicleId = tblcvmd.ClientVehicleId)
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--AND tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForWash1
(Select tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForWash into #TotalCarCountForWash1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetail1
(Select tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForDetail into #TotalCarCountForDetail1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAdditionalService1
(Select tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAdditionalService into #TotalCarCountForAdditionalService1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate)  
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAllService1
(Select tblj.LocationId,COUNT(distinct tblj.JobId)TotalCarCountForAllService into #TotalCarCountForAllService1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tblj.IsActive=1 and 
tblji.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #AdditionalServiceSales1
(Select tblj.LocationId,SUM(tblji.Price)AdditionalServiceSales into #AdditionalServiceSales1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalServiceSales1
(Select tblj.LocationId,SUM(tblji.Price)TotalServiceSales INTO #TotalServiceSales1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForWashAndAdditionalService1
(Select tblj.LocationId,SUM(tblji.Price)LabourCostForWashAndAdditionalService INTO #LabourCostForWashAndAdditionalService1 from tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalCarCountForWashAndAdditionalService1
(Select tblj.LocationId,COUNT(*)TotalCarCountForWashAndAdditionalService into #TotalCarCountForWashAndAdditionalService1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForDetailService1
(Select tblj.LocationId,SUM(tblji.Price)LabourCostForDetailService into #LabourCostForDetailService1 from tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType =@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 --and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND (tblj.JobDate>=@FromDate AND tblj.JobDate<=@ToDate) 
--tblj.JobDate='2020-09-29' 
GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetailService1
(Select tblj.LocationId,COUNT(*)TotalCarCountForDetailService into #TotalCarCountForDetailService1 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
where tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
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
ISNULL(wt.WashTimeMinutes,0) WashTime,
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