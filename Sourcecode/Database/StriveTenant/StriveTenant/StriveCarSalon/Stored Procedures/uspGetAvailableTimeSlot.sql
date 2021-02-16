

CREATE PROCEDURE [StriveCarSalon].[uspGetAvailableTimeSlot] --[StriveCarSalon].[uspGetAvailableTimeSlot] 2061,'2021-01-04'
(@LocationId INT, @Date Date)
-- =============================================
-- Author:		Vineeth B
-- Create date: 14-12-2020
-- Description:	To get Available Time Slot Details
-- =============================================
AS
BEGIN
DROP TABLE IF EXISTS #TimeSlot
SELECT bay.BayId AS BayId,SUBSTRING(CONVERT(VARCHAR(8),Slot,108),0,6)TimeIn 
INTO #TimeSlot
FROM tblBaySlot baySlot
JOIN tblBay bay on bay.BayId = baySlot.BayId
WHERE bay.IsActive=1 AND ISNULL(bay.IsDeleted,0)=0 AND bay.LocationId = @LocationId


DROP TABLE IF EXISTS #AvailableTimeSlot
SELECT 
tblb.BayId BaysId,
CONVERT(VARCHAR(5),tblj.TimeIn,108) TimesIn 
INTO #AvailableTimeSlot
FROM tblJob tblj 
INNER JOIN tblJobDetail tbljd 
ON tblj.JobId=tbljd.JobId AND tblj.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0 AND tbljd.IsActive=1 AND ISNULL(tbljd.IsDeleted,0)=0
INNER JOIN tblBay tblb 
ON tbljd.BayId = tblb.BayId AND tblb.IsActive=1 AND ISNULL(tblb.IsDeleted,0)=0
WHERE tblj.LocationId=@LocationId AND tblj.JobDate=@Date


SELECT BayId,TimeIn FROM #TimeSlot a LEFT JOIN #AvailableTimeSlot b ON a.BayId = b.BaysId AND a.TimeIn = b.TimesIn
WHERE BaysId IS NULL AND TimesIn IS NULL





END