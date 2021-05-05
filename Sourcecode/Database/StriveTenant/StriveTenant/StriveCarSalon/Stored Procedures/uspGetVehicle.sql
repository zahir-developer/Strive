﻿CREATE PROC [StriveCarSalon].[uspGetVehicle] --[StriveCarSalon].[uspGetVehicle] 'bobcat',1,10,'asc','Vehiclenumber'
@Query NVARCHAR(50) = NULL,
@PageNo INT = NULL,
@PageSize INT = NULL,	
@SortOrder VARCHAR(5) = 'ASC',
@SortBy VARCHAR(100) = NULL
AS
BEGIN
DECLARE @Skip INT = 0;
IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

IF @PageSize is NULL
BEGIN
SET @PageSize = (Select count(1) from StriveCarSalon.tblClient);
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
Print @PageSize
Print @PageNo
Print @Skip
END

Drop TABLE IF EXISTS #GetAllVehicle
SELECT
	cvl.VehicleId AS ClientVehicleId
	,cl.ClientId
	 ,cl.FirstName + ' '+cl.LastName as ClientName
	,cvl.VehicleNumber
	,cvl.VehicleMfr AS VehicleMakeId
	,cvMfr.MakeValue AS VehicleMake
	,cvl.VehicleModel AS VehicleModelId
	,cvmo.ModelValue AS ModelName
	,cvCo.valuedesc AS Color
	,cvl.VehicleColor AS ColorId
	,cvl.Upcharge
	,cvl.Barcode
	,tblm.MembershipName  into #GetAllVehicle
FROM
strivecarsalon.tblclient cl
INNER JOIN strivecarsalon.tblClientVehicle cvl ON cl.ClientId = cvl.ClientId AND ISNULL(cvl.IsDeleted,0)=0 AND ISNULL(cvl.IsActive,1)=1
LEFT JOIN strivecarsalon.tblClientVehicleMembershipDetails cvmd ON cvl.VehicleId = cvmd.ClientVehicleId AND ISNULL(cvmd.IsDeleted, 0) = 0 AND ISNULL(cvmd.IsActive,1) = 1
LEFT JOIN strivecarsalon.tblmembership tblm on cvmd.MembershipId = tblm.MembershipId AND ISNULL(tblm.IsDeleted, 0) = 0 AND ISNULL(tblm.IsActive,1) = 1  
LEFT JOIN strivecarsalon.tblVehicleMake cvMfr ON cvl.VehicleMfr = cvMfr.MakeId
LEFT JOIN strivecarsalon.tblVehicleModel cvMo ON cvl.VehicleModel = cvMo.ModelId
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1 AND

 ((@Query is null or cl.FirstName  like '%'+@Query+'%') OR
  (@Query is null or cl.LastName  like '%'+@Query+'%') OR
  (@Query is null or cvl.Barcode  like '%'+@Query+'%') OR
  (@Query is null or cvMfr.MakeValue  like '%'+@Query+'%') OR
  (@Query is null or cvmo.ModelValue  like '%'+@Query+'%') OR
  (@Query is null or cvCo.valuedesc  like '%'+@Query+'%') OR
  (@Query is null or cvl.VehicleNumber  like '%'+@Query+'%'))
GROUP BY

	cvl.VehicleId
	,cl.ClientId
	 ,cl.FirstName ,
	 cl.LastName 
	,cvl.VehicleNumber
	,cvl.VehicleMfr 
	,cvMfr.MakeValue 
	,cvl.VehicleModel 
	,cvmo.ModelValue 
	,cvCo.valuedesc 
	,cvl.VehicleColor 
	,cvl.Upcharge
	,cvl.Barcode
	,tblm.MembershipName


order by 
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='ASC' THEN cl.FirstName END ASC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='ASC' THEN cl.LastName END ASC,
CASE WHEN @SortBy = 'VehicleNumber' AND @SortOrder='ASC' THEN cvl.VehicleNumber END ASC,
CASE WHEN @SortBy = 'Color' AND @SortOrder='ASC' THEN cvCo.valuedesc END ASC,
CASE WHEN @SortBy = 'Make' AND @SortOrder='ASC' THEN cvMfr.MakeValue END ASC,
CASE WHEN @SortBy = 'Model' AND @SortOrder='ASC' THEN cvmo.ModelValue END ASC,
CASE WHEN @SortBy = 'Barcode' AND @SortOrder='ASC' THEN cvl.Barcode END ASC,
CASE WHEN @SortBy = 'Membership' AND @SortOrder='ASC' THEN tblm.MembershipName END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='DESC' THEN cl.FirstName END DESC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='DESC' THEN cl.LastName END DESC,
CASE WHEN @SortBy = 'VehicleNumber' AND @SortOrder='DESC' THEN cvl.VehicleNumber END DESC,
CASE WHEN @SortBy = 'Color' AND @SortOrder='DESC' THEN cvCo.valuedesc END DESC,
CASE WHEN @SortBy = 'Make' AND @SortOrder='DESC' THEN cvMfr.MakeValue END DESC,
CASE WHEN @SortBy = 'Model' AND @SortOrder='DESC' THEN cvmo.ModelValue END DESC,
CASE WHEN @SortBy = 'Barcode' AND @SortOrder='DESC' THEN cvl.Barcode END DESC,
CASE WHEN @SortBy = 'Membership' AND @SortOrder='DESC' THEN tblm.MembershipName END DESC,
CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN cl.FirstName END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN cl.ClientId END DESC
  

OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY
select * from #GetAllVehicle


IF @Query IS NULL OR @Query = ''
BEGIN 
select count(1) as Count from StriveCarSalon.tblClientVehicle where 
ISNULL(IsDeleted,0) = 0 

END

IF @Query IS Not NULL AND @Query != ''
BEGIN
select count(1) as Count from #GetAllVehicle
END



END
