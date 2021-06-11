CREATE PROCEDURE [StriveCarSalon].[uspGetMonthlyMoneyOwnedReport] 
(@Date varchar(7),@LocationId int)
AS
BEGIN
DECLARE @CompletedJobStatus INT =(SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')
DECLARE @WashJobType INT =(SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')
DECLARE @WashServiceType INT =(SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Wash Package')

DROP TABLE IF EXISTS #CustomerCountForLocation

SELECT tbll.LocationId, tblc.ClientId,tblj.JobDate, COUNT(tblc.FirstName) WashCount  
INTO #CustomerCountForLocation
FROM tblLocation tbll 
LEFT JOIN tblJob tblj ON(tbll.LocationId = tblj.LocationId)
LEFT JOIN tblJobItem tblji ON(tblj.JobId=tblji.JobId) 
LEFT JOIN tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
LEFT JOIN tblClientVehicleMembershipDetails tblcvmd ON(tblj.VehicleId = tblcvmd.ClientVehicleId)
LEFT JOIN tblClient tblc ON(tblj.ClientId = tblc.ClientId)
WHERE tblj.JobType=@WashJobType AND tbls.ServiceType=@WashServiceType AND tblj.JobStatus=@CompletedJobStatus 
AND SUBSTRING(CAST(tblj.JobDate AS VARCHAR(10)),1,7)=@Date AND
--AND tblj.JobDate=@Date AND 
tblj.IsActive=1 AND tblji.IsActive=1 AND tbls.IsActive=1 AND tblcvmd.IsActive=1
AND tblc.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0
AND ISNULL(tblji.IsDeleted,0)=0 AND ISNULL(tbls.IsDeleted,0)=0 AND ISNULL(tblcvmd.IsDeleted,0)=0
AND ISNULL(tblc.IsDeleted,0)=0 AND ISNULL(tbll.IsDeleted,0)=0 
GROUP BY tbll.LocationId,tblc.ClientId,tblj.JobDate

DROP TABLE IF EXISTS #TotalWashCount
SELECT tblj.JobDate,tblj.LocationId,tblj.ClientId,
tbll.LocationName,tblc.FirstName,tblc.LastName,
SUM(ISNULL(tblcvmd.TotalPrice,0.00))AccountAmount,CAST(SUM(ISNULL(tblji.Price,0.00)) AS decimal(9,2))WashesAmount,
Count(tblj.JobId) NumberOfWashes--,CAST((tblcvmd.TotalPrice/tblj.JobId) AS decimal)Average
INTO #TotalWashCount FROM tblJob tblj INNER JOIN tblJobItem tblji ON(tblj.JobId=tblji.JobId) 
INNER JOIN tblService tbls ON(tblji.ServiceId = tbls.ServiceId)
INNER JOIN tblClientVehicleMembershipDetails tblcvmd ON(tblj.VehicleId = tblcvmd.ClientVehicleId)
INNER JOIN tblClient tblc ON(tblj.ClientId = tblc.ClientId)
INNER JOIN tblLocation tbll ON(tblj.LocationId = tbll.LocationId)
WHERE tblj.JobType=@WashJobType AND tbls.ServiceType = @WashServiceType AND tblj.JobStatus=@CompletedJobStatus 
AND SUBSTRING(CAST(tblj.JobDate AS VARCHAR(10)),1,7)=@Date AND 
--AND tblj.JobDate=@Date AND 
tblj.IsActive=1 AND tblji.IsActive=1 AND tbls.IsActive=1 AND tblcvmd.IsActive=1
AND tblc.IsActive=1 AND ISNULL(tblj.IsDeleted,0)=0
AND ISNULL(tblji.IsDeleted,0)=0 AND ISNULL(tbls.IsDeleted,0)=0 AND ISNULL(tblcvmd.IsDeleted,0)=0
AND ISNULL(tblc.IsDeleted,0)=0 AND ISNULL(tbll.IsDeleted,0)=0 
GROUP BY tblj.JobDate,tblj.LocationId,tblj.ClientId,
tbll.LocationName,tblc.FirstName,tblc.LastName


--Money Owed for Each Location

--Get membership customer
DROP TABLE IF EXISTS #MembershipClientLocationDetail
SELECT 
tblcvmd.LocationId, 
tblc.ClientId,
tblcvmd.ClientVehicleId,
tbll.LocationName,
tblc.FirstName, 
tblc.LastName
INTO #MembershipClientLocationDetail
FROM 
tblClient tblc
INNER JOIN tblClientVehicle tblcv on tblcv.ClientId = tblc.ClientId
INNER JOIN tblClientVehicleMembershipDetails tblcvmd ON(tblcv.VehicleId = tblcvmd.ClientVehicleId)
INNER JOIN tblLocation tbll ON(tblcvmd.LocationId = tbll.LocationId)
WHERE tblcvmd.IsActive=1
AND tblc.IsActive=1 
AND tbll.IsActive =1
AND ISNULL(tblcvmd.IsDeleted,0)=0
AND ISNULL(tblc.IsDeleted,0)=0 AND ISNULL(tbll.IsDeleted,0)=0 

--Get Jobs done by membership customer on other franchise location
DROP TABLE IF EXISTS #membershipcustomeronotherfranchise
Select loc.LocationId,mc.ClientId, j.JobDate,SUM(ISNULL(tblji.Price,0.00)) as TotalJobAmount 
INTO #membershipcustomeronotherfranchise from tblJob j 
INNER JOIN #MembershipClientLocationDetail mc on mc.ClientId = j.ClientId and j.vehicleId = mc.ClientVehicleId and mc.LocationId != j.LocationId  
INNER JOIN tblJobItem tblji on j.JobId = tblji.JobId
INNER JOIN tblService tbls on tblji.ServiceId = tbls.ServiceId
INNER JOIN tblLocation loc on j.LocationId = loc.LocationId and loc.IsFranchise = 1
WHERE SUBSTRING(CAST(j.JobDate AS VARCHAR(10)),1,7) = @Date
--WHERE j.JobDate = @Date
AND j.JobType=@WashJobType AND tbls.ServiceType = @WashServiceType AND j.JobStatus=@CompletedJobStatus
AND j.LocationId != @LocationId
AND j.IsActive=1 AND tblji.IsActive=1 AND tbls.IsActive=1 AND loc.IsActive=1
AND ISNULL(j.IsDeleted,0)=0
AND ISNULL(tblji.IsDeleted,0)=0 AND ISNULL(tbls.IsDeleted,0)=0 AND ISNULL(loc.IsDeleted,0)=0
GROUP BY loc.LocationId,mc.ClientId, j.JobDate



select twc.JobDate,twc.LocationId,twc.ClientId,
twc.LocationName,CONCAT(twc.FirstName,' ',twc.LastName)CustomerName,
twc.AccountAmount,twc.WashesAmount,--,twc.NumberOfWashes,
cc.WashCount,CAST((twc.AccountAmount/twc.NumberOfWashes) AS decimal(9,2))Average
,mcoof.TotalJobAmount
from #TotalWashCount twc LEFT JOIN #CustomerCountForLocation cc ON(twc.ClientId = cc.ClientId AND twc.LocationId = cc.LocationId AND twc.JobDate = cc.JobDate)
LEFT JOIN #membershipcustomeronotherfranchise mcoof ON twc.JobDate = mcoof.JobDate and twc.ClientId = mcoof.ClientId and twc.locationId = mcoof.LocationId

END