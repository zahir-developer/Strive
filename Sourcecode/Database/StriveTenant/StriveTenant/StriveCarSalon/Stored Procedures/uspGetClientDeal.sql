
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 3 DEC 2021
-- Description:	Returns the Client Deal Detail
-- uspGetClientDeal 57398, '2021-12-03'
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetClientDeal]
@ClientId INT,
@Date DateTime,
@DealId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select ClientDealId, cd.DealId, cd.ClientId, d.DealName, DealCount, cd.StartDate, cd.EndDate 
	from tblClientDeal cd
	JOIN tblDeal d on d.dealId = cd.dealId 
	where ClientId = @ClientId and @Date BETWEEN cd.StartDate AND cd.EndDate and (@DealId IS NULL OR @DealId = cd.DealId) and cd.IsDeleted = 0

END