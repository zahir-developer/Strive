
CREATE PROCEDURE [StriveCarSalon].[uspGetBaySchedulesDetails]
(@JobDate DateTime, @LocationId int)

AS
-- ==============================================================
-- Author:		Vineeth B
-- Create date: 08-09-2020
-- Description:	To get Schedule Details for LocationId and JobDate
-- ===============================================================
---------------------History-----------------------------------
-- ============================================================
-- 10-09-2020, Vineeth - Added IsActive and IsDeleted condition
-- 07-09-2020, Vineeth - Add JobDate is null
-- 22-01-2021, Zahir - Added JobType condition to avoid invalid jobs
---------------------------------------------------------------
 -- [StriveCarSalon].[uspGetBaySchedulesDetails] '2021-03-15' ,20
-- ============================================================
BEGIN

SELECT BayId,BayName FROM tblBay WHERE LocationId=@LocationId AND IsActive=1 AND IsDeleted = 0 AND 
	(BayName like 'Detail%' OR BayName Like 'Bay%') AND BayName NOT Like 'Bay %'
 
SELECT 
tblB.BayId
,tblB.JobId
,SUBSTRING(CONVERT(VARCHAR(8),ScheduleInTime,108),0,6) AS ScheduleInTime 
FROM tblBaySchedule tblB
INNER JOIN tblBay tblBa ON(tblB.BayId=tblBa.BayId) 
INNER JOIN tblJob tblJ on (tblj.JobId = tblb.JobId)
INNER JOIN GetTable('JobType') jt ON(tblj.JobType = jt.valueid) and jt.valuedesc = 'Detail'

WHERE 
tblB.ScheduleDate=@JobDate 
AND 
tblB.IsActive=1 
AND 
tblBa.IsActive=1
AND 
ISNULL(tblB.IsDeleted,0)=0
AND
ISNULL(tblBa.IsDeleted,0)=0
AND
tblBa.LocationId=@LocationId;
END
