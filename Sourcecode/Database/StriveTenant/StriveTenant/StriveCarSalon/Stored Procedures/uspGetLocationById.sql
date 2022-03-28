-- =============================================================
-- Author:         Vineeth.B
-- Created date:   2020-07-01
-- Description:    Get Location Details By LocationId
-- Example: [StriveCarSalon].[uspGetLocationById] 1
-- =============================================================

----------------------------History-----------------------------
-- =============================================================
-- 16-09-2020, Vineeth - Removed tblDrawer table
-- 17-09-2020, Zahir - Added back tblDrawer table
-- 30-09-2020, Vineeth - Modified StartTime and EndTime format
--					     Removed IsActive and IsDelete from 
--						 tbllocation,tbllocationdrawer,drawer table
-- 22-12-2020, Zahir - Modified the Washtime logic - Modified WashRole logic and Wash job count logic 
-- 14-06-2021, Shalini - Added back tblMerchantDetail table
-- 14-06-2021, Zahir - Added IsActive/IsDeleted condition for locationEmail query.
--					 - Added MerchantDetailId, Password column for MerchantDetail query.


----------------------------------------------------------------
-- =============================================================

CREATE PROCEDURE [StriveCarSalon].[uspGetLocationById] 
    (
     @tblLocationId int)
AS 
BEGIN

DECLARE @DefaultWashTime INT = 25;

Declare @WashId INT = (Select valueid from GetTable('JobType') where valuedesc='Wash')
Declare @WashRole INT = (Select RoleMasterId from tblRoleMaster WHERE RoleName='Washer')


DROP TABLE IF EXISTS #WashRoleCount
SELECT tblL.LocationId, COUNT(1) Washer
INTO #WashRoleCount FROM tblTimeClock tblTC Left JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
--left JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
--AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole AND tblTC.EventDate = GETDATE() 
--AND tblJ.JobType=@WashId 
GROUP BY tblL.LocationId


DROP TABLE IF EXISTS #CarsCount

Select tbll.LocationId, count(1) Cars into #CarsCount
from tblJob tblj
INNER JOIN tblLocation tbll on tbll.LocationId = tblj.LocationId
INNER join GetTable('JobStatus') GT on GT.id = tblj.JobStatus and GT.valuedesc = 'In Progress' and tblj.JobType = @WashId
WHERE ISNULL(tbll.IsActive, 1) = 1 AND ISNULL(tbll.IsDeleted, 0) = 0 AND ISNULL(tblj.IsDeleted, 0) = 0
AND tblj.JobType = @WashId AND tblj.JobDate = GETDATE()
GROUP by tbll.LocationId
 

DROP TABLE  IF EXISTS #WashTime

(SELECT tbll.LocationId,
CASE
	   WHEN wr.Washer <=3 AND cc.Cars <=1 THEN @DefaultWashTime
	   WHEN wr.Washer <=3 AND cc.Cars > 1 THEN (@DefaultWashTime+(cc.Cars - 1) * 8) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer <=6 AND cc.Cars <=1 THEN @DefaultWashTime
	   WHEN wr.Washer <=6 AND cc.Cars >1 THEN (@DefaultWashTime+(cc.Cars - 1)*7) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer <=9 AND cc.Cars <=1 THEN @DefaultWashTime
	   WHEN wr.Washer <=9 AND cc.Cars >1 THEN (@DefaultWashTime+(cc.Cars - 1)*6) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer <=11 AND cc.Cars <=3 THEN @DefaultWashTime
	   WHEN wr.Washer <=11 AND cc.Cars >3 THEN (@DefaultWashTime+(cc.Cars - 3)*5) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer >=12 AND wr.Washer<=15 AND cc.Cars <=5 THEN @DefaultWashTime
	   WHEN wr.Washer >=12 AND wr.Washer<=15 AND cc.Cars >5  THEN (@DefaultWashTime+(cc.Cars - 5)*3) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer >=16 AND wr.Washer<=21 AND cc.Cars <=5 THEN @DefaultWashTime
	   WHEN wr.Washer >=16 AND wr.Washer<=21 AND cc.Cars >5  THEN (@DefaultWashTime+(cc.Cars - 6)*2) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer >=22 AND wr.Washer<=26 AND cc.Cars <=5 THEN @DefaultWashTime
	   WHEN wr.Washer >=22 AND wr.Washer<=26 AND cc.Cars >5  THEN (@DefaultWashTime+(cc.Cars - 5)*2) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer >26 AND cc.Cars <=7 THEN @DefaultWashTime
	   WHEN wr.Washer >26 AND cc.Cars >7  THEN (@DefaultWashTime+(cc.Cars - 7)*2) + ((cc.Cars+tbllo.OffSet1)*tbllo.OffSet1On)
	   WHEN wr.Washer is NULL and cc.Cars is NULL THEN @DefaultWashTime
	   END AS WashTimeMinutes
	   INTO #WashTime
	   
	   FROM tblLocation tbll
INNER JOIN #WashRoleCount wr ON(tbll.LocationId = wr.LocationId)
INNER JOIN #CarsCount cc on tbll.LocationId = cc.LocationId
LEFT JOIN tblLocationOffSet tbllo ON(tbll.LocationId = tbll.LocationId)
WHERE ISNULL(tbll.IsActive,1) = 1 AND
ISNULL(tbll.IsDeleted,0) = 0 --AND ISNULL(tbllo.IsDeleted,0) = 0
)

SELECT DISTINCT
       tbll.LocationId,
	   tbll.LocationType,
	   tbll.LocationName,
	   tbll.LocationDescription,
	   CONVERT(INT, ISNULL(wt.WashTimeMinutes, @DefaultWashTime)) as WashTimeMinutes,
	   tbll.ColorCode,
	   tbll.IsFranchise,
	   tbll.TaxRate,
	   tbll.SiteUrl,
	   tbll.Currency,
	   tbll.Facebook,
	   tbll.Twitter,
	   tbll.Instagram,
	   tbll.WifiDetail,
	   tbll.WorkhourThreshold,
	   CONVERT(VARCHAR(5),tbll.StartTime,108) AS StartTime,
	   CONVERT(VARCHAR(5),tbll.EndTime,108) AS EndTime
	   

FROM [tblLocation]  tbll
LEFT JOIN #WashTime wt
ON(tbll.LocationId = wt.LocationId)
WHERE 
tbll.LocationId = @tblLocationId AND
isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0  

select 
tblla.LocationAddressId	,
	   tblla.LocationId	,			
	   tblla.Address1,				
	   tblla.Address2,				
	   tblla.PhoneNumber,		
	   tblla.PhoneNumber2,		
	   tblla.Email,				
	   tblla.City,					
	   tblla.State,				
	   tblla.Zip,				
	   tblla.Country,				
	   tblla.Longitude,
	   tblla.Latitude,
	   tblla.WeatherLocationId
	   from [tblLocation] tbll inner join [tblLocationAddress] tblla
		   ON(tbll.LocationId = tblla.LocationId)
           WHERE tbll.LocationId = @tblLocationId AND
		   isnull(tblla.IsActive,1) = 1 AND
		isnull(tblla.isDeleted,0) = 0 


Select * from tblLocationEmail where locationId = @tblLocationId AND isnull(IsActive,1) = 1 AND isnull(isDeleted,0) = 0
		
SELECT 
DrawerId,
DrawerName,
LocationId

FROM [tblDrawer]
WHERE LocationId =@tblLocationId AND
isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0

SELECT Top 1 tbllo.LocationOffsetId,
tbllo.LocationId,
tbllo.OffSet1,
tbllo.OffSet1On,
tbllo.OffSetA,
tbllo.OffSetB,
tbllo.OffSetB,
tbllo.OffSetC,
tbllo.OffSetD,
tbllo.OffSetE,
tbllo.OffSetF	   
FROM [tblLocationOffset] tbllo
WHERE	tbllo.LocationId =@tblLocationId AND
isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0

select Top 1
MerchantDetailId,
tblm.UserName,
tblm.MID,
tblm.Password,
tblm.LocationId,
tblm.URL
from tblMerchantDetail tblm
where tblm.LocationId = @tblLocationId
AND isnull(IsActive,1) = 1 AND isnull(isDeleted,0) = 0

END
