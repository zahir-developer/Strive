
-- =============================================
-- Author:		Zahir Hussain 
-- Create date: 03-12-2020
-- Description:	Gets the Daily Status - Detail Info and Wash Info 
 --[StriveCarSalon].[uspGetDailyStatusInfo] 20,'2021-03-15'
-- =============================================


CREATE PROC [StriveCarSalon].[uspGetDailyStatusInfo]
(@LocationId int = null,@Date Date)
AS
BEGIN

select L.LocationId,J.TicketNumber,SE.EmployeeId As EmployeeId, CONCAT(E.FirstName + ' ', E.LastName) as EmployeeName , Sum(SE.CommissionAmount) as Commission
from tblLocation L
join tblJob J on J.LocationId = L.LocationId
left join tblJobItem JI on J.JobId = JI.JobId
left join tblJobDetail  JS on JS.JobId = J.JobId
left join tblJobServiceEmployee SE on SE.JobItemId = ji.JobItemId
INNER join tblEmployee E on E.EmployeeId = SE.EmployeeId
left join GetTable('JobStatus') GT on GT.id = JobStatus and GT.valuedesc = 'Completed'
where L.LocationId = @LocationId and JobDate = @Date --and Js.SalesRep is not null
group by L.LocationId,J.TicketNumber,SE.EmployeeId,E.FirstName, E.LastName


Select count(e.EmployeeId) WashEmployeeCount, SUM(ISNULL(DateDiff(HOUR,InTime,OutTime), 0) * ISNULL(ed.WashRate,0)) as WashExpense from tblTimeClock tc 
INNER JOIN tblEmployee e on e.EmployeeId = tc.EmployeeId
INNER JOIN tblRoleMaster r on r.RoleMasterId = tc.RoleId and r.RoleName = 'Washer'
LEFT JOIN tblEmployeeDetail ed on e.EmployeeId = ed.EmployeeId 
where tc.EventDate = @Date

END