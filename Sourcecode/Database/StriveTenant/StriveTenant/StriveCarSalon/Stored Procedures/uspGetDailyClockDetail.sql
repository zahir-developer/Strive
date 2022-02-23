CREATE PROCEDURE [StriveCarSalon].[uspGetDailyClockDetail]
(@Date Date = null, @locationId int = null)
AS
-- =============================================
-- Author:		Arunkumae S
-- Create date: 09-11-2020
-- Description:	Gets the Daily tips detail 
-- =============================================
 -- [StriveCarSalon].[uspGetDailyClockDetail] '2020-11-09', 1

BEGIN

;WITH Hours_Data
AS (
select 
	concat(E.FirstName,E.LastName) EmployeeName,
	TC.EventDate ,
	DateDiff(HOUR,InTime,OutTime) LoginTime,
	InTime,
	OutTime,
	RoleName,
	Case 
	when EventDate = @Date then 'On' 
	else 'Off' end as Checked
	 
from tblTimeClock TC 
inner join tblEmployee E on TC.EmployeeId = E.EmployeeId
inner join tblRoleMaster R on Tc.RoleId = R.RoleMasterId
where LocationId = @LocationId and EventDate = @Date) 

SELECT 
	EmployeeName,EventDate,ISNULL(Logintime,0) AS HoursPerDay,InTime,OutTime,Checked--(SUM(ISNULL(Logintime,0))/60) AS LoginHours 
FROM Hours_Data where RoleName = 'Wash'

END