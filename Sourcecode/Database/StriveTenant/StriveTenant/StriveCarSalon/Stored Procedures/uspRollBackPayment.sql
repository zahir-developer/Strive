
CREATE Procedure [StriveCarSalon].[uspRollBackPayment]   
(@TicketNumber varchar(10))
as begin 
declare @JobId int=0;
declare @JobPaymentId int=0;
declare @GiftCardId int=0;

select top 1 @JobId = JobId from StriveCarSalon.tblJob where TicketNumber=@TicketNumber
update StriveCarSalon.tblJobPayment set IsRollBack=1, IsProcessed=0, Amount = 0.00 where JobId=@JobId

set @GiftCardId =(select Top 1 GiftCardId from StriveCarSalon.tblGiftCardHistory where JobPaymentId=@JobPaymentId)
update StriveCarSalon.tblGiftCardHistory set JobPaymentId = NULL, IsDeleted = 1 where JobPaymentId=@JobPaymentId

set @JobPaymentId =(select top 1  JobPaymentId from StriveCarSalon.tblJobPayment where JobId=@JobId)
update StriveCarSalon.tblJobPaymentCreditCard set IsDeleted=1 where JobPaymentId=@JobPaymentId
	  
update StriveCarSalon.tblJobPaymentDiscount set IsDeleted=1 where JobPaymentId=@JobPaymentId

END
GO

