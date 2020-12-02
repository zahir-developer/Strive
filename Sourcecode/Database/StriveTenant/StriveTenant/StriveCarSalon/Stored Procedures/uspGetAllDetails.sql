


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
------------------------------------------------
-- =============================================
CREATE proc [StriveCarSalon].[uspGetAllDetails] --[StriveCarSalon].[uspGetAllDetails] '2020-11-20',null
(@JobDate Date, @LocationId int = NULL)
AS
BEGIN

SELECT
DISTINCT
tblb.BayId
,tblb.BayName
FROM 
tblJobDetail tbljd
right join tblBay tblb ON(tbljd.BayId = tblb.BayId)
where 
tblb.IsActive=1  
and tbljd.IsActive=1
and ISNULL(tblb.IsDeleted,0)=0
and ISNULL(tbljd.IsDeleted,0)=0
and (tblb.LocationId=@LocationId OR (@LocationId is null OR @LocationId=0))
order by tblb.BayId

DROP TABLE IF EXISTS #Upcharge
SELECT tblj.JobId,tblji.Price 
INTO #Upcharge FROM tblJob tblj INNER JOIN tblJobItem tblji ON(tblj.JobId = tblji.JobId)
INNER JOIN tblService tblS ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN GetTable('ServiceType') st ON(tbls.ServiceType = st.valueid)
WHERE st.valuedesc='Upcharges' AND  
(tblj.JobDate is null OR tblj.JobDate=@JobDate)
AND ((@LocationId is null  OR @LocationId=0)OR tblj.LocationId=@LocationId)
AND tblj.IsActive = 1 AND tbljI.IsActive = 1 AND tblS.IsActive = 1
AND ISNULL(tblj.IsDeleted,0)=0 AND ISNULL(tbljI.IsDeleted,0)=0
AND ISNULL(tblS.IsDeleted,0)=0 

SELECT 
tblb.BayId,
tblb.BayName
,tblj.JobId 
,tblj.TicketNumber
,SUBSTRING(CONVERT(VARCHAR(8),tblj.TimeIn,108),0,6) AS TimeIn
,CONCAT(tblc.FirstName,'',tblc.LastName) AS ClientName
,tblca.PhoneNumber
,SUBSTRING(CONVERT(VARCHAR(8),tblj.EstimatedTimeOut,108),0,6) AS EstimatedTimeOut
,tbls.ServiceName
,st.valuedesc AS ServiceTypeName
,cvMfr.valuedesc AS VehicleMake
,cvMo.valuedesc AS VehicleModel
,cvCo.valuedesc AS VehicleColor
,ISNULL(upc.Price,0.00) AS Upcharge
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
inner join GetTable('ServiceType') st ON(st.valueid = tbls.ServiceType)
left join #Upcharge upc ON(tblj.JobId = upc.JobId)
WHERE 
(tblj.JobDate is null OR tblj.JobDate=@JobDate)
and 
((@LocationId is null  OR @LocationId=0)OR tblj.LocationId=@LocationId)
and
(st.valuedesc='Details')
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
order by tblj.JobId
END
GO

