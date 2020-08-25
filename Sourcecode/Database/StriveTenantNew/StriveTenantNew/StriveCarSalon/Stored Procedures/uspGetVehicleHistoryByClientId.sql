
CREATE proc [StriveCarSalon].[uspGetVehicleHistoryByClientId] 
(@ClientId int)
AS
BEGIN
Select 
tblj.JobId,
tblj.TicketNumber,
tblj.JobDate AS Date,
tbls.ServiceName AS ServiceCompleted,
tbljp.Amount,
tblji.Price,
tblji.Commission AS Comm
from [StriveCarSalon].[tblJob] tblj 
inner join [StriveCarSalon].[tblJobItem] tblji on(tblj.JobId = tblji.JobId)
inner join [StriveCarSalon].GetTable('JobStatus') gt on(tblj.JobStatus = gt.valueid)
inner join [StriveCarSalon].[tblService] tbls on(tblji.ServiceId = tbls.ServiceId)
inner join [StriveCarSalon].[tblJobPayment] tbljp on(tblji.JobId = tbljp.JobId)
WHERE
tblj.ClientId=@ClientId
AND
gt.valuedesc='Completed'
END