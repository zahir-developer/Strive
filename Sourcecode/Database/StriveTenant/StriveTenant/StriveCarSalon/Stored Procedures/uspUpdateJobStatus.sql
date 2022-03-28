

-- =============================================
-- Author:		Zahir Hussain
-- Create date: 2021-10-12
-- Description:	Procedure to update JobStatus based on jobId
-- EXEC [StriveCarSalon].[uspUpdateJobStatus] 580799, '2021-10-12T16:15:44.200Z', 68, 'In Progress'
-- =============================================

---------------------History--------------------
-- =============================================
-- <Modified Date>, <Author> - <Description>

------------------------------------------------
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspUpdateJobStatus] 
@JobId int, @Date DateTimeOffset(7), @JobStatusId int, @JobStatus VARCHAR(20)
AS
BEGIN

IF @JobStatus = 'In Progress'
BEGIN
Update tbljob set JobStartTime = @Date, JobStatus = @JobStatusId where jobId = @JobId
END

IF @JobStatus = 'Completed'
BEGIN
Update tbljob set ActualTimeOut = @Date, JobStatus = @JobStatusId where jobId = @JobId
END

END