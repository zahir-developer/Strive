CREATE procedure [StriveCarSalon].[uspUpdateJobStatusComplete]  
(@JobId int,
@ActualTimeOut DateTimeoffSet)  
AS  
-- =============================================  
-- Author:  Vineeth.B  
-- Create date: 24-08-2020  
-- Description: To update COMPLETE JobStatus for JobId  
-- =============================================  
-- 25-Aug-2021 - Vetriselvi - changed data type from datetime to DateTimeoffSet for @ActualTimeOut
-- =============================================
BEGIN  
DECLARE @JobStatus int;  
SELECT
@JobStatus = (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc ='Completed') 

UPDATE tblJob SET JobStatus= @JobStatus , ActualTimeOut = @ActualTimeOut 
WHERE JobId=@JobId 
END