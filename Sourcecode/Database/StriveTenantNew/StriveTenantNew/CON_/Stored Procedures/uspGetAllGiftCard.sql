
CREATE PROCEDURE [CON].[uspGetAllGiftCard]
(@GiftCardCode varchar(10))
as
begin
select GiftCardId,
LocationId,
GiftCardCode,
GiftCardName,
ExpiryDate,
TotalAmount,
Comments,
IsActive,
IsDeleted

from [CON].[tblGiftCard] 
where IsDeleted =0 and IsActive=1 and
(@GiftCardCode is null or GiftCardCode = @GiftCardCode) 
end