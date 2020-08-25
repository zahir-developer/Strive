

-- ======================================================================
-- Author:               BennyJohnson
-- Created date:         2020-08-20
-- LastModified date:    2020-08-20
-- Last Modified Author: Vineeth.B
-- Description:          To get schedule for the respective dates and locationid
-- ======================================================================

CREATE PROC [StriveCarSalon].[uspGetSchedule]
@ScheduledStartDate Date,
@ScheduledEndDate Date,
@LocationId int
AS
BEGIN  

SELECT
	 ScheduleId,
	 EmployeeId,
	 LocationId,
	 RoleId,
	 ScheduledDate,
	 StartTime,
	 EndTime,
	 ScheduleType,
	 Comments
FROM
StriveCarSalon.tblSchedule 
WHERE ScheduledDate BETWEEN @ScheduledStartDate AND @ScheduledendDate
AND
LocationId =@LocationId OR (0 = @LocationId)
AND
ISNULL(IsDeleted,0)=0 
AND IsActive = 1


END