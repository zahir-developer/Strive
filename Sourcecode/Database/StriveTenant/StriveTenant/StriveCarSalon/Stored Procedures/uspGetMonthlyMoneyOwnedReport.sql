


CREATE procedure [StriveCarSalon].[uspGetMonthlyMoneyOwnedReport] --'2020-11'
(@Date varchar(7))
AS 
BEGIN
DECLARE @CompletedJobStatus INT =(SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')
DECLARE @WashJobType INT =(SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')
DECLARE @WashServiceType INT =(SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Washes')


DROP TABLE IF EXISTS #TotalWashCount
SELECT tblj.JobDate,tblj.LocationId,tblj.ClientId,
tbll.LocationName,tblc.FirstName,tblc.LastName,
ISNULL(tblcvmd.TotalPrice,0.00)AccountAmount,CAST(SUM(ISNULL(tbls.Cost,0.00)) AS decimal(9,2))WashesAmount,
Count(tblj.JobId) NumberOfWashes--,CAST((tblcvmd.TotalPrice/tblj.JobId) AS decimal)Average
INTO #TotalWashCount FROM tblJob tblj INNER JOIN tblJobItem tblji ON(tblj.JobId=tblji.JobId) 
INNER JOIN tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN tblClientVehicleMembershipDetails tblcvmd ON(tblj.VehicleId = tblcvmd.ClientVehicleId)
INNER JOIN tblClient tblc ON(tblj.ClientId = tblc.ClientId)
INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
WHERE tblj.JobType=@WashJobType AND tbls.ServiceType=@WashServiceType AND tblj.JobStatus=@CompletedJobStatus 
AND SUBSTRING(CAST(tblj.JobDate AS VARCHAR(10)),1,7)='2020-11' AND 
tblj.IsActive=1 AND tblji.IsActive=1 AND tbls.IsActive=1 AND tblcvmd.IsActive=1
AND tblc.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0
AND ISNULL(tblji.IsDeleted,0)=0 AND ISNULL(tbls.IsDeleted,0)=0 AND ISNULL(tblcvmd.IsDeleted,0)=0
AND ISNULL(tblc.IsDeleted,0)=0 AND ISNULL(tbll.IsDeleted,0)=0 
GROUP BY tblj.JobDate,tblj.LocationId,tblj.ClientId,
tbll.LocationName,tblc.FirstName,tblc.LastName,tblcvmd.TotalPrice

select JobDate,LocationId,ClientId,
LocationName,CONCAT(FirstName,' ',LastName)CustomerName,
AccountAmount,WashesAmount,NumberOfWashes,CAST((AccountAmount/NumberOfWashes) AS decimal(9,2))Average from #TotalWashCount

END