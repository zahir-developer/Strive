
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 18-Nov-2020
-- Description:	Returns the time clock details of the Employees based on location and date. Sample EXEC Strivecarsalon.uspGetTimeClockEmployeeHourDetail 2034, '2020-11-17'
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetTimeClockEmployeeHourDetail]
	@locationId INT,
	@date datetime
AS
BEGIN

	DROP TABLE IF EXISTS #Hours_Data

select 
	e.employeeId,
	E.FirstName,
	E.LastName,
	TC.EventDate,
	DateDiff(MINUTE,InTime,ISNULL(OutTime,InTime)) LoginTime,
	rm.RoleName INTO #Hours_Data
from tblTimeClock TC 
inner join tblEmployee E on TC.EmployeeId = E.EmployeeId
inner join tblRoleMaster rm on rm.RoleMasterId = tc.RoleId
where LocationId = @locationId and EventDate = @date and tc.InTime is Not NULL

;WITH FinalResult AS (	
SELECT 
EmployeeId, FirstName, LastName,
CASE WHEN RoleName='Washer' THEN ISNULL(LoginTime,0) ELSE 0 END AS TotalWashHours,
CASE WHEN RoleName='Detailer' THEN ISNULL(LoginTime,0) ELSE 0 END AS TotalDetailHours,
CASE WHEN RoleName != 'Washer' AND RoleName != 'Detailer' THEN ISNULL(LoginTime,0) ELSE 0 END AS OtherHours
FROM #Hours_Data
)

Select EmployeeId, FirstName, LastName, SUM(TotalWashHours)/60 WashHours , SUM(TotalDetailHours)/60 DetailHours, SUM(OtherHours)/60 OtherHours from FinalResult
GROUP By EmployeeId, FirstName, LastName

SELECT TimeClockId, EmployeeId, RoleId, rm.RoleName, InTime, OutTime, CONVERT(VARCHAR(5), InTime, 108) as TimeIn, CONVERT(VARCHAR(5), OutTime, 108) as TimeOut
FROM StriveCarSalon.tblTimeClock tc
INNER JOIN StriveCarSalon.tblRoleMaster rm on rm.RoleMasterId = tc.RoleId
WHERE LocationId = @LocationId AND tc.EventDate = @Date AND (tc.IsDeleted = 0 OR tc.IsDeleted is null)
AND tc.IsActive = 1





END