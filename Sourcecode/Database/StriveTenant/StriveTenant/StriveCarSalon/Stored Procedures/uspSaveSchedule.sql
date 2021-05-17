

CREATE procedure [StriveCarSalon].[uspSaveSchedule]
(@ScheduleId int,
 @EmployeeId int,
 @LocationId int,
 @RoleId int,
 @ScheduledDate DateTime,
 @StartTime DateTimeOffset,
 @EndTime DateTimeOffset,
 @ScheduleType int,
 @Comments varchar(20),
 @IsActive bit
 )

AS 
IF EXISTS(SELECT * FROM [tblSchedule] WHERE EmployeeId = @EmployeeId and ScheduledDate= @ScheduledDate and (StartTime between @StartTime and @EndTime OR EndTime between @StartTime and @EndTime))
BEGIN
select Result ='Employee Is Already Scheduled Between This Time'
END
ELSE
IF EXISTS(SELECT * FROM [tblSchedule] WHERE ScheduleId = @ScheduleId)
BEGIN
UPDATE [tblSchedule] SET EmployeeId=@EmployeeId, LocationId=@LocationId, RoleId=@RoleId, ScheduledDate=@ScheduledDate, 
      StartTime=@StartTime, EndTime = @EndTime, ScheduleType = @ScheduleType,Comments = @Comments
	  ,IsActive = @IsActive where ScheduleId=@ScheduleId
END
ELSE
BEGIN 
INSERT INTO [tblSchedule] (EmployeeId,LocationId,RoleId,ScheduledDate,StartTime,EndTime,ScheduleType,Comments,IsActive)
 VALUES (@EmployeeId,@LocationId,@RoleId,@ScheduledDate,@StartTime,@EndTime,@ScheduleType,@Comments,@IsActive);
END
