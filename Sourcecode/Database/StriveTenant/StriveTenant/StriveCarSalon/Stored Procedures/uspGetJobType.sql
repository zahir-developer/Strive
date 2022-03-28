CREATE PROCEDURE [StriveCarSalon].[uspGetJobType]
AS
-- =============================================
-- Author:		Vineeth B
-- Create date: 03-09-2020
-- Description:	To get All Job Type Details
-- =============================================

BEGIN
SELECT category,valueid,valuedesc from GetTable('JobType')
END
