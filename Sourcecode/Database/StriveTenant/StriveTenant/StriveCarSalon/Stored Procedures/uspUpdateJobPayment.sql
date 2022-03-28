---------------------History--------------------
-- =============================================
-- 1 | 2021-06-01 | Shalini | Added list of jobids
-- 2 | 2022-01-06 | Zahir M | Replaced JobId with TicketNumber
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspUpdateJobPayment] 
(@TicketNumber varchar(max), @JobPaymentId int, @locationId INT = NULL)
AS
BEGIN

Update tblJob set JobPaymentId = @JobPaymentId, IsActive = 1

where TicketNumber in(select value from string_split(@TicketNumber,',')) and (LocationId = @locationId OR @locationId IS NULL)

END