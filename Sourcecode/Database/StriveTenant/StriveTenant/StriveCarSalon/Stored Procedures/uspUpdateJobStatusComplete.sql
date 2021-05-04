  
  
CREATE procedure [StriveCarSalon].[uspUpdateJobStatusComplete]  
(@JobId int,
@ActualTimeOut DateTime)  
AS  
-- =============================================  
-- Author:  Vineeth.B  
-- Create date: 24-08-2020  
-- Description: To update COMPLETE JobStatus for JobId  
-- =============================================  
BEGIN  
DECLARE @JobStatus int;  
SELECT
@JobStatus = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc ='Completed') 

UPDATE tblJob SET JobStatus= @JobStatus , ActualTimeOut = @ActualTimeOut 
WHERE JobId=@JobId 
END