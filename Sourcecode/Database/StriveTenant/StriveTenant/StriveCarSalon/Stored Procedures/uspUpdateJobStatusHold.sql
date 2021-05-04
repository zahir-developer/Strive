

Create procedure [StriveCarSalon].[uspUpdateJobStatusHold]
(@JobId int,@IsHold bit)
AS
-- =============================================
-- Author:		Vineeth.B
-- Create date: 24-08-2020
-- Description:	To update HOLD JobStatus for JobId
-- =============================================
BEGIN
--DECLARE @JobStatus int ;
--SELECT @JobStatus = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc ='Hold')
UPDATE tblJob SET IsHold=@IsHold
 WHERE JobId=@JobId
END