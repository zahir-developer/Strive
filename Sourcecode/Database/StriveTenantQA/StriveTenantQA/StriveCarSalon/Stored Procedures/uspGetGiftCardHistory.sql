
CREATE proc [StriveCarSalon].[uspGetGiftCardHistory]
(@GiftCardCode varchar(10))
as
begin
select
tblgch.GiftCardHistoryId 
,tblgch.GiftCardId   
,tblgch.LocationId   
,tblgch.TransactionType          
,tblgch.TransactionAmount      
,tblgch.TransactionDate        
,tblgch.Comments  
,tblgch.IsActive      

from [StriveCarSalon].[tblGiftCard] tblgc inner join 
[StriveCarSalon].[tblGiftCardHistory] tblgch on(tblgc.GiftCardId = tblgch.GiftCardId) 
where tblgc.GiftCardCode =@GiftCardCode
AND tblgch.IsDeleted=0 
end
