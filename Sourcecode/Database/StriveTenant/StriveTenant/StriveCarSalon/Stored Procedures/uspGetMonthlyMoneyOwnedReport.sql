

CREATE procedure [StriveCarSalon].[uspGetMonthlyMoneyOwnedReport] --'2020-11'
(@Date varchar(7))
AS 
BEGIN
DECLARE @CompletedJobStatus INT =(SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')
DECLARE @WashJobType INT =(SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')
DECLARE @WashServiceType INT =(SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Washes')

DROP TABLE IF EXISTS #TotalWashCount
SELECT tblj.JobDate,Count(*) NumberOfWashes
INTO #TotalWashCount FROM tblJob tblj INNER JOIN tblJobItem tblji ON(tblj.JobId=tblji.JobId) 
INNER JOIN tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN tblClientVehicleMembershipDetails tblcvmd ON(tblj.VehicleId = tblcvmd.ClientVehicleId)
INNER JOIN tblClient tblc ON(tblj.ClientId = tblc.ClientId)
INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
WHERE tblj.JobType=@WashJobType AND tbls.ServiceType=@WashServiceType AND tblj.JobStatus=@CompletedJobStatus 
AND SUBSTRING(CAST(tblj.JobDate AS VARCHAR(10)),1,7)=@Date AND 
tblj.IsActive=1 AND tblji.IsActive=1 AND tbls.IsActive=1 AND tblcvmd.IsActive=1
AND tblc.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0
AND ISNULL(tblji.IsDeleted,0)=0 AND ISNULL(tbls.IsDeleted,0)=0 AND ISNULL(tblcvmd.IsDeleted,0)=0
AND ISNULL(tblc.IsDeleted,0)=0 AND ISNULL(tbll.IsDeleted,0)=0 
GROUP BY tblj.JobDate

DROP TABLE IF EXISTS #Detail
SELECT tblj.JobDate,tblc.ClientId,tbll.LocationId,tbll.LocationName,tblc.FirstName,tblc.LastName,
ISNULL(tblcvmd.TotalPrice,0.00)AccountAmount,ISNULL(tbls.Cost,0.00) WashesAmount
INTO #Detail FROM tblJob tblj INNER JOIN tblJobItem tblji ON(tblj.JobId=tblji.JobId) 
INNER JOIN tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN tblClientVehicleMembershipDetails tblcvmd ON(tblj.VehicleId = tblcvmd.ClientVehicleId)
INNER JOIN tblClient tblc ON(tblj.ClientId = tblc.ClientId)
INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
WHERE tblj.JobType=@WashJobType AND tbls.ServiceType=@WashServiceType AND tblj.JobStatus=@CompletedJobStatus 
AND SUBSTRING(CAST(tblj.JobDate AS VARCHAR(10)),1,7)=@Date AND 
tblj.IsActive=1 AND tblji.IsActive=1 AND tbls.IsActive=1 AND tblcvmd.IsActive=1
AND tblc.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0
AND ISNULL(tblji.IsDeleted,0)=0 AND ISNULL(tbls.IsDeleted,0)=0 AND ISNULL(tblcvmd.IsDeleted,0)=0
AND ISNULL(tblc.IsDeleted,0)=0 AND ISNULL(tbll.IsDeleted,0)=0
SELECT DISTINCT CAST(dt.JobDate AS date)JobDate,dt.ClientId,dt.LocationId,dt.LocationName,CONCAT(dt.FirstName,' ',dt.LastName) AS CustomerName,
twc.NumberOfWashes,dt.AccountAmount,dt.WashesAmount,CAST((dt.AccountAmount/twc.NumberOfWashes) AS decimal)Average FROM #TotalWashCount twc INNER JOIN #Detail dt ON(twc.JobDate = dt.JobDate)

END