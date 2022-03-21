---------------------History---------------------------
-- ====================================================
-- 08-jun-2021, shalini - pagenumber and count for nullquery changes
-- 16-06-2021, Shalini -removed wildcard from Query
-------------------------------------------------------
--[StriveCarSalon].[uspGetAllGiftCards]null,9,10,'ASC',null,null
CREATE PROCEDURE [StriveCarSalon].[uspGetAllGiftCards]
(@Query NVARCHAR(50) = NULL,
@PageNo INT = NULL,
@PageSize INT = NULL,
@SortOrder VARCHAR(5) = 'ASC', 
@SortBy VARCHAR(50) = NULL,
@StartDate date =NULL,

@EndDate date =NULL)
As
Begin
DECLARE @Skip INT = 0;
IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

IF @PageSize is NULL
BEGIN
SET @PageSize = (Select count(1) from tblGiftCard);
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
Print @PageSize
Print @PageNo
Print @Skip
END

--Select * from StriveCarSalon.tblGiftcard where giftcardCode = '749839'
DROP TABLE If EXISTS #GiftCardHistory  

DROP TABLE If EXISTS #GetAllGiftCard

select 
GiftCardId,
SUM(gh.TransactionAmount) Balance
INTO #GiftCardHistory
from tblGiftCardHistory gh
group by GiftCardId

select 
gc.GiftCardId,
gc.LocationId,
gc.GiftCardCode,
gc.GiftCardName,
gc.ActivationDate,
 gh.Balance as TotalAmount,
--(gc.TotalAmount + gh.Balance) as TotalAmount,
gc.Comments,
gc.IsActive,
gc.IsDeleted,
ISNULL(tblCli.FirstName,'None') FirstName,
ISNULL(tblCli.LastName,'') LastName
into #GetAllGiftCard
from [tblGiftCard] gc
LEFT JOIN #GiftCardHistory gh on gh.GiftCardId = gc.GiftCardId
left Join [tblClient] tblCli on(gc.ClientId = tblCli.ClientId) 
where gc.IsDeleted =0 and gc.IsActive=1 and
--( cast (gc.ActivationDate as date) between @StartDate  and @EndDate or(@StartDate is null and @EndDate is null ))
(@Query is null OR	gc.GiftCardName like '%'+@Query+'%'
OR	gc.GiftCardCode like '%'+@Query+'%'
OR	tblCli.FirstName like '%'+@Query+'%'
OR	tblCli.LastName like '%'+@Query+'%'
OR	TotalAmount like '%'+@Query+'%'
OR	gc.ActivationDate like '%'+@Query+'%')
order by 
CASE WHEN @SortBy = 'GifCardNo' AND @SortOrder='ASC' THEN gc.GiftCardCode END ASC,
CASE WHEN @SortBy = 'ActivatedDate' AND @SortOrder='ASC' THEN gc.ActivationDate END ASC,
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='ASC' THEN tblCli.FirstName END ASC,
CASE WHEN @SortBy = 'TotalAmount' AND @SortOrder='ASC' THEN TotalAmount END ASC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='ASC' THEN tblCli.LastName END ASC,

CASE WHEN @SortBy IS NULL AND @SortOrder='ASC' THEN 1 END ASC,
----DESC
CASE WHEN @SortBy = 'GifCardNo' AND @SortOrder='DESC' THEN gc.GiftCardCode END DESC,
CASE WHEN @SortBy = 'ActivatedDate' AND @SortOrder='DESC' THEN gc.ActivationDate END DESC,
CASE WHEN @SortBy = 'FirstName' AND @SortOrder='DESC' THEN tblcli.FirstName END DESC,
CASE WHEN @SortBy = 'LastName' AND @SortOrder='DESC' THEN tblCli.LastName END DESC,
CASE WHEN @SortBy = 'TotalAmount' AND @SortOrder='DESC' THEN TotalAmount END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder='DESC' THEN gc.GiftCardId END DESC,

CASE WHEN @SortBy IS NULL AND @SortOrder IS NULL THEN gc.GiftCardId  END ASC

OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY

select * from #GetAllGiftCard

IF @Query IS NULL OR @Query = ''
BEGIN 
select count(1) as Count 
from [tblGiftCard] gc
LEFT JOIN #GiftCardHistory gh on gh.GiftCardId = gc.GiftCardId
left Join [tblClient] tblCli on(gc.ClientId = tblCli.ClientId) 
where gc.IsDeleted =0 and gc.IsActive=1


END

IF @Query IS Not NULL AND @Query != ''
BEGIN
select count(1) as Count from [tblGiftCard] gc
LEFT JOIN #GiftCardHistory gh on gh.GiftCardId = gc.GiftCardId
left Join [tblClient] tblCli on(gc.ClientId = tblCli.ClientId) 
where gc.IsDeleted =0 and gc.IsActive=1
and (@Query is null OR	gc.GiftCardName like @Query+'%'
OR	gc.GiftCardCode like @Query+'%'
OR	tblCli.FirstName like @Query+'%'
OR	tblCli.LastName like @Query+'%'
OR	TotalAmount like @Query+'%'
OR	gc.ActivationDate like @Query+'%')

END



end