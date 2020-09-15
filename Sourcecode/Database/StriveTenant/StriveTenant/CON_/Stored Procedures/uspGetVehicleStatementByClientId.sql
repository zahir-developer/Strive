


CREATE PROCEDURE [CON].[uspGetVehicleStatementByClientId] 
(@ClientId int)
AS
BEGIN
Select 
tblj.TicketNumber,
tblj.JobDate AS Date,
tbls.ServiceName AS ServiceCompleted,
tbljp.Amount AS TotalAmount

from [CON].[tblJob] tblj 
inner join [CON].[tblJobItem] tblji on(tblj.JobId = tblji.JobId)
inner join [CON].GetTable('JobStatus') gt on(tblj.JobStatus = gt.valueid)
inner join [CON].[tblService] tbls on(tblji.ServiceId = tbls.ServiceId)
inner join [CON].[tblJobPayment] tbljp on(tblji.JobId = tbljp.JobId)
WHERE
tblj.ClientId=@ClientId
AND
gt.valuedesc='Completed'
END
