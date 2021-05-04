CREATE PROCEDURE [StriveCarSalon].[uspGetMonthlyTipDetail] 
(@LocationId int = null,@Month int = null ,@year int = null)
AS

-- =============================================
-- Author:		Arunkumae S
-- Create date: 09-11-2020
-- Description:	Gets the Monthly tips detail 
-- =============================================

BEGIN

;WITH Hours_Data
AS (
select 
	concat(E.FirstName,' ',E.LastName) EmployeeName,
	TC.EventDate ,
	DateDiff(HOUR,InTime,OutTime) LoginTime
from tblTimeClock TC 
inner join tblEmployee E on TC.EmployeeId = E.EmployeeId
where LocationId = @LocationId and DATEPART(month,EventDate) = @Month and DATEPART(year,EventDate) = @year
		AND Tc.Isdeleted=0)

SELECT 
	EmployeeName, SUM(ISNULL(Logintime,0)) AS HoursPerDay --(SUM(ISNULL(Logintime,0))/60) AS LoginHours 
FROM Hours_Data
Group by EmployeeName




END