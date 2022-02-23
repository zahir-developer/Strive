-- =============================================
-- Author:		Zahir Hussain 
-- Create date: 2021-Aug-31
-- Description:	Procedure to delete JobItems
-- EXEC uspDeleteJobItemById '1193467, 1193466'
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspDeleteJobItemById] @JobItemId VARCHAR(100)
AS
BEGIN

SET NOCOUNT ON;

DECLARE @IsDeleted BIT = 1

Update tblJobItem set isDeleted = @IsDeleted  Where JobItemId in (Select Id from GetIDs(@JobItemId))

END