

-- ==========================================================
-- Author:              Vineeth.B
-- Created date:        2020-08-20
-- LastModified date: 
-- LastModified Author: 
-- Description:         To get Schedule details by ScheduleId
-- ===========================================================

CREATE PROC [StriveCarSalon].[uspGetScheduleByScheduleId]
@ScheduleId int
AS
BEGIN  

SELECT
	 ScheduleId,
	 EmployeeId,
	 LocationId,
	 ISNULL(IsAbscent, 0) as IsEmployeeAbscent,
	 RoleId,
	 ScheduledDate,
	 StartTime,
	 EndTime,
	 ScheduleType,
	 Comments
FROM
StriveCarSalon.tblSchedule 
WHERE ScheduleId=@ScheduleId
AND
ISNULL(IsDeleted,0)=0 
AND IsActive = 1


END