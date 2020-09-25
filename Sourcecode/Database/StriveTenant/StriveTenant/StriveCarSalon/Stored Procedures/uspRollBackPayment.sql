CREATE Procedure [StriveCarSalon].[uspRollBackPayment]   
(@TicketNumber varchar(10))
as begin 
declare @JobId int=0;
declare @JobPaymentId int=0;
declare @GiftCardId int=0;
      set @JobId=(  select top 1 JobId from StriveCarSalon.tblJob where TicketNumber=@TicketNumber)
	  update StriveCarSalon.tblJobPayment set IsDeleted=1 where JobId=@JobId
	  set @JobPaymentId =(select top 1  JobPaymentId from StriveCarSalon.tblJobPayment where JobId=@JobId)
	  update StriveCarSalon.tblGiftCardHistory set IsDeleted=1 where JobPaymentId=@JobPaymentId
	  set @GiftCardId =(select Top 1 GiftCardId from StriveCarSalon.tblGiftCardHistory where JobPaymentId=@JobPaymentId)
	  update StriveCarSalon.tblGiftCard set IsDeleted=1 where GiftCardId=@GiftCardId

END
GO

