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
	   IsActive,
	   IsDeleted,
	   TaxRate,
	   SiteUrl,
	   Currency,
	   Facebook,
	   Twitter,
	   Instagram,
	   WifiDetail,
	   WorkhourThreshold,
	   StartTime,
	   EndTime
	   

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
	   tblla.IsActive,			
	   tblla.Country,				
	   tblla.Longitude,
	   tblla.Latitude,
	   tblla.WeatherLocationId,
	   tblla.IsDeleted
	   from [StriveCarSalon].[tblLocation] tbll inner join [StriveCarSalon].[tblLocationAddress] tblla
		   ON(tbll.LocationId = tblla.LocationId)
           WHERE tbll.LocationId = @tblLocationId AND
		   isnull(tblla.IsActive,1) = 1 AND
		isnull(tblla.isDeleted,0) = 0  

SELECT 
DrawerId,
DrawerName,
LocationId,
IsActive,
IsDeleted

FROM [StriveCarSalon].[tblDrawer]
WHERE LocationId =@tblLocationId AND
isnull(IsActive,1) = 1 AND
isnull(isDeleted,0) = 0
END