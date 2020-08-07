
CREATE proc [StriveCarSalon].[uspGetAllGiftCard]
(@LocationId int)
as
begin
select tblgc.GiftCardId,
tblgc.LocationId,
tblgc.GiftCardCode,
tblgc.GiftCardName,
tblgc.ExpiryDate,
tblgc.IsActive,
tblgc.IsDeleted,
tblgc.Comments,

tblgch.GiftCardHistoryId AS GiftCardHistory_GiftCardHistoryId,
tblgch.GiftCardId       AS GiftCardHistory_GiftCardId,
tblgch.LocationId     AS GiftCardHistory_LocationId,
tblgch.TransactionType           AS GiftCardHistory_TransactionType,
tblgch.TransactionAmount       AS GiftCardHistory_TransactionAmount,
tblgch.TransactionDate         AS GiftCardHistory_TransactionDate,
tblgch.Comments        AS GiftCardHistory_Comments

from [StriveCarSalon].[tblGiftCard] tblgc inner join 
[StriveCarSalon].[tblGiftCardHistory] tblgch on(tblgc.GiftCardId = tblgch.GiftCardId) 
where tblgc.IsDeleted =0 and tblgc.LocationId=@LocationId
end