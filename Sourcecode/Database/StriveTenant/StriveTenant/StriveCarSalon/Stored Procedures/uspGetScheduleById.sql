CREATE PROCEDURE [StriveCarSalon].[uspGetScheduleById]
@ScheduleId int
AS
-- ==========================================================
-- Author:              Vineeth.B
-- Created date:        2020-08-20
-- LastModified date: 
-- LastModified Author: 
-- Description:         To get Schedule details by ScheduleId
-- ===========================================================

-- ==========================================================
---------------------------History---------------------------
-- 26-08-2020 - Zahir Hussain - ColorCode added from Location table, Alias name added.
-- ===========================================================
BEGIN  

SELECT
	 ScheduleId,
	 EmployeeId,
	 tbls.LocationId,
	 tbll.ColorCode,
	 ISNULL(IsAbscent, 0) as IsEmployeeAbscent,
	 RoleId,
	 ScheduledDate,
	 tbls.StartTime,
	 tbls.EndTime,
	 ScheduleType,
	 Comments
FROM
StriveCarSalon.tblSchedule tbls
INNER JOIN StriveCarSalon.tblLocation tbll on tbll.LocationId = tbls.LocationId
WHERE ScheduleId=@ScheduleId
AND
ISNULL(tbls.IsDeleted,0)=0 
AND tbls.IsActive = 1


END
