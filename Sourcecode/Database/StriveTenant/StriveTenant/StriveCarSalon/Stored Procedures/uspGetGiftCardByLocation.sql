CREATE PROCEDURE [StriveCarSalon].[uspGetGiftCardByLocation] 
(@LocationId int)
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
where IsDeleted =0 and IsActive=1 and
(@LocationId is null or LocationId = @LocationId)
end
