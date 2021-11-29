-------------history-----------------
-- =============================================
-- 1  shalini 2021-06-01  -added list of jobids

-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspUpdateJobPayment] 
(@JobId varchar(max),@JobPaymentId int)
AS
BEGIN

     Update tblJob set JobPaymentId = @JobPaymentId, IsActive = 1
	 where JobId in(select value from string_split(@JobId,','))

END