CREATE proc [StriveCarSalon].[uspGetGiftCardBalanceHistory] --[StriveCarSalon].[uspGetGiftCardBalanceHistory]'363519'
(@GiftCardCode varchar(10))
as
begin


--ActiveDate
--GiftCardId
declare @GiftCardId int,@ActiveDate datetime ;

Select @GiftCardId=GiftCardId ,@ActiveDate =ActivationDate from tblGiftCard where GiftCardCode  = @GiftCardCode


Drop TABLE IF EXISTS #GiftCardHistory

select
tblgch.GiftCardHistoryId,
tblgch.GiftCardId   
,tblgch.TransactionType          
,tblgch.TransactionAmount      
,tblgch.TransactionDate
,tj.TicketNumber
,isnull(tj.IsActive,1) as Status
INTO #GiftCardHistory   

from [StriveCarSalon].[tblGiftCardHistory] tblgch 

LEFT JOIN tblJobPaymentDetail jp on tblgch.JobPaymentDetailId =jp.JobPaymentDetailId
left join tblJob tj on jp.JobPaymentId = tj.JobPaymentId
where tblgch.GiftCardId =@GiftCardId
AND tblgch.IsDeleted=0 

declare @Balance decimal(18,2);
Select @Balance=SUM(TransactionAmount) from #GiftCardHistory 

Select @GiftCardId as GiftCardId , @Balance as BalanceAmount,@ActiveDate  as ActivationDate

Select * from #GiftCardHistory

end