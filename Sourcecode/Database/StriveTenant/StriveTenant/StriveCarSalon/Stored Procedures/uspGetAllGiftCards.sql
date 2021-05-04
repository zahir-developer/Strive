
CREATE proc [StriveCarSalon].[uspGetAllGiftCards]--[StriveCarSalon].[uspGetAllGiftCards] null,1,10,null,null,null,null --'2021-03-08','2021-03-15'
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
SET @PageSize = (Select count(1) from StriveCarSalon.tblGiftCard);
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
Print @PageSize
Print @PageNo
Print @Skip
END
DROP TABLE If EXISTS #GiftCardHistory  

DROP TABLE If EXISTS #GetAllGiftCard

select 
GiftCardId,
SUM(gh.TransactionAmount) Balance
INTO #GiftCardHistory
from StriveCarSalon.tblGiftCardHistory gh
group by GiftCardId

select 
gc.GiftCardId,
LocationId,
GiftCardCode,
GiftCardName,
ActivationDate,

(gc.TotalAmount + gh.Balance) as TotalAmount,
Comments,
gc.IsActive,
gc.IsDeleted,
tblCli.FirstName,
tblCli.LastName
into #GetAllGiftCard
from [StriveCarSalon].[tblGiftCard] gc
LEFT JOIN #GiftCardHistory gh on gh.GiftCardId = gc.GiftCardId
left Join [StriveCarSalon].[tblClient] tblCli on(gc.ClientId = tblCli.ClientId)
where gc.IsDeleted =0 and gc.IsActive=1 and ( gc.ActivationDate between @StartDate  and @EndDate or(@StartDate is null and @EndDate is null ))
 and (
@Query is null OR	gc.GiftCardName like '%'+@Query+'%'
								OR	gc.GiftCardCode like '%'+@Query+'%'
								OR	tblCli.FirstName like '%'+@Query+'%'
								OR	tblCli.LastName like '%'+@Query+'%'
									OR	TotalAmount like '%'+@Query+'%'
								OR	gc.ActivationDate like '%'+@Query+'%'
								)
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
select count(1) as Count from StriveCarSalon.tblGiftCard where 
ISNULL(IsDeleted,0) = 0 

END

IF @Query IS Not NULL AND @Query != ''
BEGIN
select count(1) as Count from #GetAllGiftCard
END



end