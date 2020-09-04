

CREATE PROCEDURE [CON].[uspDeleteSchedule]
(@tblScheduleId int)
AS
BEGIN
     UPDATE [CON].[tblSchedule] 
	 SET IsDeleted=1 WHERE ScheduleId = @tblScheduleId
END