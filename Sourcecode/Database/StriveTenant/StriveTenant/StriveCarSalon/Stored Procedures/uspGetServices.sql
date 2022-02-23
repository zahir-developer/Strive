---------------------History---------------------------
-- ====================================================
-- 08-jun-2021, shalini - pagenumber and count for nullquery changes

--2021-06-16,   shalini  -removed wildcard from query					 

-------------------------------------------------------
--EXEC [StriveCarSalon].[uspGetServices] null,1,'upcharge',1,null,'ASC','ServiceName',1

CREATE PROCEDURE [StriveCarSalon].[uspGetServices]

(@ServiceId int =NULL,
@locationId int =NULL,
@Query NVARCHAR(50) = NULL,
@PageNo INT = NULL,
@PageSize INT = NULL,
@SortOrder VARCHAR(5) = 'ASC', 
@SortBy VARCHAR(50) = NULL,
@Status bit = null)
As
Begin
DECLARE @Skip INT = 0;
IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

IF @PageSize is NULL
BEGIN
SET @PageSize = (Select count(1) from tblService);
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
Print @PageSize
Print @PageNo
Print @Skip
END
Drop TABLE IF EXISTS #GetAllServices

SELECT 
	svc.ServiceId,
	svc.ServiceType as ServiceTypeId,
	svc.CommisionType as CommissionTypeId,
	svc.ParentServiceId,
	cv.valuedesc as ServiceType,
	ct.valuedesc as CommisionType,
	svc.CommissionCost as CommissionCost,
	svc.Commision,
	svc.Upcharges,
	svc.ServiceName,
	svc.Cost,	
	svc.ServiceCategory,
	svc.IsCeramic,
	svc.LocationId,
	--loc.locationName,
	--added price
		svc.Price,
	svc.Description,	
	svc.DiscountServiceType,
	svc.DiscountType,
	svc.EstimatedTime,
	 isnull(svc.IsActive,1) as IsActive
	into #GetAllServices
	FROM tblService svc 
	--INNER JOIN [tblLocation] as loc ON (svc.LocationId = loc.LocationId)
	LEFT JOIN GetTable('ServiceType') cv ON (svc.ServiceType = cv.valueid)
	LEFT JOIN GetTable('CommisionType') ct ON (svc.CommisionType = ct.valueid)	
	LEFT JOIN GetTable('ServiceType') c ON (svc.DiscountServiceType = c.valueid)
WHERE (@ServiceId is null or svc.ServiceId = @ServiceId)
--AND ((svc.locationId = @locationId) OR (@locationId is NULL AND @locationId = 0))
AND isnull(svc.IsDeleted,0)=0
AND ((@Query is null OR (cv.valuedesc like '%'+@Query+'%') OR svc.ServiceName  like '%'+ @Query+'%')) 
AND (isnull(svc.IsActive,1)= @Status or (@Status is null))

  Group By
  svc.ServiceId,
	svc.ServiceType ,
	svc.CommisionType ,
	svc.ParentServiceId,
	cv.valuedesc ,
		svc.ServiceCategory,
	svc.IsCeramic,
	ct.valuedesc ,
	svc.CommissionCost,
	svc.Commision,
	svc.Upcharges,
	svc.ServiceName,
	svc.Cost,
	svc.LocationId,
	--loc.LocationName,
	--added price
		svc.Price,
	svc.Description,	
	svc.DiscountServiceType,
	svc.DiscountType,	
	svc.EstimatedTime,

	svc.IsActive
ORDER BY 
CASE WHEN @SortBy = 'ServiceName' AND @SortOrder='ASC' THEN svc.ServiceName END ASC,
CASE WHEN @SortBy = 'ServiceType' AND @SortOrder='ASC' THEN cv.valuedesc END ASC,
CASE WHEN @SortBy = 'Cost' AND @SortOrder='ASC' THEN svc.Cost END ASC,
--added price

CASE WHEN @SortBy = 'Price' AND @SortOrder='ASC' THEN svc.Price END ASC,
CASE WHEN @SortBy = 'IsActive' AND @SortOrder='ASC' THEN svc.IsActive END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC
CASE WHEN @SortBy = 'ServiceType' AND @SortOrder='DESC' THEN cv.valuedesc END DESC,
CASE WHEN @SortBy = 'Cost' AND @SortOrder='DESC' THEN svc.Cost END DESC,
--added price

CASE WHEN @SortBy = 'Price' AND @SortOrder='DESC' THEN svc.Price END DESC,
CASE WHEN @SortBy = 'IsActive' AND @SortOrder='DESC' THEN svc.IsActive END DESC,
CASE WHEN @SortBy = 'ServiceName' AND @SortOrder='DESC' THEN svc.ServiceName END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN svc.ServiceName END ASC,

CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN svc.ServiceId END DESC
  
OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY

select * from #GetAllServices


IF @Query IS NULL OR @Query = ''
BEGIN 
select COUNT(1) as [Count] 
	FROM tblService svc 
	--INNER JOIN [tblLocation] as loc ON (svc.LocationId = loc.LocationId)
	LEFT JOIN GetTable('ServiceType') cv ON (svc.ServiceType = cv.valueid)
	LEFT JOIN GetTable('CommisionType') ct ON (svc.CommisionType = ct.valueid)	
	LEFT JOIN GetTable('ServiceType') c ON (svc.DiscountServiceType = c.valueid)
WHERE  (@ServiceId is null or svc.ServiceId = @ServiceId) and   isnull(svc.IsDeleted,0)=0
AND (isnull(svc.IsActive,1)=@Status or @Status is null  ) 

END

IF @Query IS Not NULL AND @Query != ''
BEGIN
SELECT 
	COUNT(1) as [Count] 
	FROM tblService svc 
	--INNER JOIN [tblLocation] as loc ON (svc.LocationId = loc.LocationId)
	LEFT JOIN GetTable('ServiceType') cv ON (svc.ServiceType = cv.valueid)
	LEFT JOIN GetTable('CommisionType') ct ON (svc.CommisionType = ct.valueid)	
	LEFT JOIN GetTable('ServiceType') c ON (svc.DiscountServiceType = c.valueid)
WHERE (@ServiceId is null or svc.ServiceId = @ServiceId)
--AND ((svc.locationId = @locationId) OR (@locationId is NULL AND @locationId = 0))
AND isnull(svc.IsDeleted,0)=0
AND ((@Query is null OR (cv.valuedesc like '%'+@Query+'%') OR svc.ServiceName  like '%'+ @Query+'%')) 
AND (isnull(svc.IsActive,1)= @Status or (@Status is null))
 
END


end


