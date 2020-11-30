-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetDailyStatusReport] -- strivecarsalon.uspGetDailyStatusReport 2047,'2020-11-18'
	@LocationId INT,
	@Date datetime
AS
BEGIN
	select Count(JI.serviceId) as Number, 0 as JobId ,S.ServiceName,JT.valueid as JobTypeId, JT.valuedesc as JobType, JobDate
from tblJob J
INNER join tblJobItem JI on J.JobId = JI.JobId
INNER join tblService S on S.ServiceId = JI.ServiceId
INNER join GetTable('JobType') JT on JT.valueId = J.JobType-- and GT.valuedesc = 'Completed'
INNER join GetTable('ServiceType') ST on ST.valueId = S.ServiceType AND (ST.valuedesc = 'Washes' OR ST.valuedesc = 'Details')-- and GT.valuedesc = 'Completed'

where J.LocationId = @LocationId and JobDate = @Date
group by S.ServiceName, JT.valueid, JT.valuedesc, JobDate


select L.LocationId,TicketNumber,js.EmployeeId, e.FirstName, e.LastName ,Sum(JI.Commission) as Commission
from tblLocation L
 join tblJob J on J.LocationId = L.LocationId
left join tblJobItem JI on J.JobId = JI.JobId
left join tblJobServiceEmployee JS on JS.JobItemId = JI.JobItemId
left join tblService S on S.ServiceId = JI.ServiceId
JOIN tblEmployee e on e.EmployeeId = js.EmployeeId
left join GetTable('JobStatus') GT on GT.id = JobStatus and GT.valuedesc = 'Completed'
where L.LocationId = @LocationId and JobDate = @Date and JS.EmployeeId != null
group by L.LocationId,TicketNumber,js.EmployeeId, e.FirstName, e.LastName

END