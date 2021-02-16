
CREATE proc [StriveCarSalon].[uspGetAllGiftCards]

as
begin

DROP TABLE If EXISTS #GiftCardHistory  

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
ExpiryDate,
(gc.TotalAmount + gh.Balance) as TotalAmount,
Comments,
IsActive,
IsDeleted

from [StriveCarSalon].[tblGiftCard] gc
LEFT JOIN #GiftCardHistory gh on gh.GiftCardId = gc.GiftCardId
where IsDeleted =0 and IsActive=1

end