--uspRollBackPayment '580885,580886',1
/*
-----------------------------------------------------------------------------------------
Author              : Zahir
Create date         : 2021-05-13
Description         : Rollback job payment 
FRS					: Admin
-----------------------------------------------------------------------------------------
 Rev | Date Modified | Developer	| Change Summary
-----------------------------------------------------------------------------------------
  1  |  2021-05-13   | Zahir		| ClientId not Null removed, JobPaymentId taken from Job table.
  2  |  2021-05-23   | Vetriselvi	| Replaced JobPaymentDetailId by JobPaymentId in where condition for tblGiftCardHistory.
  3  |  2021-01-21   | Zahir		| TicketNumber changes - JobId taken based the ticketNumber and assigned. 
  4  |  2021-02-18   | Zahir		| Credit Account History table record soft deleted for Account payment  

-----------------------------------------------------------------------------------------
*/
CREATE PROCEDURE [StriveCarSalon].[uspRollBackPayment]
(@TicketNumber varchar(20),
@LocationId int
)
as begin 

DROP TABLE IF EXISTS #tblJob 
create table #tblJob(Id int identity(1,1),jobID int)

INSERT INTO #tblJob
Select jobId from tblJob where TicketNumber in (Select Id from Split(@TicketNumber)) and LocationId = @LocationId

DECLARE @Start INT = 1,@Count INT 

SELECT @Count = COUNT(1)
FROM #tblJob

declare @TicNo int=0

WHILE(@Start <= @Count)
BEGIN
	select @TicNo = jobID
	FROM #tblJob
	where Id = @Start

	declare @JobId int=0;
	declare @JobPaymentId int=0;
	declare @GiftCardId int=0;
	declare @JobPaymentDetailId int =0;
	declare @ClientId int=0;
	declare @CreditAmount decimal(18,2);


	select top 1 @JobId = JobId , @ClientId = ClientId, @JobPaymentId = JobPaymentId from tblJob where JobId = @TicNo 
	and LocationId = @LocationId 
	--and ClientId is not Null 
	--and IsActive=1

	set @JobPaymentDetailId =(select top 1 JobPaymentDetailId from tblJobPaymentDetail where JobPaymentId=@JobPaymentId)

	IF @JobPaymentDetailId > 0
	BEGIN
	set @GiftCardId =(select Top 1 GiftCardId from tblGiftCardHistory where JobPaymentDetailId=@JobPaymentDetailId)
	END

	update tblJobPayment set IsRollBack=1, IsProcessed = 0 where JobPaymentId=@JobPaymentId

	update tblJob set JobPaymentId = NULL, CheckOut = 0 where JobId=@JobId

	update tblGiftCardHistory set IsDeleted = 1 where JobPaymentId=@JobPaymentId

	Select top 1 @CreditAmount = amount from tblJobPaymentDetail payDet
	join GetTable('PaymentType') cat on cat.valueid = payDet.PaymentType where cat.valuedesc='Account' --Account
	and payDet.JobPaymentId = @JobPaymentId and payDet.IsDeleted = 0

	IF (@CreditAmount > 0)
	BEGIN

	--Update tblClient set Amount = Amount + ISNULL(@CreditAmount,0) where ClientId = @ClientId and IsDeleted = 0
	Update tblCreditAccountHistory set IsDeleted = 1, IsActive = 0 where JobPaymentId = @JobPaymentId

	END
	SET @Start = @Start + 1
END
END
GO

