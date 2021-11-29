
-- =============================================
-- Author:		Zahir Hussain
-- Create date: 09-15-2020
-- Description:	Retrieves all list of employees clocked based on location, EXEC [StriveCarSalon].uspGetTimeClockEmployeeDetails 0, '2020-08-15', '2020-09-09'
-- =============================================

-----------------------------------------------------------------------------------------
-- Rev | Date Modified  | Developer	| Change Summary
-----------------------------------------------------------------------------------------
--  1  |  2020-Sep-30   | Vineeth	| Added IsActive and IsDelete condition for Employee
-----------------------------------------------------------------------------------------
CREATE PROCEDURE [StriveCarSalon].[uspGetTimeClockEmployeeDetails] 
	@LocationId INT = NULL,
	@StartDate DATETIME = NULL,
	@EndDate DATETIME = NULL
AS
BEGIN
	
Select distinct e.EmployeeId, e.FirstName, e.LastName, l.LocationId, l.LocationName--, tc.EventDate
FROM tblTimeClock tc
JOIN tblEmployee e on e.EmployeeId = tc.EmployeeId 
JOIN tblLocation l on l.LocationId = tc.LocationId
WHERE (l.LocationId = @LocationId OR @LocationId = 0) AND
(tc.EventDate BETWEEN @StartDate AND @EndDate OR (@StartDate IS NULL AND @EndDate IS NULL))
AND ISNULL(tc.IsDeleted,0) = 0 AND tc.IsActive = 1
AND ISNULL(e.IsDeleted,0) = 0 AND e.IsActive = 1

Select distinct 
EmployeeId, 
CONCAT(FirstName,' ',LastName) AS EmployeeName
FROM tblEmployee 
WHERE 
EmployeeId 
NOT IN
(SELECT EmployeeId FROM tblTimeClock where EventDate BETWEEN @StartDate AND @EndDate 
AND LocationId=@LocationId AND IsActive=1 AND ISNULL(IsDeleted,0)=0)AND IsActive=1 AND ISNULL(IsDeleted,0)=0
END