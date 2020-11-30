







-- =============================================
-- Author:		Arunkumae S
-- Create date: 09-11-2020
-- Description:	Gets the Daily tips detail 
-- =============================================


CREATE PROC [StriveCarSalon].[uspGetDailyTipDetail] -- [StriveCarSalon].[uspGetDailyTipDetail] '2020-11-08', 2034
(@Date Date = null, @locationId int = null)
AS
BEGIN
 
 --Select * from StriveCarSalon.tblTimeClock order by 1 desc

--Select 
--L.LocationId,
--L.LocationName,
--E.EmployeeId,
----CONCAT(E.FirstName, E.LastName) as EmployeeName,
----ER.RoleId,
----ERT.valuedesc as RoleDescription, 
----DATEDIFF(HOUR,InTime,OutTime) as TotalHours
--SUM(DATEDIFF(HOUR,InTime,OutTime)) as HoursPerDay
----InTime,
----OutTime
--from [StriveCarSalon].tblLocation L
--inner join StriveCarSalon.tblEmployeeLocation EL on L.LocationId = EL.LocationId 
--inner join StriveCarSalon.tblEmployee E on e.EmployeeId = EL.EmployeeId 
--inner join StriveCarSalon.tblEmployeeRole ER on ER.EmployeeId = EL.EmployeeId 
--LEFT join StriveCarSalon.GetTable('EmployeeRole') ERT on ERT.valueid = ER.RoleId 
----left join StriveCarSalon.tblEmployeeDetail ED on ED.EmployeeId = EL.EmployeeId
--INNER join [StriveCarSalon].[tblTimeClock] TC on TC.LocationId = el.LocationId and E.EmployeeId = TC.EmployeeId
--where EventDate = @Date and L.LocationId = @locationId and OutTime Is not null and ERT.valuedesc is not null and ER.RoleId is not null 
--Group by L.LocationId, L.LocationName, E.EmployeeId--, InTime, OutTime
--order by L.LocationId


;WITH Hours_Data
AS (
select 
	concat(E.FirstName,E.LastName) EmployeeName,
	TC.EventDate ,
	DateDiff(HOUR,InTime,OutTime) LoginTime
from tblTimeClock TC 
inner join tblEmployee E on TC.EmployeeId = E.EmployeeId
where LocationId = @LocationId and EventDate = @Date)

SELECT 
	EmployeeName,EventDate,SUM(ISNULL(Logintime,0)) AS HoursPerDay --(SUM(ISNULL(Logintime,0))/60) AS LoginHours 
FROM Hours_Data
Group by EmployeeName,EventDate

END