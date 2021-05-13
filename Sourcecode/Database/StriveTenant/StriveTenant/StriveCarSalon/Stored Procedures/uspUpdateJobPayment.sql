CREATE PROC [StriveCarSalon].[uspUpdateJobPayment]
(@JobId int,@JobPaymentId int)
AS
BEGIN

     Update tblJob set JobPaymentId = @JobPaymentId
	 where JobId = @JobId

END