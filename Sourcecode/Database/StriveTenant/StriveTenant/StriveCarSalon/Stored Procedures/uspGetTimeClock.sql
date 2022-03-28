
-- =============================================
-- Author:		Zahir Hussain
-- Create date: 06-08-2020
-- Description: Returns the TimeClock based on given input Details
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetTimeClock] 
	-- Add the parameters for the stored procedure here
	@LocationId int,
	@EmployeeId int, 
	@RoleId int = NULL,
	@Date DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT 	TimeClockId, EmployeeId, LocationId, RoleId, rm.RoleName, EventDate, InTime, OutTime, EventType, UpdatedFrom, [Status]
FROM tblTimeClock tc
JOIN tblRoleMaster rm on tc.RoleId = rm.RoleMasterId
WHERE LocationId = @LocationId and EmployeeId = @EmployeeId AND 
(RoleId = @RoleId OR @RoleId is NULL OR @RoleId = 0) AND CONVERT(date, EventDate) = @Date AND tc.IsDeleted = 0
AND tc.IsActive = 1

END