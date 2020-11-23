
CREATE Procedure [StriveCarSalon].[uspRollBackPayment]   
(@TicketNumber varchar(10))
as begin 
declare @JobId int=0;
declare @JobPaymentId int=0;
declare @GiftCardId int=0;

declare @ClientId int=0;
declare @CreditAmount decimal(18,2);

select top 1 @JobId = JobId , @ClientId = ClientId from StriveCarSalon.tblJob where TicketNumber=@TicketNumber
update StriveCarSalon.tblJobPayment set IsRollBack=1, IsProcessed=0, Amount = 0.00 where JobId=@JobId

set @GiftCardId =(select Top 1 GiftCardId from StriveCarSalon.tblGiftCardHistory where JobPaymentId=@JobPaymentId)
update StriveCarSalon.tblGiftCardHistory set JobPaymentId = NULL, IsDeleted = 1 where JobPaymentId=@JobPaymentId

set @JobPaymentId =(select top 1  JobPaymentId from StriveCarSalon.tblJobPayment where JobId=@JobId order by JobPaymentId desc)

Select top 1 @CreditAmount = amount from StriveCarSalon.tblJobPaymentDetail payDet
	  join strivecarsalon.GetTable('PaymentType') cat on cat.valueid = payDet.PaymentType where cat.valuedesc='Account' --Account
	  and payDet.JobPaymentId = @JobPaymentId and payDet.IsDeleted = 0

Update StriveCarSalon.tblClient set Amount = Amount + ISNULL(@CreditAmount,0) where ClientId = @ClientId and IsDeleted = 0

update StriveCarSalon.tblJobPaymentCreditCard set IsDeleted=1 where JobPaymentId=@JobPaymentId
	  
--update StriveCarSalon.tblJobPaymentDiscount set IsDeleted=1 where JobPaymentId=@JobPaymentId

update StriveCarSalon.tblJobPaymentDetail set IsDeleted = 1 where JobPaymentId = @JobPaymentId

END
GO

