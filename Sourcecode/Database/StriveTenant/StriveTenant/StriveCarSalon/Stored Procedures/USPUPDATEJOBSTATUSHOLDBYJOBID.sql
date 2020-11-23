

CREATE procedure [StriveCarSalon].[USPUPDATEJOBSTATUSHOLDBYJOBID]
(@JobId int)
AS
-- =============================================
-- Author:		Vineeth.B
-- Create date: 24-08-2020
-- Description:	To update HOLD JobStatus for JobId
-- =============================================
BEGIN
DECLARE @JobStatus int;
SELECT @JobStatus = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc ='Hold')
UPDATE tblJob SET JobStatus=@JobStatus WHERE JobId=@JobId
END