CREATE PROCEDURE [StriveCarSalon].[uspGetDailyStatusDetailsAndWashes] 
(@LocationId int = null,@Date Date)
AS
-- =============================================
-- Author:		Arunkumae S
-- Create date: 09-11-2020
-- Description:	Gets the Monthly tips detail 
-- =============================================
BEGIN
 
 select Count(JI.serviceId) as Number ,S.ServiceName,JobDate
from tblLocation L
--inner join tblService S on L.LocationId = S.LocationId
--left join tblJobServiceEmployee  JS on JS.ServiceId = S.ServiceId
 join tblJob J on J.LocationId = L.LocationId
left join tblJobItem JI on J.JobId = JI.JobId
left join tblService S on S.ServiceId = JI.ServiceId
left join GetTable('JobStatus') GT on GT.id = JobStatus and GT.valuedesc = 'Completed'
where L.LocationId = @LocationId and JobDate = @Date
group by L.LocationId,S.ServiceName,JobDate


END