Create PROC [StriveCarSalon].[uspUpdateJobPayment]
(@JobId int,@JobPaymentId int)
AS
BEGIN

     Update StriveCarSalon.tblJob set JobPaymentId = @JobPaymentId
	 where JobId = @JobId

END