
-- =============================================
-- Author:		Vineeth B
-- Create date: 03-09-2020
-- Description:	To get All Job Type Details
-- =============================================
CREATE proc [StriveCarSalon].[uspGetJobType]
AS
BEGIN
SELECT category,valueid,valuedesc from [StriveCarSalon].GetTable('JobType')
END
