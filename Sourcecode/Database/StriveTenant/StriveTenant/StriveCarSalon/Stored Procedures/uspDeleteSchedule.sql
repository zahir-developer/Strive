
CREATE PROCEDURE [StriveCarSalon].[uspDeleteSchedule]
(@tblScheduleId int)
AS
BEGIN
     UPDATE [StriveCarSalon].[tblSchedule] 
	 SET IsDeleted=1 WHERE ScheduleId = @tblScheduleId
END
