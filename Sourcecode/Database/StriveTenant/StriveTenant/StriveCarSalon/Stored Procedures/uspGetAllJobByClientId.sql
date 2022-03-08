-- =============================================
-- Author:		JUKI B
-- Create date: 02-03-2022
-- Description:	To show jobs based on client id

------------------------------------------------
 --[StriveCarSalon].[uspGetAllJobByClientId] null,1,NULL,'null', null, null, 'ASC', 'TicketNo','2022-01-04','2022-01-04'
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAllJobByClientId] 
(@JobDate Date = NULL, @LocationId int = NULL, @ClientId int = NULL, @Search varchar(50)=null,
@PageNo INT = NULL, 
@PageSize INT = NULL,
@SortOrder VARCHAR(5) = NULL, 
@SortBy VARCHAR(50) = NULL,
@StartDate date = NULL, 
@EndDate date = NULL,
@Type VARCHAR(20) = 'Detail'
)
AS
BEGIN

SET NOCOUNT ON;  

Declare @JobPackage varchar = (CASE WHEN @Type = 'WASH' THEN 'Wash Package' ELSE 'Detail Package' END)
Declare @TipPaymentId INT = (select top 1 valueid from GetTable('PaymentType') where valuedesc = 'tips')

 DROP TABLE IF EXISTS #ServiceType
Select valueid,valuedesc into #ServiceType from GetTable('ServiceType')

DROP TABLE IF EXISTS #Services
SELECT tblj.JobId, tblji.Price, tbls.ServiceName, tbls.ServiceId, st.valuedesc as ServiceType, tblj.ClientId, tblj.JobPaymentId 
INTO #Services FROM tblJob tblj 
INNER JOIN tblJobItem tblji ON(tblj.JobId = tblji.JobId)
INNER JOIN tblService tblS ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN GetTable('ServiceType') st ON(tbls.ServiceType = st.valueid)
WHERE (@JobDate is null OR tblj.JobDate=@JobDate) AND ((tblj.JobDate Between @StartDate and @EndDate) OR (@StartDate is null and @EndDate is null))
AND (@ClientId is null OR tblj.ClientId = @ClientId)
AND ((@LocationId is null OR @LocationId=0)OR tblj.LocationId=@LocationId)
AND tblj.IsActive = 1 AND tbljI.IsActive = 1 AND tblS.IsActive = 1
AND ISNULL(tblj.IsDeleted,0)=0 AND ISNULL(tbljI.IsDeleted,0)=0
--AND ISNULL(tblS.IsDeleted,0)=0 

--Select * from #Services

DROP TABLE IF EXISTS #Upcharge
SELECT JobId, ServiceName, Price 
INTO #Upcharge FROM #Services
WHERE ServiceType like '%Upcharge%'

--Select * from #Upcharge

--DROP TABLE IF EXISTS #OutsideServices
--SELECT JobId, ServiceName AS OutsideService INTO #OutsideServices 
--FROM #Services
--WHERE ServiceType = 'Outside Services'

----Select * from #OutsideServices

DROP TABLE IF EXISTS #Details
SELECT JobId, JobPaymentId, ServiceName AS ServiceTypeName INTO #Details 
FROM #Services
WHERE 
ServiceType = @JobPackage
--ServiceType = 'Detail Package' OR ServiceType = 'Wash Package'

--Select * from #Details

DROP TABLE IF EXISTS #TipAmount
SELECT JobId, tbljp.Amount  INTO #TipAmount
FROM #Services tbljs
LEFT JOIN tblJobPaymentDetail tbljp on tbljs.JobPaymentId = tbljp.JobPaymentId
Where tbljp.PaymentType = @TipPaymentId

--Select * from #TipAmount

--DROP TABLE IF EXISTS #AirFresheners
--SELECT JobId, ServiceName AS OutsideService INTO #AirFresheners 
--FROM #Services
--WHERE ServiceType = 'Air Fresheners'

----Select * from #AirFresheners

DROP TABLE IF EXISTS #ServicePrice
SELECT JobId, SUM(Price) Price
INTO #ServicePrice FROM #Services
GROUP BY JobId

--Select * from #ServicePrice

DROP TABLE IF EXISTS #Detailslist

SELECT 
--tblb.BayId,
--tblb.BayName
tblj.JobId 
,tblj.TicketNumber
,tblj.JobDate
,tblj.LocationId
,tbll.LocationName
--,tblji.JobItemId
,ISnULL(sp.Price, 0) AS Cost
,SUBSTRING(CONVERT(VARCHAR(8),tblj.TimeIn,108),0,6) AS TimeIn
,CONCAT(ISNULL(tblc.FirstName, 'Drive'),' ',ISNULL(tblc.LastName, 'Up')) AS ClientName
,tblca.PhoneNumber
,SUBSTRING(CONVERT(VARCHAR(8),tblj.EstimatedTimeOut,108),0,6) AS EstimatedTimeOut
,(tblji.ServiceName) ServiceTypeName
,cvMfr.MakeValue AS VehicleMake
,cvMo.ModelValue AS VehicleModel
,cvCo.valuedesc AS VehicleColor
,ISNULL(upc.Price,0.00) AS Upcharge
,tblj.VehicleId
,Format (tbljp.Createddate, 'yyyy-MM-dd hh:mm:ss') as PaymentDate
,tblj.JobPaymentId
, @TipPaymentId AS TipPaymentId
--,ISNULL(outs.OutsideService,'None') AS OutsideService
,ISNULL(TA.Amount, 0) AS TipAmount
,tblcv.Barcode into #Detailslist
FROM tbljob tblj 
INNER JOIN GetTable('JobType') jt ON(tblj.JobType = jt.valueid)
LEFT join tblClient tblc ON (tblj.ClientId = tblc.ClientId OR @ClientId = NULL) OR (tblj.ClientId = @ClientId OR @ClientId IS NOT NULL)
INNER join tblJobItem tbljd ON(tblj.JobId = tbljd.JobId)
left join tblClientAddress tblca ON(tblj.ClientId = tblca.ClientId)
LEFT join tblClientVehicle tblcv ON (tblc.ClientId = tblcv.ClientId and tblj.VehicleId = tblcv.VehicleId)
LEFT JOIN tblVehicleMake cvMfr ON(tblj.Make = cvMfr.MakeId)
LEFT JOIN tblVehicleModel cvMo ON(tblj.Model = cvMo.ModelId) and cvMfr.MakeId = cvMo.MakeId
LEFT join GetTable('VehicleColor') cvCo ON tblj.Color = cvCo.valueid
inner join #Services tblji ON(tblj.JobId = tblji.JobId)
inner join tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
--right join tblBay tblb ON(tbljd.BayId = tblb.BayId)
inner join tblLocation tbll on tblj.LocationId =tbll.LocationId
--inner join GetTable('ServiceType') st ON(st.valueid = tbls.ServiceType)
left join #Upcharge upc ON(tblj.JobId = upc.JobId)
left join #Details det ON(tblj.JobId = det.JobId)
--left join #OutsideServices outs ON(tblj.JobId = outs.JobId)
--left join #AirFresheners ar ON(tblj.JobId =ar.JobId)
left join #ServicePrice sp ON(tblj.JobId = sp.JobId)
LEFT JOIN	tblJobPayment tbljp  WITH(NOLOCK) ON tblj.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
LEFT JOIN #TipAmount TA on tblj.JobId = TA.JobId
WHERE 
(@JobDate is null OR tblj.JobDate = convert(date, @JobDate, 105))
and 
(@LocationId is null OR  @LocationId=0 OR tblj.LocationId=@LocationId)
and
(@ClientId is null OR @ClientId=0 OR tblc.ClientId=@ClientId)
and
jt.valuedesc = @Type
and
tblji.ServiceType in('Wash Package', 'Detail Package','Outside Services','Air Fresheners')
and
tblj.IsActive=1
and
tbljd.IsActive=1
--and
--tblji.IsActive=1
--and
--tblb.IsActive=1
--and
--tblcv.IsActive=1
--and
--ISNULL(tblb.IsDeleted,0)=0
and
ISNULL(tblj.IsDeleted,0)=0
and
ISNULL(tbljd.IsDeleted,0)=0
--and
--ISNULL(tblji.IsDeleted,0)=0
--and
--ISNULL(tblcv.IsDeleted,0)=0
and 
 (@Search is null or tblj.TicketNumber like @Search+'%'
 or det.ServiceTypeName like @Search+'%' or cvMfr.MakeValue like @Search+'%'
 or cvMo.ModelValue like @Search+'%'
 or cvCo.valuedesc like @Search+'%'
 or tblc.FirstName like @Search+'%'
 or tblc.LastName like @Search+'%'
 --or outs.OutsideService like @Search+'%'
 or tblj.TimeIn like @Search+ '%'
 or tblj.EstimatedTimeOut like @Search+ '%')
 
 ORDER BY 
 CASE WHEN @SortBy = 'TicketNo' AND @SortOrder = 'ASC' THEN tblj.TicketNumber END ASC,
 CASE WHEN @SortBy = 'TimeIn' AND @SortOrder = 'ASC' THEN tblj.TimeIn END ASC,
 CASE WHEN @SortBy = 'TimeOut' AND @SortOrder = 'ASC' THEN tblj.EstimatedTimeOut END ASC,
 CASE WHEN @SortBy = 'ClientName' AND @SortOrder = 'ASC' THEN tblc.FirstName END ASC,
 --CASE WHEN @SortBy = 'PhoneNo' AND @SortOrder = 'ASC' THEN tblca.PhoneNumber END ASC,
 CASE WHEN @SortBy = 'Service' AND @SortOrder = 'ASC' THEN det.ServiceTypeName END ASC,
-- CASE WHEN @SortBy = 'OutSideService' AND @SortOrder = 'ASC' THEN outs.OutsideService END ASC,
 --DESC
 CASE WHEN @SortBy = 'TicketNo' AND @SortOrder = 'DESC' THEN tblj.TicketNumber END DESC,
 CASE WHEN @SortBy = 'TimeIn' AND @SortOrder = 'DESC' THEN tblj.TimeIn END DESC,
 CASE WHEN @SortBy = 'TimeOut' AND @SortOrder = 'DESC' THEN tblj.EstimatedTimeOut END DESC,
 CASE WHEN @SortBy = 'ClientName' AND @SortOrder = 'DESC' THEN tblc.FirstName END DESC,
 --CASE WHEN @SortBy = 'PhoneNo' AND @SortOrder = 'DESC' THEN tblc.PhoneNumber END DESC,
 CASE WHEN @SortBy = 'Service' AND @SortOrder = 'DESC' THEN det.ServiceTypeName END DESC,
-- CASE WHEN @SortBy = 'OutSideService' AND @SortOrder = 'DESC' THEN outs.OutsideService END DESC,
 
 CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN tblj.JobId END ASC,
 CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN tblj.JobId END ASC

 SELECT DISTINCT * FROM #Detailslist

END