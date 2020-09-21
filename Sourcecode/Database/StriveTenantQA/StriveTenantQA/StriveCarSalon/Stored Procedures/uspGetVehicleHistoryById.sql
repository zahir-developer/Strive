
CREATE PROCEDURE [StriveCarSalon].[uspGetVehicleHistoryById] 
(@ClientId int)
AS
BEGIN
Select 
tblj.TicketNumber,
tblj.JobDate AS Date,
tbls.ServiceName AS ServiceCompleted,
tbljp.Amount,
tblji.Price,
tblji.Commission
from [StriveCarSalon].[tblJob] tblj 
inner join [StriveCarSalon].[tblJobItem] tblji on(tblj.JobId = tblji.JobId)
inner join [StriveCarSalon].[tblService] tbls on(tblji.ServiceId = tbls.ServiceId)
inner join [StriveCarSalon].[tblJobPayment] tbljp on(tblji.JobId = tbljp.JobId)
WHERE
tblj.ClientId=@ClientId
END

