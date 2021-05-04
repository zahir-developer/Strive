﻿CREATE proc [StriveCarSalon].[uspGetServices]
--EXEC [StriveCarSalon].[uspGetServices] null,null,'list2',null,null,'ASC','ServiceName',1
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
SET @PageSize = (Select count(1) from StriveCarSalon.tblService);
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
	svc.LocationId,
	--added price
		svc.Price,
	svc.Description,	
	svc.DiscountServiceType,
	svc.DiscountType,
	isnull(svc.IsActive,1) as IsActive
	into #GetAllServices
	FROM [StriveCarSalon].tblService svc 
	INNER JOIN [StriveCarSalon].[tblLocation] as loc ON (svc.LocationId = loc.LocationId)
	LEFT JOIN [striveCarSalon].GetTable('ServiceType') cv ON (svc.ServiceType = cv.valueid)
	LEFT JOIN [striveCarSalon].GetTable('CommisionType') ct ON (svc.CommisionType = ct.valueid)	
	LEFT JOIN [striveCarSalon].GetTable('ServiceType') c ON (svc.DiscountServiceType = c.valueid)
WHERE  (@ServiceId is null or svc.ServiceId = @ServiceId) and   isnull(svc.IsDeleted,0)=0
AND

 (@Query is null or cv.valuedesc like '%'+@Query+'%'
  or svc.ServiceName  like '%'+@Query+'%') AND  
   (isnull(svc.IsActive,1)=@Status or @Status is null  ) 
  Group By
  svc.ServiceId,
	svc.ServiceType ,
	svc.CommisionType ,
	svc.ParentServiceId,
	cv.valuedesc ,
	ct.valuedesc ,
	svc.CommissionCost,
	svc.Commision,
	svc.Upcharges,
	svc.ServiceName,
	svc.Cost,
	svc.LocationId,
	--added price
		svc.Price,
	svc.Description,	
	svc.DiscountServiceType,
	svc.DiscountType,
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
select count(1) as Count from StriveCarSalon.tblService where 
ISNULL(IsDeleted,0) = 0 

END

IF @Query IS Not NULL AND @Query != ''
BEGIN
select count(1) as Count from #GetAllServices
END


end


