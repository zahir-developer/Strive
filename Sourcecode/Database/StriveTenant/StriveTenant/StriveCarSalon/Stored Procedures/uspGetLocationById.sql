
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

CREATE PROCEDURE [StriveCarSalon].[uspGetLocationById]
    (
     @tblLocationId int)
AS 
BEGIN
SELECT 
       LocationId,
	   LocationType,
	   LocationName,
	   LocationDescription,
	   WashTimeMinutes,
	   ColorCode,
	   IsFranchise,
	   TaxRate,
	   SiteUrl,
	   Currency,
	   Facebook,
	   Twitter,
	   Instagram,
	   WifiDetail,
	   WorkhourThreshold,
	   CONVERT(VARCHAR(5),StartTime,108) AS StartTime,
	   CONVERT(VARCHAR(5),EndTime,108) AS EndTime
	   

FROM [StriveCarSalon].[tblLocation]  
WHERE isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0  AND
LocationId = @tblLocationId

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
