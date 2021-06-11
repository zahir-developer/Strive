CREATE PROCEDURE [StriveCarSalon].[uspUpdateJobPayment]
(@JobId int,@JobPaymentId int)
AS
BEGIN

     Update tblJob set JobPaymentId = @JobPaymentId, IsActive = 1
	 where JobId = @JobId

END