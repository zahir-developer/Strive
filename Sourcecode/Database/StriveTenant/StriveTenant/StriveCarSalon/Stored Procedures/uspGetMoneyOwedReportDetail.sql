-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 2021-05-03
-- Description:	Retrives Money owned report details.
-- Sample: StriveCarSalon.uspGetMoneyOwedReportDetail 2, '2021-11'
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetMoneyOwedReportDetail] 
@LocationId int,
@Date varchar(7)
AS
BEGIN

SET NOCOUNT ON;

--DECLARE @LocationId INT = 1

--DECLARE @Date VARCHAR(10) = '2021-07'

DECLARE @JobStatusCompleted INT =(SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')

DECLARE @JobTypeWash INT =(SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')

DECLARE @PaymentStatusSuccess INT =(SELECT valueid FROM GetTable('PaymentStatus') WHERE valuedesc='Success')

DECLARE @WashServiceType INT =(SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Wash Package')

DECLARE @PaymentTypeMembership INT =(SELECT valueid FROM GetTable('PaymentType') WHERE valuedesc='Membership')

DROP TABLE IF EXISTS #MembershipClients

Select c.ClientId, c.FirstName, c.LastName, cv.VehicleId, cv.Barcode, cm.ClientMembershipId, cm.LocationId, loc.LocationName, cm.TotalPrice as MembershipAmount,
ms.ClientVehicleMembershipServiceId,
s.Price
INTO #MembershipClients 
from tblClientVehicle (nolock) cv 
JOIN tblClient (nolock) c on cv.ClientId = c.ClientId
JOIN tblClientVehicleMembershipDetails (nolock) cm on cm.ClientVehicleId = cv.VehicleId and cm.IsDeleted = 0
JOIN tblLocation (nolock) loc on loc.LocationId = cm.LocationId 
JOIN tblClientVehicleMembershipService (nolock) ms on ms.ClientMembershipId = cm.ClientMembershipId and ms.IsDeleted = 0
JOIN tblService (nolock) s on s.ServiceId = ms.ServiceId
JOIN tblCodeValue (nolock) cdv on cdv.id = s.ServiceType
WHERE cm.IsDeleted = 0 and cm.LocationId = @locationId --and cm.TotalPrice is not null
--and ms.IsDeleted = 0
--and c.ClientId in  (52100,47537)
--and s.ServiceType = @WashServiceType AND cm.TotalPrice is NOT NULL


DROP TABLE IF EXISTS #tblJob

Select JobId, LocationId, ClientId, VehicleId into #tblJob from tblJob
where SUBSTRING(CAST(JobDate AS VARCHAR(10)),1,7) = @Date

DROP TABLE IF EXISTS #MembershipClientJobs

Select count(distinct j.jobId) WashCount, j.LocationId, l.LocationName, j.ClientId, j.VehicleId, mc.Barcode, c.FirstName, c.LastName, mc.MembershipAmount
into #MembershipClientJobs
from #MembershipClients mc
JOIN #tblJob j on 
--j.ClientId = mc.ClientId and 
mc.VehicleId = j.VehicleId
JOIN tblLocation l on l.LocationId = j.LocationId
JOIN tblClient (nolock) c on c.ClientId = j.ClientId 
--where c.ClientId = 39323

GROUP BY j.LocationId, l.LocationName, j.ClientId, j.VehicleId, mc.Barcode, c.FirstName, c.LastName, mc.MembershipAmount


DROP TABLE IF EXISTS #ClientTotalJobAmount

Select j.VehicleId, SUM(ISNULL(mc.Price,0)) as TotalJobAmount
into #ClientTotalJobAmount
from #MembershipClients mc
JOIN #tblJob j on j.ClientId = mc.ClientId and mc.VehicleId = j.VehicleId
JOIN tblLocation l on l.LocationId = j.LocationId
--JOIN tblJobItem ji on ji.JobId = j.JobId
JOIN tblClient (nolock) c on c.ClientId = j.ClientId 
--where c.ClientId = 39323

GROUP BY j.vehicleId

--Select * from #MembershipClientJobs
DROP TABLE IF EXISTS #ClientWashCount

DROP TABLE IF EXISTS #ClientJobAvg

Select TotalJobAmount/MembershipAmount as Avgerage, cj.VehicleId, cj.Barcode into #ClientJobAvg 
from #MembershipClientJobs cj 
JOIN #ClientTotalJobAmount ja on ja.VehicleId = cj.VehicleId

where MembershipAmount != 0


DROP TABLE IF EXISTS #ClientTotalWash

Select SUM(wc.WashCount) TotalWashCount, wc.VehicleId into #ClientTotalWash from #MembershipClientJobs wc
GROUP BY VehicleId


DROP TABLE IF EXISTS #MoneyOwedReport

Select cj.ClientId, cj.VehicleId, cj.Barcode, cj.FirstName, cj.LastName, cj.LocationId, cj.LocationName, cj.MembershipAmount,cj.WashCount, tj.TotalJobAmount ,tw.TotalWashCount, CAST(Round(MembershipAmount/TotalWashCount,2) as decimal(18,2)) Average into #MoneyOwedReport 
from #MembershipClientJobs cj
JOIN #ClientTotalJobAmount tj on cj.VehicleId = tj.VehicleId
LEFT JOIN #ClientTotalWash tw on tw.VehicleId = cj.VehicleId

DECLARE @LocationAvg DECIMAL(18,2) = (Select top 1 WashCount from #MoneyOwedReport where LocationId = @LocationId)

Select WashCount, LocationId, LocationName, VehicleId, Barcode, ClientId, FirstName, LastName, TotalJobAmount, MembershipAmount, TotalWashCount, Average, 
MoneyOwed = CASE  
WHEN WashCount = 1 THEN TotalJobAmount
WHEN WashCount > 1 THEN (Average * WashCount)
END 
from #MoneyOwedReport mw order by FirstName, LastName


Select DISTINCT ClientId, VehicleId, FirstName, LastName from #MoneyOwedReport order by FirstName, LastName, VehicleId

Select locationId, LocationName from tbllocation where IsDeleted = 0


END