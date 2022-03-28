CREATE PROCEDURE [StriveCarSalon].[uspGetVehicleStatementByClientId] 
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
tblj.TicketNumber,
tblj.JobDate AS Date,
tbls.ServiceName AS ServiceCompleted,
tbljp.Amount AS TotalAmount
into #temp
from [StriveCarSalon].[tblJob] tblj 
left join [StriveCarSalon].[tblJobItem] tblji on(tblj.JobId = tblji.JobId)
left join [StriveCarSalon].GetTable('JobStatus') gt on(tblj.JobStatus = gt.valueid)
left join [StriveCarSalon].[tblService] tbls on(tblji.ServiceId = tbls.ServiceId)
left join [StriveCarSalon].[tblJobPayment] tbljp on(tblj.JobPaymentId = tbljp.JobPaymentId)
WHERE
tblj.ClientId=@ClientId
--AND
--gt.valuedesc='Completed'

 --select temp table

 select 
 
TicketNumber
,[Date]
,ServiceCompleted
,TotalAmount
 from #temp 
 order by  TicketNumber
 OFFSET (@Skip) ROWS FETCH NEXT (@PageSize) ROWS ONLY
 --temp table count
 select count(*) AS Count from #temp  
 -- drop temp table
 drop table if exists #temp


END
