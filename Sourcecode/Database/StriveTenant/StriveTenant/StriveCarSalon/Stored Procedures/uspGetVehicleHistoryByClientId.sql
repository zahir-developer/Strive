--[StriveCarSalon].[uspGetVehicleHistoryByClientId] 1846
CREATE PROCEDURE [StriveCarSalon].[uspGetVehicleHistoryByClientId] 
@ClientId int,
@PageNo INT = NULL,
@PageSize INT = NULL	
AS
BEGIN
DECLARE @Skip INT = 0;
IF @PageSize is not NULL
BEGIN
SET @Skip = @PageSize * (@PageNo-1);
END

IF @PageSize is NULL
BEGIN
SET @PageSize = (Select count(1) from StriveCarSalon.[tblJob]);
SET @PageNo = 1;
SET @Skip = @PageSize * (@PageNo-1);
Print @PageSize
Print @PageNo
Print @Skip
END

Select 
tblj.JobId,
tblj.TicketNumber,
tblj.JobDate AS Date,
tbls.ServiceName AS ServiceCompleted,
tbljp.Amount,
tblji.Price,
tblji.Commission AS Comm,
jt.valuedesc as JobType,
St.valuedesc as ServiceType
from [StriveCarSalon].[tblJob] tblj 
left join [StriveCarSalon].[tblJobItem] tblji on(tblj.JobId = tblji.JobId)
left join [StriveCarSalon].GetTable('JobStatus') gt on(tblj.JobStatus = gt.valueid)
left join [StriveCarSalon].GetTable('JobType') jt on(tblj.JobType = jt.valueid)
left join [StriveCarSalon].[tblService] tbls on(tblji.ServiceId = tbls.ServiceId)
left join [StriveCarSalon].GetTable('ServiceType') St on(tbls.ServiceType = St.valueid)
left join [StriveCarSalon].[tblJobPayment] tbljp on(tblj.JobPaymentId = tbljp.JobPaymentId)
WHERE
tblj.ClientId=@ClientId


 order by tblj.JobDate DESC
 OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY
 --temp table count
 select count(*) AS Count  from [StriveCarSalon].[tblJob] where ISNULL(IsDeleted,0) = 0 



END