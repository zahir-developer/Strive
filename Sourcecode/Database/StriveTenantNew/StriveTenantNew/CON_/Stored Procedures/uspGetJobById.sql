

CREATE PROCEDURE [CON].[uspGetJobById] 
(@JobId int)
AS
BEGIN
DECLARE @ReviewNote varchar(50);
DECLARE @ClientId int;
DECLARE @Count int;
SELECT @ClientId = (select ClientId from StriveCarSalon.tblJob where JobId=@JobId)
SELECT @Count = (select count(1) from StriveCarSalon.tbljob where ClientId=@ClientId)
if(@Count > 1)
BEGIN
SELECT @ReviewNote = (select * from (select top 2 JD.ReviewNote from StriveCarSalon.tblJob J
JOIN StriveCarSalon.tblJobDetail JD on Jd.JobId = J.JobId 
and (J.ClientId = @ClientId) ORDER BY J.JobDate DESC) x
except
select * from (select top 1 JD.ReviewNote from StriveCarSalon.tblJob J
JOIN StriveCarSalon.tblJobDetail JD on Jd.JobId = J.JobId 
and(J.ClientId = @ClientId) ORDER BY J.JobDate DESC) y)
END
else
BEGIN
SELECT @ReviewNote = (Select * from (select top 1 JD.ReviewNote from StriveCarSalon.tblJob J
JOIN StriveCarSalon.tblJobDetail JD on Jd.JobId = J.JobId 
and (J.ClientId = @ClientId) ORDER BY J.JobDate DESC)a)
END
Select 
tbj.JobId
,tbj.TicketNumber
,tbj.LocationId
,tbj.ClientId
,CONCAT(tblc.FirstName,' ',tblc.LastName) AS ClientName
,tblca.PhoneNumber
,tbj.VehicleId
,tbj.Make
,tbj.Model
,tbj.Color
,tbj.JobType
,tbj.JobDate
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbj.ActualTimeOut
,tbj.JobStatus
,tblji.ServiceId
,tbls.ServiceType as ServiceTypeId
,tbls.ServiceName
,tblji.Commission
,tblji.Price
,tblji.Quantity
,tblji.ReviewNote
--,@ReviewNote AS PastHistoryNote
from 
StriveCarSalon.tblJob tbj 
INNER JOIN StriveCarSalon.tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
LEFT JOIN StriveCarSalon.tblClientAddress tblca on tbj.ClientId = tblca.ClientId
LEFT JOIN StriveCarSalon.tblClient tblc on tbj.ClientId = tblc.ClientId
LEFT JOIN StriveCarSalon.tblJobItem tblji on tbj.JobId = tblji.JobId
LEFT JOIN StriveCarSalon.tblService tbls on tblji.ServiceId = tbls.ServiceId
LEFT JOIN StriveCarSalon.GetTable('ServiceType') tblcv on tbls.ServiceType = tblcv.valueid
WHERE tblcv.valuedesc='Washes'
AND isnull(tbj.IsDeleted,0)=0
AND isnull(tblca.IsDeleted,0)=0
AND isnull(tblc.IsDeleted,0)=0
AND isnull(tblji.IsDeleted,0)=0
AND isnull(tbls.IsDeleted,0)=0
AND isnull(tblji.IsActive,1)=1
AND ((tbj.JobId = @JobId) OR @JobId IS NULL)


Select 
JobItemId,
JobId,
tblji.ServiceId,
s.ServiceType as ServiceTypeId,
Commission,
Price,
Quantity,
ReviewNote
from StriveCarSalon.tblJobItem tblji
LEFT JOIN StriveCarSalon.tblService s on s.ServiceId = tblji.ServiceId
WHERE (JobId = @JobId OR @JobId IS NULL)
AND isnull(tblji.IsDeleted,0)=0

END