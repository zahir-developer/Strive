CREATE PROCEDURE [StriveCarSalon].[uspGetGiftCardById]
(@GiftCardId int)
as
begin
select tblgc.GiftCardId,
tblgc.LocationId,
tblgc.GiftCardCode,
tblgc.GiftCardName,
tblgc.ActivationDate,
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

from [tblGiftCard] tblgc inner join 
[tblGiftCardHistory] tblgch on(tblgc.GiftCardId = tblgch.GiftCardId) 
where tblgc.IsActive =1 and tblgc.GiftCardId =@GiftCardId
AND tblgc.IsDeleted=0 and tblgch.IsDeleted=0
end
