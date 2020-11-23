
-- =============================================
-- Author:		Arunkumae S
-- Create date: 09-11-2020
-- Description:	Gets the Daily Status Detail Info
-- =============================================




CREATE PROC [StriveCarSalon].[uspGetDailyStatusDetailInfo] --[StriveCarSalon].[uspGetDailyStatusDetailInfo] 2046,'2020-11-20'
(@LocationId int = null,@Date Date)
AS
BEGIN
 
-- select L.LocationId,TicketNumber,Js.SalesRep As EmployeeId,E.FirstName,tblRM.RoleName,Sum(JI.Commission) as Commission
--from tblLocation L
-- join tblJob J on J.LocationId = L.LocationId
--left join tblJobItem JI on J.JobId = JI.JobId
--left join tblJobDetail  JS on JS.JobId = J.JobId
--left join tblEmployee E on E.EmployeeId = JS.SalesRep
--left join tblService S on S.ServiceId = JI.ServiceId
--left join tblTimeClock tblTC on tblTC.EmployeeId = E.EmployeeId
--left join tblRoleMaster tblRM on tblRM.RoleMasterId = tblTC.RoleId
--left join GetTable('JobStatus') GT on GT.id = JobStatus and GT.valuedesc = 'Completed'
--where L.LocationId = @LocationId and JobDate = @Date and Js.SalesRep is not null
--group by L.LocationId,TicketNumber,js.SalesRep,E.FirstName,tblRM.RoleName

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


END