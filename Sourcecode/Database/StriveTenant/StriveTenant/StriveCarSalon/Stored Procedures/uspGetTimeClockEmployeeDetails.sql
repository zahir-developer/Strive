-- =============================================
-- Author:		Zahir Hussain
-- Create date: 09-15-2020
-- Description:	Retrieves all list of employees clocked based on location, EXEC [StriveCarSalon].uspGetTimeClockEmployeeDetails 0, '2020-08-15', '2020-09-09'
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetTimeClockEmployeeDetails] 
	@LocationId INT = NULL,
	@StartDate DATETIME = NULL,
	@EndDate DATETIME = NULL
AS
BEGIN
	
Select distinct e.EmployeeId, e.FirstName, e.LastName, l.LocationId, l.LocationName--, tc.EventDate
FROM StriveCarSalon.tblTimeClock tc
JOIN StriveCarSalon.tblEmployee e on e.EmployeeId = tc.EmployeeId 
JOIN StriveCarSalon.tblLocation l on l.LocationId = tc.LocationId
WHERE (l.LocationId = @LocationId OR @LocationId = 0) AND
(tc.EventDate BETWEEN @StartDate AND @EndDate OR (@StartDate IS NULL AND @EndDate IS NULL))
AND ISNULL(tc.IsDeleted,0) = 0 AND tc.IsActive = 1


END