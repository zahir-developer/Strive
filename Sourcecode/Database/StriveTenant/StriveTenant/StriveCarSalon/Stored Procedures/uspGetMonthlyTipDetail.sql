
CREATE     PROCEDURE [StriveCarSalon].[uspGetMonthlyTipDetail] 
(@LocationId int = null,@Month int = null ,@year int = null)
AS

-- =============================================
-- Author:		Arunkumae S
-- Create date: 09-11-2020
-- Description:	Gets the Monthly tips detail 
-- =============================================

BEGIN
 
-- select 
--L.LocationId,
--L.LocationName,
--E.EmployeeId,
--CONCAT(E.FirstName , E.MiddleName , E.LastName) as EmployeeName,
----ER.RoleId,
----ERT.valuedesc as RoleDescription, 
----DATEDIFF(HOUR,InTime,OutTime) as TotalHours,
--SUM(DATEDIFF(HOUR,InTime,OutTime)) OVER( PARTITION BY E.EmployeeId) as HoursPerDay
----InTime,
----OutTime
--from [StriveCarSalon].tblLocation L
--inner join StriveCarSalon.tblEmployeeLocation EL on L.LocationId = EL.LocationId 
--inner join StriveCarSalon.tblEmployee E on e.EmployeeId = EL.EmployeeId 
--inner join StriveCarSalon.tblEmployeeRole ER on ER.EmployeeId = EL.EmployeeId 
--left join StriveCarSalon.GetTable('EmployeeRole') ERT on ERT.valueid = ER.RoleId 
----left join StriveCarSalon.tblEmployeeDetail ED on ED.EmployeeId = EL.EmployeeId
--left join [StriveCarSalon].[tblTimeClock] TC on TC.LocationId = el.LocationId
--where DATEPART(month,EventDate) = @Month and DATEPART(year,EventDate) = @year and L.LocationId = @LocationId and DATEDIFF(HOUR,InTime,OutTime) <> 0 
--and OutTime Is not null and ERT.valuedesc is not null and ER.RoleId is not null
--Group by L.LocationId,L.LocationName,E.EmployeeId,E.FirstName , E.MiddleName , E.LastName,InTime,OutTime
--order by L.LocationId

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