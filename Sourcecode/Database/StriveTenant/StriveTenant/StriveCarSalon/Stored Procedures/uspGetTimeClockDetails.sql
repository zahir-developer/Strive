-- =============================================
-- Author:		Zahir Hussain
-- Create date: 06-08-2020
-- Description: Returns the ClockTime Details
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetTimeClockDetails] 
	-- Add the parameters for the stored procedure here
	@LocationId int,
	@EmployeeId int, 
	@RoleId int,
	@Date DateTime
AS
BEGIN

SET NOCOUNT ON;

SET @RoleId = (Select RoleMasterId from tblRoleMaster where RoleName = 'Detailer')

SELECT 	TimeClockId, EmployeeId, LocationId, RoleId, EventDate, InTime, OutTime, EventType, UpdatedFrom, [Status]
FROM tblTimeClock
WHERE LocationId = @LocationId and EmployeeId = @EmployeeId AND 
RoleId = @RoleId AND CONVERT(date, EventDate) = @Date AND (IsDeleted = 0 OR IsDeleted is null)
AND IsActive = 1

END