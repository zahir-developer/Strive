CREATE Procedure [StriveCarSalon].[uspDeleteJobItems]   
(@TicketNumber varchar(10))
as begin 
declare @JobId int=0;
declare @JobPaymentId int=0;
declare @GiftCardId int=0;
      set @JobId=(select top 1 JobId from StriveCarSalon.tblJob where TicketNumber=@TicketNumber)
	  update StriveCarSalon.tblJob set IsDeleted=1 where TicketNumber=@TicketNumber
      update StriveCarSalon.tblJobItem set IsDeleted=1 where JobId=@JobId
END