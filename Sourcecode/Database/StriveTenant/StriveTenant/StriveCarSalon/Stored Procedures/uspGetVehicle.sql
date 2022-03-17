/****** Object:  StoredProcedure [StriveCarSalon].[uspGetVehicle]    Script Date: 14-03-2022 08:04:18 PM ******/

---------------------History---------------------------
-- ====================================================
-- 21-06-2021 - Shalini - pagenumber and count for nullquery changes 					 
-- 14-03-2022 - Zahir H - Membership canceled status added.	
-- 16-03-2022 - Zahir H - Minor Fixes - Latest Inactive membership details added.

-------------------------------------------------------
--[StriveCarSalon].[uspGetVehicle] null,1,10,null,'VehicleNumber'

CREATE PROCEDURE [StriveCarSalon].[uspGetVehicle]
@Query NVARCHAR(50) = NULL,
@PageNo INT = 1,
@PageSize INT = 100,	
@SortOrder VARCHAR(5) = 'ASC',
@SortBy VARCHAR(100) = NULL
AS
BEGIN

/*DECLARE @Query NVARCHAR(50) = 'Zahir',
@PageNo INT = 1,
@PageSize INT = 5,	
@SortOrder VARCHAR(5) = 'ASC',
@SortBy VARCHAR(100) = NULL
*/

DECLARE @Skip INT = 0;
IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

IF @PageSize is NULL
BEGIN
SET @PageSize = (Select count(1) from tblClient);
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
	into #GetAllVehicle
FROM tblClientVehicle cvl 
LEFT JOIN tblClient cl ON cl.ClientId = cvl.ClientId AND ISNULL(cvl.IsDeleted,0)=0 AND ISNULL(cvl.IsActive,1)=1
LEFT JOIN tblVehicleMake cvMfr ON cvl.VehicleMfr = cvMfr.MakeId
LEFT JOIN tblVehicleModel cvMo ON cvl.VehicleModel = cvMo.ModelId and cvMo.MakeId = cvMfr.MakeId
LEFT JOIN GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1 AND
 ((@Query is null or cl.FirstName  like @Query+'%') OR
  (@Query is null or cl.LastName  like @Query+'%') OR  
   (CONCAT_WS(' ',cl.FirstName,cl.LastName) like '%'+@Query+'%')OR
  (@Query is null or cvl.Barcode  like @Query+'%') OR
  (@Query is null or cvMfr.MakeValue  like @Query+'%') OR
  (@Query is null or cvmo.ModelValue  like @Query+'%') OR
  (@Query is null or cvCo.valuedesc  like @Query+'%') OR
  (@Query is null or cvl.VehicleNumber  like @Query+'%') --OR
  --(@Query is null or tblm.MembershipName  like @Query+'%')
  )
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
	
order by 
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='ASC' THEN cl.FirstName END ASC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='ASC' THEN cl.LastName END ASC,
CASE WHEN @SortBy = 'VehicleNumber' AND @SortOrder='ASC' THEN cvl.VehicleNumber END ASC,
CASE WHEN @SortBy = 'Color' AND @SortOrder='ASC' THEN cvCo.valuedesc END ASC,
CASE WHEN @SortBy = 'Make' AND @SortOrder='ASC' THEN cvMfr.MakeValue END ASC,
CASE WHEN @SortBy = 'Model' AND @SortOrder='ASC' THEN cvmo.ModelValue END ASC,
CASE WHEN @SortBy = 'Barcode' AND @SortOrder='ASC' THEN cvl.Barcode END ASC,
--CASE WHEN @SortBy = 'Membership' AND @SortOrder='ASC' THEN tblm.MembershipName END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='DESC' THEN cl.FirstName END DESC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='DESC' THEN cl.LastName END DESC,
CASE WHEN @SortBy = 'VehicleNumber' AND @SortOrder='DESC' THEN cvl.VehicleNumber END DESC,
CASE WHEN @SortBy = 'Color' AND @SortOrder='DESC' THEN cvCo.valuedesc END DESC,
CASE WHEN @SortBy = 'Make' AND @SortOrder='DESC' THEN cvMfr.MakeValue END DESC,
CASE WHEN @SortBy = 'Model' AND @SortOrder='DESC' THEN cvmo.ModelValue END DESC,
CASE WHEN @SortBy = 'Barcode' AND @SortOrder='DESC' THEN cvl.Barcode END DESC,
--CASE WHEN @SortBy = 'Membership' AND @SortOrder='DESC' THEN tblm.MembershipName END DESC,
CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN cl.FirstName END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN cl.ClientId END DESC
  

OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY


DROP TABLE IF EXISTS #MembershipDetail
select cvmd.ClientMembershipId, cvmd.MembershipId, v.ClientVehicleId,  cvmd.DocumentId, cvmd.IsDeleted into #MembershipDetail from #GetAllVehicle v
LEFT JOIN tblClientVehicleMembershipDetails cvmd ON v.ClientVehicleId = cvmd.ClientVehicleId  AND ISNULL(cvmd.IsActive,1) = 1
ORDER BY cvmd.ClientMembershipId DESC


DROP TABLE IF EXISTS #MembershipInactiveDetail
select TOP 1 cvmd.ClientMembershipId, cvmd.MembershipId, v.ClientVehicleId,  cvmd.DocumentId into #MembershipInactiveDetail from #GetAllVehicle v
LEFT JOIN #MembershipDetail cvmd ON v.ClientVehicleId = cvmd.ClientVehicleId AND ISNULL(cvmd.IsDeleted, 0) = 1
ORDER BY cvmd.ClientMembershipId DESC

--Select * from #MembershipDetail
--Select * from #MembershipInactiveDetail


select v.ClientVehicleId
	, v.ClientId
	, v.ClientName
	, v.VehicleNumber
	,v.VehicleMakeId
	,v.VehicleMake
	,v.VehicleModelId
	,v.ModelName
	,v.Color
	,v.ColorId
	,v.Upcharge
	,v.Barcode
	,cvmd.DocumentId
	,tblm.MembershipName
	,Incvmd.ClientMembershipId as InActiveClientMembershipId
from #GetAllVehicle v
LEFT JOIN #MembershipInactiveDetail Incvmd ON v.ClientVehicleId = Incvmd.ClientVehicleId --AND ISNULL(cvmd.IsDeleted, 0) = 0 AND ISNULL(cvmd.IsActive,1) = 1
LEFT JOIN #MembershipDetail cvmd ON v.ClientVehicleId = cvmd.ClientVehicleId AND ISNULL(cvmd.IsDeleted, 0) = 0 --AND ISNULL(cvmd.IsActive,1) = 1
LEFT JOIN tblMembership tblm ON cvmd.MembershipId = tblm.MembershipId AND ISNULL(tblm.IsDeleted, 0) = 0 AND ISNULL(tblm.IsActive,1) = 1

ORDER BY cvmd.ClientMembershipId DESC

IF @Query IS NULL OR @Query = ''
BEGIN 
select count(1) as Count 
FROM tblClientVehicle cvl 
LEFT JOIN tblClient cl ON cl.ClientId = cvl.ClientId AND ISNULL(cvl.IsDeleted,0)=0 AND ISNULL(cvl.IsActive,1)=1
LEFT JOIN  tblClientVehicleMembershipDetails cvmd ON cvl.VehicleId = cvmd.ClientVehicleId AND ISNULL(cvmd.IsDeleted, 0) = 0 AND ISNULL(cvmd.IsActive,1) = 1
LEFT JOIN  tblmembership tblm on cvmd.MembershipId = tblm.MembershipId AND ISNULL(tblm.IsDeleted, 0) = 0 AND ISNULL(tblm.IsActive,1) = 1  
LEFT JOIN tblVehicleMake cvMfr ON cvl.VehicleMfr = cvMfr.MakeId
LEFT JOIN tblVehicleModel cvMo ON cvl.VehicleModel = cvMo.ModelId and cvMo.MakeId = cvMfr.MakeId
LEFT JOIN GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1

END

IF @Query IS Not NULL AND @Query != ''
BEGIN
select count(1) as Count 
FROM tblClientVehicle cvl 
LEFT JOIN tblClient cl ON cl.ClientId = cvl.ClientId AND ISNULL(cvl.IsDeleted,0)=0 AND ISNULL(cvl.IsActive,1)=1
LEFT JOIN tblClientVehicleMembershipDetails cvmd ON cvl.VehicleId = cvmd.ClientVehicleId AND ISNULL(cvmd.IsDeleted, 0) = 0 AND ISNULL(cvmd.IsActive,1) = 1
LEFT JOIN  tblmembership tblm on cvmd.MembershipId = tblm.MembershipId AND ISNULL(tblm.IsDeleted, 0) = 0 AND ISNULL(tblm.IsActive,1) = 1  
LEFT JOIN tblVehicleMake cvMfr ON cvl.VehicleMfr = cvMfr.MakeId
LEFT JOIN tblVehicleModel cvMo ON cvl.VehicleModel = cvMo.ModelId and cvMo.MakeId = cvMfr.MakeId
LEFT JOIN GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1 AND

 ((@Query is null or cl.FirstName  like @Query+'%') OR
  (@Query is null or cl.LastName  like @Query+'%') OR 
   (CONCAT_WS(' ',cl.FirstName,cl.LastName) like '%'+@Query+'%')OR
  (@Query is null or cvl.Barcode  like @Query+'%') OR
  (@Query is null or cvMfr.MakeValue  like @Query+'%') OR
  (@Query is null or cvmo.ModelValue  like @Query+'%') OR
  (@Query is null or cvCo.valuedesc  like @Query+'%') OR
  (@Query is null or cvl.VehicleNumber  like @Query+'%') OR
  (@Query is null or tblm.MembershipName  like @Query+'%'))

END



END
