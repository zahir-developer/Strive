


CREATE PROC [StriveCarSalon].[uspGetJobById] 
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
,CONCAT(tblclv.VehicleModel,' ',tblclv.VehicleMfr,' ',tblclv.VehicleColor) AS VehicleName
,tbj.JobType
,tbj.JobDate
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbj.ActualTimeOut
,tbj.JobStatus
,tblji.ServiceId
,tbls.ServiceName
,tblji.Commission
,tblji.Price
,tblji.Quantity
,tblji.ReviewNote
,@ReviewNote AS PastHistoryNote
from 
StriveCarSalon.tblJob tbj 
inner join StriveCarSalon.GetTable('ServiceType') tblcv on tbj.JobType = tblcv.valueid
inner join StriveCarSalon.tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
inner join StriveCarSalon.tblClientAddress tblca on tbj.ClientId = tblca.ClientId
inner join StriveCarSalon.tblClient tblc on tbj.ClientId = tblc.ClientId
inner join StriveCarSalon.tblJobItem tblji on tbj.JobId = tblji.JobId
inner join StriveCarSalon.tblService tbls on tblji.ServiceId = tbls.ServiceId
WHERE
(tbj.JobId = @JobId)
AND 
tblcv.valuedesc='Washes'
AND isnull(tblca.IsDeleted,0)=0
AND isnull(tblc.IsDeleted,0)=0
AND isnull(tblji.IsDeleted,0)=0
AND isnull(tbls.IsDeleted,0)=0
AND tblji.IsActive=1
END