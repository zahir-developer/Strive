﻿
CREATE Procedure [StriveCarSalon].[uspRollBackPayment]--  '993502' ,1 
(@TicketNumber varchar(10),
@LocationId int = NULL
)
as begin 
declare @JobId int=0;
declare @JobPaymentId int=0;
declare @GiftCardId int=0;
declare @JobPaymentDetailId int =0;
declare @ClientId int=0;
declare @CreditAmount decimal(18,2);


select top 1 @JobId = JobId , @ClientId = ClientId from StriveCarSalon.tblJob where TicketNumber=@TicketNumber 
and LocationId = @LocationId and ClientId is not Null and IsActive=1

set @JobPaymentDetailId =(select top 1 JobPaymentDetailId from tblJobPaymentDetail where JobPaymentId=@JobPaymentId)

set @GiftCardId =(select Top 1 GiftCardId from StriveCarSalon.tblGiftCardHistory where JobPaymentDetailId=@JobPaymentDetailId)

set @JobPaymentId =(select top 1  JobPaymentId from StriveCarSalon.tblJobPayment where JobId=@JobId order by JobPaymentId desc)

update StriveCarSalon.tblJobPayment set IsRollBack=1, IsProcessed = 0, Amount = 0.00 where JobId=@JobId

update StriveCarSalon.tblJob set JobPaymentId = NULL where JobId=@JobId

update StriveCarSalon.tblGiftCardHistory set JobPaymentDetailId = NULL, IsDeleted = 1 where JobPaymentDetailId=@JobPaymentDetailId

Select top 1 @CreditAmount = amount from StriveCarSalon.tblJobPaymentDetail payDet
	  join strivecarsalon.GetTable('PaymentType') cat on cat.valueid = payDet.PaymentType where cat.valuedesc='Account' --Account
	  and payDet.JobPaymentId = @JobPaymentId and payDet.IsDeleted = 0

IF (@CreditAmount > 0)
BEGIN

Update StriveCarSalon.tblClient set Amount = Amount + ISNULL(@CreditAmount,0) where ClientId = @ClientId and IsDeleted = 0

END

--update StriveCarSalon.tblJobPaymentCreditCard set IsDeleted=1 where JobPaymentDetailId=@JobPaymentDetailId


END
GO

