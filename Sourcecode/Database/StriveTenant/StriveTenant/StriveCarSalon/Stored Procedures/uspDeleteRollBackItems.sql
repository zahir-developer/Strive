CREATE Procedure [StriveCarSalon].[uspDeleteRollBackItems]   
(@TicketNumber varchar(10))
as begin 
declare @JobId int=0;
declare @JobPaymentId int=0;
declare @GiftCardId int=0;

declare @ClientId int=0;
declare @CreditAmount decimal(18,2);

(select top 1 @JobId=JobId, @ClientId = ClientId from StriveCarSalon.tblJob where TicketNumber=@TicketNumber)
	  
	  /*
  Select top 1 @CreditAmount = amount from StriveCarSalon.tblJobPaymentDetail payDet
	  join strivecarsalon.GetTable('PaymentType') cat on cat.valueid = payDet.PaymentType where cat.valuedesc='Account' --Account
	  and payDet.JobPaymentId = @JobPaymentId and payDet.IsDeleted = 0

	  Select Amount + ISNULL(@CreditAmount,0) from StriveCarSalon.tblClient where ClientId = @ClientId and IsDeleted = 0
	  
	  
	  --Update StriveCarSalon.tblClient set Amount = Amount + ISNULL(@CreditAmount,0) where ClientId = @ClientId

	  */


      update StriveCarSalon.tblJob set IsDeleted=1 where TicketNumber=@TicketNumber
      update StriveCarSalon.tblJobItem set IsDeleted=1 where JobId=@JobId
	  update StriveCarSalon.tblJobPayment set IsDeleted=1 where JobId=@JobId
	  set @JobPaymentId =(select top 1  JobPaymentId from StriveCarSalon.tblJobPayment where JobId=@JobId)
	  update StriveCarSalon.tblGiftCardHistory set IsDeleted=1 where JobPaymentId=@JobPaymentId
	  set @GiftCardId =(select Top 1 GiftCardId from StriveCarSalon.tblGiftCardHistory where JobPaymentId=@JobPaymentId)
	  update StriveCarSalon.tblGiftCard set IsDeleted=1 where GiftCardId=@GiftCardId

	

END
