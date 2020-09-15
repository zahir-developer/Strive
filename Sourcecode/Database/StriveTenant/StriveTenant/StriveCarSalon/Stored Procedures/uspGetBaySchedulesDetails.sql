﻿




-- ==============================================================
-- Author:		Vineeth B
-- Create date: 08-09-2020
-- Description:	To get Schedule Details for LocationId and JobDate
-- ===============================================================
---------------------History-----------------------------------
-- ============================================================
-- 10-09-2020, Vineeth - Added IsActive and IsDeleted condition
-- 07-09-2020, Vineeth - Add JobDate is null

---------------------------------------------------------------
-- ============================================================
CREATE proc [StriveCarSalon].[uspGetBaySchedulesDetails] 
(@JobDate DateTime, @LocationId int)

AS
BEGIN

SELECT BayId,BayName FROM tblBay WHERE LocationId=@LocationId AND IsActive=1 AND IsDeleted = 0;

SELECT 
tblB.BayId
,tblB.JobId
,SUBSTRING(CONVERT(VARCHAR(8),ScheduleInTime,108),0,6) AS ScheduleInTime 
FROM tblBaySchedule tblB
INNER JOIN 
tblBay tblBa ON(tblB.BayId=tblBa.BayId) 
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
