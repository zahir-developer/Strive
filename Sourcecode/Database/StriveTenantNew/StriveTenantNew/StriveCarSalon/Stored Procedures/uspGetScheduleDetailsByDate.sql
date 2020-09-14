


-- ========================================================
-- Author:		Vineeth B
-- Create date: 03-09-2020
-- Description:	To get Scheduled Time details for given Date
-- =========================================================
---------------------History--------------------
-- =============================================
-- 04-09-2020, Vineeth - Added BayId and JobId
-- 07-09-2020, Vineeth - Add JobDate is null

------------------------------------------------
-- =============================================
CREATE proc [StriveCarSalon].[uspGetScheduleDetailsByDate] 
(@JobDate DateTime, @LocationId int)

AS
BEGIN

SELECT BayId,BayName FROM tblBay WHERE LocationId=@LocationId AND IsActive=1 ;

SELECT tblB.BayId,tblB.JobId,SUBSTRING(CONVERT(VARCHAR(8),ScheduleInTime,108),0,6) AS ScheduleInTime FROM tblBaySchedule  tblB
INNER JOIN tblJob tblj ON(tblB.JobId=tblj.JobId) WHERE tblB.ScheduleDate=@JobDate AND tblB.IsActive=1 AND tblj.LocationId=@LocationId;

END