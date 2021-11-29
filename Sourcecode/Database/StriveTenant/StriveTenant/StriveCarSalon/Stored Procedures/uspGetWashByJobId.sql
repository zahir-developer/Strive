-- ================================================
-- ---------------History--------------------------
-- ================================================
-- Sample Input: [StriveCarSalon].[uspGetWashByJobId]206271 205978--35746
--05-05-2021 - Zahir - Make/Model Change - tblVehiclMake/Model table used instead of tblCodeValue table.
--06-05-2021 - Zahir - Make/Model/Color NULL handled.
-- 19-05-2021 -Shalini -Make/Model/Color added alias name
CREATE PROCEDURE [StriveCarSalon].[uspGetWashByJobId]
(@JobId int)
AS
BEGIN
DECLARE @ReviewNote varchar(50);
DECLARE @ClientId int;
DECLARE @Count int;
SELECT @ClientId = (select ClientId from tblJob where JobId=@JobId)
SELECT @Count = (select count(1) from tbljob where ClientId=@ClientId)
if(@Count > 1)
BEGIN
SELECT @ReviewNote = (select * from (select top 2 JD.ReviewNote from tblJob J
JOIN tblJobDetail JD on Jd.JobId = J.JobId 
and (J.ClientId = @ClientId) ORDER BY J.JobDate DESC) x
except
select * from (select top 1 JD.ReviewNote from tblJob J
JOIN tblJobDetail JD on Jd.JobId = J.JobId 
and(J.ClientId = @ClientId) ORDER BY J.JobDate DESC) y)
END
else
BEGIN
SELECT @ReviewNote = (Select * from (select top 1 JD.ReviewNote from tblJob J
JOIN tblJobDetail JD on Jd.JobId = J.JobId 
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
,ISNULL(tbj.Make,0) as Make
,ISNULL(tbj.Model,0) as Model
,ISNULL(tbj.Color,0) as Color
,model.ModelValue AS VehicleModel
,make.MakeValue As VehicleMake
,cvCo.valuedesc as VehicleColor
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
,tbj.Notes as ReviewNote
,tblclv.Barcode
--,@ReviewNote AS PastHistoryNote
,tbj.JobPaymentId
,ISNULL(ps.valuedesc,'NotPaid') AS Paymentstatus
,Case
when (ps.valuedesc != 'Success'OR tbljp.PaymentStatus IS NULL) then 'False'
when ps.valuedesc = 'Success' then 'True'
End AS IsPaid
from 
tblJob tbj 
LEFT JOIN	tblJobPayment tbljp  WITH(NOLOCK) ON tbj.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
LEFT JOIN	GetTable('PaymentStatus') ps ON(tbljp.PaymentStatus = ps.valueid)
LEFT JOIN tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
LEFT JOIN tblClientAddress tblca on tbj.ClientId = tblca.ClientId
LEFT JOIN tblClient tblc on tbj.ClientId = tblc.ClientId
LEFT JOIN tblJobItem tblji on tbj.JobId = tblji.JobId
LEFT JOIN tblService tbls on tblji.ServiceId = tbls.ServiceId
LEFT JOIN GetTable('ServiceType') tblcv on tbls.ServiceType = tblcv.valueid

Left join tblVehicleMake make on tbj.Make=make.MakeId
Left join tblvehicleModel model on tbj.Model= model.ModelId
LEFT JOIN GetTable('VehicleColor') cvCo ON tbj.Color = cvCo.valueid
WHERE tblcv.valuedesc='Wash Package'
AND isnull(tbj.IsDeleted,0)=0
AND isnull(tblji.IsActive,1)=1
AND ((tbj.JobId = @JobId) OR @JobId IS NULL)


Select 
JobItemId,
JobId,
tblji.ServiceId,
s.ServiceName,
s.ServiceType as ServiceTypeId,
Commission,
tblji.Price,
Quantity,
ReviewNote
from tblJobItem tblji
LEFT JOIN tblService s on s.ServiceId = tblji.ServiceId
WHERE (JobId = @JobId OR @JobId IS NULL)
AND isnull(tblji.IsDeleted,0)=0

END