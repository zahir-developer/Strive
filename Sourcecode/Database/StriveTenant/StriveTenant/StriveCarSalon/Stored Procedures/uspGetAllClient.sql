---------------------History---------------------------
-- ====================================================
-- 07-jun-2021, Shalini - Pagenumber and Count for null query changes
-- 13-Oct-2021, Zahir   - Client Type fixed.

-------------------------------------------------------
--[StriveCarSalon].[uspGetAllClient] 1,'Aaliyah ULTRA',1,10,null,null
CREATE PROCEDURE [StriveCarSalon].[uspGetAllClient]
@locationId int =null,
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
Drop TABLE IF EXISTS #GetAllClient

SELECT 
tblc.ClientId,
tblc.FirstName,
tblc.LastName,
ISNULL(tblc.Amount,0) as Amount,
tblc.IsActive,
tblc.ClientType,
ct.CodeValue AS Type,
tblca.PhoneNumber AS PhoneNumber,
tblca.Address1,
tblca.Address2
into #GetAllClient
FROM [tblClient] tblc 
     left join [tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId)
	 left join tblcodevalue ct ON tblc.ClientType = ct.id	 
WHERE ISNULL(tblc.IsDeleted,0) = 0 AND ISNULL(tblc.FirstName,' ') != ''   AND
isnull(tblc.IsDeleted,0)=0 and 
isnull(tblca.IsDeleted,0)=0 and (
@Query is null OR	tblc.FirstName like @Query+'%' 
								OR	tblc.lastName like @Query+'%'
								OR CONCAT_WS(' ',tblc.FirstName,tblc.LastName) like '%'+@Query+'%'
								OR	ct.CodeValue like +@Query+'%'
								OR	tblca.IsActive like +@Query+'%'
								OR	tblca.PhoneNumber like +@Query+'%')
Group by
tblc.ClientId,
tblc.FirstName,
tblc.LastName,
tblc.Amount,
tblc.IsActive,
tblc.ClientType,
ct.CodeValue ,
tblca.PhoneNumber,
tblca.Address1,
tblca.Address2
order by 
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='ASC' THEN tblc.FirstName END ASC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='ASC' THEN tblc.LastName END ASC,
CASE WHEN @SortBy = 'PhoneNumber' AND @SortOrder='ASC' THEN tblca.PhoneNumber END ASC,
CASE WHEN @SortBy = 'Type' AND @SortOrder='ASC' THEN ct.CodeValue END ASC,
CASE WHEN @SortBy = 'IsActive' AND @SortOrder='ASC' THEN tblc.IsActive END ASC,
CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='DESC' THEN tblc.FirstName END DESC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='DESC' THEN tblc.LastName END DESC,
CASE WHEN @SortBy = 'PhoneNumber' AND @SortOrder='DESC' THEN tblca.PhoneNumber END DESC,
CASE WHEN @SortBy = 'Type' AND @SortOrder='DESC' THEN ct.CodeValue END DESC,
CASE WHEN @SortBy = 'IsActive' AND @SortOrder='DESC' THEN tblc.IsActive END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN tblc.FirstName END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN tblc.FirstName END ASC



OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY

select * from #GetAllClient


IF @Query IS NULL OR @Query = ''
BEGIN 
select count(1) as Count FROM [tblClient] tblc 
     left join [tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId)
	 left join GetTable('ClientType') ct ON tblc.ClientType = ct.valueid	 
WHERE ISNULL(tblc.IsDeleted,0) = 0 AND ISNULL(tblc.FirstName,' ') != ''   AND
isnull(tblc.IsDeleted,0)=0 and 
isnull(tblca.IsDeleted,0)=0 

END

IF @Query IS Not NULL AND @Query != ''
BEGIN
select count(1) as Count 
FROM [tblClient] tblc 
     left join [tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId)
	 left join GetTable('ClientType') ct ON tblc.ClientType = ct.valueid	 
WHERE ISNULL(tblc.IsDeleted,0) = 0 AND ISNULL(tblc.FirstName,' ') != ''   AND
isnull(tblc.IsDeleted,0)=0 and 
isnull(tblca.IsDeleted,0)=0 and (
@Query is null OR	tblc.FirstName like '%'+@Query+'%' 
OR	tblc.lastName like '%'+@Query+'%'
OR CONCAT_WS(' ',tblc.FirstName,tblc.LastName) like '%'+@Query+'%'
OR	ct.valuedesc like '%'+@Query+'%'
OR	tblca.IsActive like '%'+@Query+'%'
OR	tblca.PhoneNumber like '%'+@Query+'%')
END

END
