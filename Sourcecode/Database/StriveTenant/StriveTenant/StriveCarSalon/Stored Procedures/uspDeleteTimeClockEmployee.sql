
-- =============================================
-- Author:		Zahir Hussain
-- Create date: 09-15-2020
-- Description:	Deletes TimeClock record based on locationId and EmployeeId
-- =============================================



CREATE PROCEDURE [StriveCarSalon].[uspDeleteTimeClockEmployee] (@locationId int, @EmployeeId int)
AS
BEGIN

Update [tblTimeClock] Set IsDeleted=1 where locationId= @locationId and EmployeeId = @EmployeeId

END