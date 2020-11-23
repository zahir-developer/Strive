  
  
create procedure [StriveCarSalon].[uspUpdateJobStatusCompleteByJobId]  
(@JobId int)  
AS  
-- =============================================  
-- Author:  Vineeth.B  
-- Create date: 24-08-2020  
-- Description: To update COMPLETE JobStatus for JobId  
-- =============================================  
BEGIN  
DECLARE @JobStatus int;  
SELECT @JobStatus = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc ='Completed')  
UPDATE tblJob SET JobStatus=@JobStatus WHERE JobId=@JobId  
END