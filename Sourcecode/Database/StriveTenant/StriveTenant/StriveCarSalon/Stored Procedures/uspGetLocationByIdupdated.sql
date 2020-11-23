
-- =============================================================
-- Author:         Vineeth.B
-- Created date:   2020-07-01
-- Description:    Get Location Details By LocationId
-- =============================================================

----------------------------History-----------------------------
-- =============================================================
-- 16-09-2020, Vineeth - Removed tblDrawer table
-- 17-09-2020, Zahir - Added back tblDrawer table
-- 30-09-2020, Vineeth - Modified StartTime and EndTime format
--					     Removed IsActive and IsDelete from 
--						 tbllocation,tbllocationdrawer,drawer table
----------------------------------------------------------------
-- =============================================================

CREATE PROCEDURE [StriveCarSalon].[uspGetLocationByIdupdated] --2050
    (
     @tblLocationId int)
AS 
BEGIN 
DECLARE @WashType INT =(SELECT valueid from GetTable('JobType') WHERE valuedesc='Wash')
DECLARE @WashRole INT =(SELECT RoleMasterId from tblRoleMaster WHERE RoleName='Wash')

DROP TABLE IF EXISTS #WashTime
SELECT tblL.LocationId,COUNT(tblTC.EmployeeId) Washer,COUNT(tblJ.JobId) CarCount
INTO #WashTime FROM tblTimeClock tblTC INNER JOIN
tblLocation tblL ON(tblTC.LocationId = tblL.LocationId) 
INNER JOIN tblJob tblJ ON(tblJ.LocationId = tblL.LocationId)
WHERE tblL.IsActive=1 AND ISNULL(tblL.IsDeleted,0)=0 
AND tblTC.IsActive=1 AND ISNULL(tblTC.IsDeleted,0)=0
AND tblJ.IsActive=1 AND ISNULL(tblJ.IsDeleted,0)=0
AND tblTC.RoleId =@WashRole  
AND tblJ.JobType=@WashType GROUP BY tblL.LocationId





SELECT 
       tbll.LocationId,
	   --wt.Washer,
	   --wt.CarCount,
	   tbll.LocationType,
	   tbll.LocationName,
	   tbll.LocationDescription,
	   --tbll.WashTimeMinutes,
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
	   END AS WashTimeMinutes,
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
	   

FROM [StriveCarSalon].[tblLocation] tbll
LEFT JOIN #WashTime wt ON(tbll.LocationId = wt.LocationId)
LEFT JOIN [StriveCarSalon].[tblLocationOffSet] tbllo
ON(tbll.LocationId = tbllo.LocationId)
WHERE isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0  AND
tbll.LocationId = @tblLocationId

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
	   from [StriveCarSalon].[tblLocation] tbll inner join [StriveCarSalon].[tblLocationAddress] tblla
		   ON(tbll.LocationId = tblla.LocationId)
           WHERE tbll.LocationId = @tblLocationId AND
		   isnull(tblla.IsActive,1) = 1 AND
		isnull(tblla.isDeleted,0) = 0 
		
SELECT 
DrawerId,
DrawerName,
LocationId

FROM [StriveCarSalon].[tblDrawer]
WHERE LocationId =@tblLocationId AND
isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0

END