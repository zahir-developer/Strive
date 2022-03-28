-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 22-09-2021
-- Description:	To get Available Time Slot for Details
-- EXEC [StriveCarSalon].[uspGetAvailableTimeSlot] 1, '2021-11-26 00:07:00'
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAvailableTimeSlot] 
--DECLARE
@LocationId INT--= 1
, @Date DateTIME
--= 2021-10-18T08:59:19.000Z
AS
BEGIN

SET @Date =  CONVERT(VARCHAR(10),@Date,111)


DROP TABLE IF EXISTS #TimeSlot

SELECT bay.BayId AS BayId,SUBSTRING(CONVERT(VARCHAR(8),Slot,108),0,6)TimeIn 
INTO #TimeSlot
FROM tblBaySlot baySlot
JOIN tblBay bay on bay.BayId = baySlot.BayId
WHERE bay.IsActive=1 AND ISNULL(bay.IsDeleted,0)=0 AND bay.LocationId = @LocationId
and (BayName like 'Detail%' OR BayName Like 'Bay%') AND BayName NOT Like 'Bay %'

--Select * from #TimeSlot

DROP TABLE IF EXISTS #BookedSlots

Select ScheduleInTime,ScheduleDate,j.jobDate, BayId into #BookedSlots from tblBaySchedule bs
JOIN tblJob j on j.jobId = bs.jobId 
where ScheduleDate = @Date and j.IsDeleted = 0

--Select * from #BookedSlots

Select DISTINCT ts.BayId, ts.TimeIn from #TimeSlot ts
LEFT JOIN #BookedSlots bs on bs.bayId = ts.bayId and ts.TimeIn = bs.ScheduleInTime
WHERE bs.ScheduleInTime is NULL

END