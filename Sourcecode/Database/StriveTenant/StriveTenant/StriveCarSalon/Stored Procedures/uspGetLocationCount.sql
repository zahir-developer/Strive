-- =============================================
-- Author:		Zahir Hussain 
-- Create date: 2021-06-21
-- Description:	Returns number of location count.
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetLocationCount]
@TenantId INT = NULL
AS
BEGIN
	
	SET NOCOUNT ON;

    Select count(1) from tbllocation where IsDeleted = 0
END