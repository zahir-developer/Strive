
-- =============================================
-- Author:		Vineeth B
-- Create date: 05-09-2020
-- Description:	To show data in DetailsGrid
-- =============================================
---------------------History--------------------
-- =============================================
-- 07-09-2020, Vineeth - Add a new select for bay
--						 Add bayid col in 2dselect
-- 08-09-2020, Vineeth - Add service type name col
-- 09-09-2020, Vineeth - Add isactive condition
-- 09-10-2020, Vineeth - Add LocationId condition
-- 17-09-2020, Vineeth - Add valuedesc-Details
-- 21-09-2020, Vineeth - Add Outside service cond
--						 and order by condition
-- 29-09-2020, Vineeth - Add bracket in Details
--						 and Outside service
-- 24-11-2020, Vineeth - Add code for Upcharge
-- 09-12-2020, Vineeth - Add outside service cond
------------------------------------------------
-- =============================================

CREATE proc [StriveCarSalon].[uspGetAllDetails] --[StriveCarSalon].[uspGetAllDetails] null,null,89
(@JobDate Date = NULL, @LocationId int = NULL, @ClientId int = NULL
, @Search varchar(50)=null
)
AS
BEGIN
SELECT
BayId,
BayName
FROM tblBay
where 
IsActive=1  
and ISNULL(IsDeleted,0)=0
and (LocationId=@LocationId OR( @LocationId is null OR @LocationId=0)) and BayName like '%Detail%'
order by BayId

DROP TABLE IF EXISTS #Upcharge
SELECT tblj.JobId,tblji.Price 
INTO #Upcharge FROM tblJob tblj INNER JOIN tblJobItem tblji ON(tblj.JobId = tblji.JobId)
INNER JOIN tblService tblS ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN GetTable('ServiceType') st ON(tbls.ServiceType = st.valueid)
WHERE st.valuedesc='Upcharges' AND  
(@JobDate is null OR tblj.JobDate=@JobDate)
AND ((@LocationId is null  OR @LocationId=0)OR tblj.LocationId=@LocationId)
AND tblj.IsActive = 1 AND tbljI.IsActive = 1 AND tblS.IsActive = 1
AND ISNULL(tblj.IsDeleted,0)=0 AND ISNULL(tbljI.IsDeleted,0)=0
AND ISNULL(tblS.IsDeleted,0)=0 

DROP TABLE IF EXISTS #OutsideServices
SELECT tblj.JobId,tbls.ServiceName AS OutsideService INTO #OutsideServices FROM tblJobItem tblji INNER JOIN tblJob tblj ON tblj.JobId = tblji.JobId AND tblj.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0
AND tblji.IsActive=1 AND ISNULL(tblji.IsDeleted,0)=0
INNER JOIN tblService tbls ON tblji.ServiceId = tbls.ServiceId AND tbls.IsActive=1 AND ISNULL(tbls.IsDeleted,0)=0
INNER JOIN GetTable('ServiceType') st ON(st.valueid = tbls.ServiceType) 
AND st.valuedesc ='Outside Services'
AND (@JobDate IS NULL OR tblj.JobDate=@JobDate) AND  (@LocationId is null OR @LocationId= 0 OR tblj.LocationId =@LocationId)

DROP TABLE IF EXISTS #Details
SELECT tblj.JobId,tbls.ServiceName AS ServiceTypeName INTO #Details FROM tblJobItem tblji INNER JOIN tblJob tblj ON tblj.JobId = tblji.JobId AND tblj.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0
AND tblji.IsActive=1 AND ISNULL(tblji.IsDeleted,0)=0
INNER JOIN tblService tbls ON tblji.ServiceId = tbls.ServiceId AND tbls.IsActive=1 AND ISNULL(tbls.IsDeleted,0)=0
INNER JOIN GetTable('ServiceType') st ON(st.valueid = tbls.ServiceType) 
AND st.valuedesc ='Details'
AND (@JobDate is null OR tblj.JobDate=@JobDate) AND (@LocationId is null  OR @LocationId= 0 OR tblj.LocationId =@LocationId)


DROP TABLE IF EXISTS #AirFresheners
SELECT tblj.JobId,tbls.ServiceName AS OutsideService INTO #AirFresheners FROM tblJobItem tblji INNER JOIN tblJob tblj ON tblj.JobId = tblji.JobId AND tblj.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0
AND tblji.IsActive=1 AND ISNULL(tblji.IsDeleted,0)=0
INNER JOIN tblService tbls ON tblji.ServiceId = tbls.ServiceId AND tbls.IsActive=1 AND ISNULL(tbls.IsDeleted,0)=0
INNER JOIN GetTable('ServiceType') st ON(st.valueid = tbls.ServiceType) 
AND st.valuedesc ='Air Fresheners'
AND (@JobDate IS NULL OR tblj.JobDate=@JobDate) AND  (@LocationId is null  Or @LocationId= 0 OR tblj.LocationId =@LocationId)

DROP TABLE IF EXISTS #ServicePrice
SELECT tblj.JobId,SUM(tblji.Price)Price
INTO #ServicePrice FROM tblJob tblj INNER JOIN tblJobItem tblji ON(tblj.JobId = tblji.JobId)
INNER JOIN tblService tblS ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN GetTable('ServiceType') st ON(tbls.ServiceType = st.valueid)
WHERE (@JobDate is null OR tblj.JobDate=@JobDate)
AND ((@LocationId is null  OR @LocationId=0)OR tblj.LocationId=@LocationId)
AND tblj.IsActive = 1 AND tbljI.IsActive = 1 AND tblS.IsActive = 1
AND ISNULL(tblj.IsDeleted,0)=0 AND ISNULL(tbljI.IsDeleted,0)=0
AND ISNULL(tblS.IsDeleted,0)=0 GROUP BY tblj.JobId

SELECT 
DISTINCT
tblb.BayId,
tblb.BayName
,tblj.JobId 
,tblj.TicketNumber
,tblj.JobDate
,tblj.LocationId
,tbll.LocationName
--,tblji.JobItemId
,ISnULL(sp.Price, 0) AS Cost
,SUBSTRING(CONVERT(VARCHAR(8),tblj.TimeIn,108),0,6) AS TimeIn
,CONCAT(tblc.FirstName,' ',tblc.LastName) AS ClientName
,tblca.PhoneNumber
,SUBSTRING(CONVERT(VARCHAR(8),tblj.EstimatedTimeOut,108),0,6) AS EstimatedTimeOut
,(det.ServiceTypeName)ServiceTypeName
,cvMfr.valuedesc AS VehicleMake
,cvMo.valuedesc AS VehicleModel
,cvCo.valuedesc AS VehicleColor
,ISNULL(upc.Price,0.00) AS Upcharge
,ISNULL(outs.OutsideService,'None')AS OutsideService
,tblcv.Barcode
FROM 
tblJob tblj inner join tblClient tblc ON(tblj.ClientId = tblc.ClientId) 
inner join tblJobDetail tbljd ON(tblj.JobId = tbljd.JobId)
inner join tblClientAddress tblca ON(tblj.ClientId = tblca.ClientId)
inner join tblClientVehicle tblcv ON(tblc.ClientId = tblcv.ClientId and tblj.VehicleId = tblcv.VehicleId)
inner join strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON tblcv.VehicleMfr = cvMfr.valueid
inner join strivecarsalon.GetTable('VehicleModel') cvMo ON tblcv.VehicleModel = cvMo.valueid
inner join strivecarsalon.GetTable('VehicleColor') cvCo ON tblcv.VehicleColor = cvCo.valueid
inner join tblJobItem tblji ON(tblj.JobId = tblji.JobId)
inner join tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
right join tblBay tblb ON(tbljd.BayId = tblb.BayId)
inner join tblLocation tbll on tblj.LocationId =tbll.LocationId
inner join GetTable('ServiceType') st ON(st.valueid = tbls.ServiceType)
left join #Upcharge upc ON(tblj.JobId = upc.JobId)
left join #Details det ON(tblj.JobId = det.JobId)
left join #OutsideServices outs ON(tblj.JobId = outs.JobId)
left join #AirFresheners ar ON(tblj.JobId =ar.JobId)
left join #ServicePrice sp ON(tblj.JobId = sp.JobId)
WHERE 
 (@JobDate is null OR tblj.JobDate=@JobDate)
and 
(@LocationId is null OR  @LocationId=0 OR tblj.LocationId=@LocationId)
and
(@ClientId is null OR @ClientId=0 OR tblc.ClientId=@ClientId)
and
st.valuedesc in('Details','Outside Services','Air Fresheners')
and
tblj.IsActive=1
and
tbljd.IsActive=1
and
tblji.IsActive=1
and
tblb.IsActive=1
and
tblcv.IsActive=1
and
ISNULL(tblb.IsDeleted,0)=0
and
ISNULL(tblj.IsDeleted,0)=0
and
ISNULL(tbljd.IsDeleted,0)=0
and
ISNULL(tblji.IsDeleted,0)=0
and
ISNULL(tblcv.IsDeleted,0)=0
and 
 (@Search is null or tblj.TicketNumber like '%'+@Search+'%'
 or det.ServiceTypeName like '%'+@Search+'%' or cvMfr.valuedesc like '%'+@Search+'%'
 or cvMo.valuedesc like '%'+@Search+'%'
 or cvCo.valuedesc like '%'+@Search+'%')
order by tblj.JobId

END
GO

