





-- =============================================
-- Author:		Vineeth B
-- Create date: 03-11-2020
-- Description:	To get Dashboard Details
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetDashboard] 
(@LocationId INT = null)
AS
BEGIN
Declare @WashId INT = (Select top 1 valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashServiceId INT = (Select top 1 valueid from GetTable('ServiceType') where valuedesc='Washes')
Declare @DetailServiceId INT = (Select top 1 valueid from GetTable('ServiceType') where valuedesc='Detail Package')
Declare @AdditionalServiceId INT = (Select top 1 valueid from GetTable('ServiceType') where valuedesc='Additional Services')
Declare @DetailId INT = (Select top 1 valueid from GetTable('JobType') where valuedesc='Detail')
Declare @CompletedJobStatus INT = (Select top 1 valueid from GetTable('JobStatus') where valuedesc='Completed')

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
	and
	--AND tblj.JobDate=CAST(getdate() AS Date) AND
    tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0
	and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0
	AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

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
	AND
	--AND tblj.JobDate=CAST(getdate() AS Date) AND
    tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0
	and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 AND 
	(@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId )

DROP TABLE  IF EXISTS #EmployeeCount
(SELECT
    LocationId,COUNT(*) EmployeeCount
	INTO #EmployeeCount
	from tblTimeClock where 
	EventDate='2020-09-29'
	--EventDate=CAST(getdate() AS Date) 
	and (@LocationId IS NULL or LocationId=@LocationId)and IsActive=1 and ISNULL(IsDeleted,0)=0
	GROUP BY LocationId)
	

DROP TABLE  IF EXISTS #TotalCarWashed
(Select tblj.LocationId,Count(*) TotalCarWashed INTO #TotalCarWashed from tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.JobStatus=@CompletedJobStatus and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)
	--AND tblj.JobDate=CAST(getdate() AS Date))


DROP TABLE  IF EXISTS #TotalHoursWashed
(Select tblj.LocationId,convert(varchar(8),Cast(DateAdd(ms, SUM(CAST(DateDiff( ms, '00:00:00', cast(ActualTimeOut as time)) AS BIGINT)), '00:00:00' ) as Time )) TotalHoursWashed
INTO #TotalHoursWashed from tblJob tblj inner join tblJobItem tblji on(tblj.JobId =tblji.JobId)
inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId) where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	--AND tblj.JobDate=CAST(getdate() AS Date)
	AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)
	

DROP TABLE  IF EXISTS #Score
(Select * INTO #Score from #TotalCarWashed )


DROP TABLE  IF EXISTS #WashTime
(SELECT
    tblj.LocationId,convert(varchar(8),Cast(DateAdd(ms, AVG(CAST(DateDiff( ms, '00:00:00', cast(ActualTimeOut as time)) AS BIGINT)), '00:00:00' ) as Time )) WashTime
 into #WashTime  from tblJob tblj inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId)
	 where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.JobStatus=@CompletedJobStatus
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	AND tblj.JobDate =CAST(getdate() AS Date) GROUP BY tblj.LocationId)
	--AND tblj.JobDate='2020-09-29')

DROP TABLE  IF EXISTS #ForecastedCar
(SELECT tblj.LocationId,COUNT(VehicleId) ForecastedCar into #ForecastedCar
	 from tblJob tblj inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) inner join tblService tbls on(tblji.ServiceId=tbls.ServiceId)
	 where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	--AND tblj.JobDate=CAST(getdate() AS Date)
	AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #Current
(SELECT
    tblj.LocationId,COUNT(VehicleId) Currents
into #Current from tblJob tblj inner join tblJobItem tblji on(tblj.JobId=tblji.JobId) inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
	 where tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus and tbls.ServiceType IN(@WashServiceId,@DetailServiceId)
and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 AND (@LocationId IS NULL or tblj.LocationId=@LocationId)
	--AND tblj.JobDate=CAST(getdate() AS Date)
	AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #WashSales
(Select tblj.LocationId,SUM(tblji.Price)WashSales into #WashSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 and 
(@LocationId IS NULL or tblj.LocationId=@LocationId)
--AND tblj.JobDate=CAST(getdate() AS Date)
AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)



DROP TABLE  IF EXISTS #DetailSales
(Select tblj.LocationId,SUM(tblji.Price) DetailSales into #DetailSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
and ISNULL(tbls.IsDeleted,0) =0 --AND tblj.JobDate=CAST(getdate() AS Date)
AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)


Declare @ServiceType INT = (Select valueid from GetTable('ServiceType') where valuedesc ='Additional Services')
DROP TABLE  IF EXISTS #ExtraService
(Select tblj.LocationId,SUM(tblji.Price)ExtraService into #ExtraService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) where tblj.JobType in(@WashId,@DetailId) and tblj.JobStatus=@CompletedJobStatus 
and tbls.ServiceType=@ServiceType and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 and 
(@LocationId IS NULL or tblj.LocationId=@LocationId)
--and tblj.JobDate=CAST(getdate() AS Date)
AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

Declare @MerchandizeId INT =(select valueid from GetTable('ProductType') where valuedesc='Merchandize')
DROP TABLE  IF EXISTS #MerchandizeSales
(Select tblj.LocationId,SUM(tbljpi.Price)MerchandizeSales into #MerchandizeSales from tblJob tblj inner join tblJobProductItem tbljpi 
on(tblj.JobId = tbljpi.JobId) inner join tblProduct tbp on(tbljpi.ProductId = tbp.ProductId)
where tblj.JobType in(@WashId,@DetailId) and tbp.ProductType=@MerchandizeId and tblj.IsActive=1 and tbljpi.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tbljpi.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
--and tblj.JobDate=CAST(getdate() AS Date)
AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #MonthlyClientSales
(Select tblj.LocationId,COUNT(1)MonthlyClientSales into #MonthlyClientSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tbls.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
--AND tblj.JobDate=CAST(getdate() AS Date)
AND tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForWash
(Select tblj.LocationId,COUNT(*)TotalCarCountForWash into #TotalCarCountForWash from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType=@WashId and tbls.ServiceType=@WashServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetail
(Select tblj.LocationId,COUNT(*)TotalCarCountForDetail into #TotalCarCountForDetail from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAdditionalService
(Select tblj.LocationId,COUNT(*)TotalCarCountForAdditionalService into #TotalCarCountForAdditionalService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForAllService
(Select tblj.LocationId,COUNT(*)TotalCarCountForAllService into #TotalCarCountForAllService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@DetailServiceId,@AdditionalServiceId) and tblj.IsActive=1 and 
tblji.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #AdditionalServiceSales
(Select tblj.LocationId,SUM(tblji.Price)AdditionalServiceSales into #AdditionalServiceSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType=@AdditionalServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalServiceSales
(Select tblj.LocationId,SUM(tblji.Price)TotalServiceSales INTO #TotalServiceSales from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForWashAndAdditionalService
(Select tblj.LocationId,SUM(tblji.Price)LabourCostForWashAndAdditionalService INTO #LabourCostForWashAndAdditionalService from tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and tbls.IsActive=1 and ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)


DROP TABLE  IF EXISTS #TotalCarCountForWashAndAdditionalService
(Select tblj.LocationId,COUNT(*)TotalCarCountForWashAndAdditionalService into #TotalCarCountForWashAndAdditionalService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
where tblj.JobType in(@WashId,@DetailId) and tbls.ServiceType in(@WashServiceId,@AdditionalServiceId) and tblj.IsActive=1 and tblji.IsActive=1 
and ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #LabourCostForDetailService
(Select tblj.LocationId,SUM(tblji.Price)LabourCostForDetailService into #LabourCostForDetailService from tblJob tblj inner join tblJobItem tblji 
on(tblj.JobId = tblji.JobId) 
inner join tblService tblS on(tblji.ServiceId = tbls.ServiceId)
where tblj.JobType =@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and tbls.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and 
ISNULL(tblji.IsDeleted,0)=0 and ISNULL(tbls.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)

DROP TABLE  IF EXISTS #TotalCarCountForDetailService
(Select tblj.LocationId,COUNT(*)TotalCarCountForDetailService into #TotalCarCountForDetailService from tblJob tblj inner join tblJobItem tblji on(tblj.JobId = tblji.JobId)
inner join tblService tbls on(tblji.ServiceId = tbls.ServiceId) 
where tblj.JobType=@DetailId and tbls.ServiceType=@DetailServiceId and tblj.IsActive=1 and tblji.IsActive=1 and 
ISNULL(tblj.IsDeleted,0)=0 and ISNULL(tblji.IsDeleted,0)=0 and (@LocationId IS NULL or tblj.LocationId=@LocationId)
AND --tblj.JobDate=CAST(getdate() AS Date) AND
tblj.JobDate='2020-09-29' GROUP BY tblj.LocationId)


DROP TABLE IF EXISTS #ServiceSales
SELECT 
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
ISNULL(tbl.LocationId,0) LocationId,
tbl.LocationName,
ISNULL(wc.WashesCount,0) WashesCount,
ISNULL(dc.DetailCount,0) DetailCount,
ISNULL(ec.EmployeeCount,0) EmployeeCount,
ISNULL(tcw.TotalCarWashed,0) Score,
ISNULL(wt.WashTime,'00:00:00') WashTime,
ISNULL(cu.Currents,0) Currents,
ISNULL(fc.ForecastedCar,0)ForecastedCar,
ISNULL(ss.WashSales,0.00) WashSales,
ISNULL(ss.DetailSales,0.00) DetailSales,
ISNULL(ss.ExtraService,0.00) ExtraService,
ISNULL(ss.MerchandizeSales,0) MerchandizeSales,
ss.SumOfWashDetailMerchandizeSales AS TotalSales,
ISNULL(mcs.MonthlyClientSales,0) MonthlyClientSales,
CAST(CAST((ss.WashSales/tccfw.TotalCarCountForWash) AS Decimal)AS INT) AverageWashPerCar,
CAST(CAST((ss.DetailSales/tccfd.TotalCarCountForDetail)AS Decimal)AS INT) AverageDetailPerCar,
CAST(CAST((ass.AdditionalServiceSales/tccfas.TotalCarCountForAdditionalService) AS DECIMAL)AS INT) AverageExtraServicePerCar,
CAST(CAST((tss.TotalServiceSales/tccfals.TotalCarCountForAllService)AS DECIMAL )AS INT)  AverageTotalPerCar,
CAST(CAST((lcfwaas.LabourCostForWashAndAdditionalService/tccfwaas.TotalCarCountForWashAndAdditionalService) AS DECIMAL)AS INT) LabourCostPerCarMinusDetail,
CAST(CAST((lcfds.LabourCostForDetailService/tccfds.TotalCarCountForDetailService)AS DECIMAL )AS INT)  DetailCostPerCar
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
WHERE ( (wc.WashesCount !=0) OR (dc.DetailCount !=0) OR(ec.EmployeeCount!=0) OR (tcw.TotalCarWashed!=0)
OR (thw.TotalHoursWashed!='00:00:00') OR (wt.WashTime!='00:00:00')OR (cu.Currents !=0) 
OR (ss.WashSales!=0.00) OR (ss.DetailSales!=0.00) OR (ss.ExtraService!=0.00)
OR (ss.MerchandizeSales!=0.00) OR (mcs.MonthlyClientSales!=0) OR (tccfw.TotalCarCountForWash !=0)
OR (tccfd.TotalCarCountForDetail !=0) OR (tccfas.TotalCarCountForAdditionalService!=0)
OR (ass.AdditionalServiceSales!=0) OR (tss.TotalServiceSales!=0) OR (lcfwaas.LabourCostForWashAndAdditionalService!=0)
OR (tccfwaas.TotalCarCountForWashAndAdditionalService!=0) OR (lcfds.LabourCostForDetailService!=0)
OR (tccfds.TotalCarCountForDetailService!=0))
END