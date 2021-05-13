CREATE proc [StriveCarSalon].[uspIsGiftCardExist]
(@GiftCardCode varchar(10))
as
begin
select GiftCardId,
LocationId,
GiftCardCode,
GiftCardName,
ActivationDate,
TotalAmount,
Comments,
IsActive,
IsDeleted

from [tblGiftCard] 
where 
( GiftCardCode = @GiftCardCode) 
end