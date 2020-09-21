﻿

CREATE proc [StriveCarSalon].[uspGetGiftCardByLocation] 
(@LocationId int)
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

from [StriveCarSalon].[tblGiftCard] 
where IsDeleted =0 and IsActive=1 and
(@LocationId is null or LocationId = @LocationId)
end
