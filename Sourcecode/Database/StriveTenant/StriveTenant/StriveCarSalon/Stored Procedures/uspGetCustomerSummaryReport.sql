
CREATE PROCEDURE [StriveCarSalon].[uspGetCustomerSummaryReport]
@LocationId INT, @Date VARCHAR(4)
AS

-- =================================================
-- Author:		Vineeth B
-- Create date: 03-10-2020
-- Description:	To get CustomerSummaryReport Details
-- Sample: EXEC [uspGetCustomerSummaryReport] 1,'2021'
-- =================================================
-- =============================================
----------History------------

-- =============================================

-- 05-Jul-2021 - Vetriselvi - The Wash Vehilce must have membership
-- 15-Jul-2021 - Vetriselvi - If number of Membership Accounts is zero then the percentage should be zero
-- 19-Jul-2021 - Vetriselvi - Optimized the query
-- 20-Jul-2021 - Vetriselvi - Added date filter in Customer count

-- =============================================

BEGIN

DROP TABLE  IF EXISTS #tblJob
SELECT VehicleId,JobDate,ClientId,JobType,JobStatus,cv.codevalue
INTO
#tblJob
FROM tblJob j
INNER JOIN tblClientVehicleMembershipDetails md on md.ClientVehicleId = VehicleId
INNER JOIN tblJobPayment jp on jp.JobPaymentId = j.JobPaymentId
INNER JOIN tblJobPaymentDetail jpd on jpd.JobPaymentId = jp.JobPaymentId
INNER JOIN tblCodeValue cv on cv.id = jpd.PaymentType
WHERE md.LocationId=@LocationId
AND SUBSTRING(CAST(JobDate AS VARCHAR(10)),1,4) = @Date
AND j.IsActive=1 AND ISNULL(j.IsDeleted,0)=0 AND md.IsDeleted = 0
AND cv.codevalue = 'Membership'

--Select * from #tblJob

--Select CAST(DATEPART(MONTH,JobDate)AS INT) Month, count(distinct clientId) from #tblJob GROUP BY DATEPART(MONTH,JobDate) order by Month


DROP TABLE  IF EXISTS #tblClientVehicleMembershipDetails
SELECT 
    StartDate,
	ClientVehicleId
INTO 
	#tblClientVehicleMembershipDetails
FROM 
	tblClientVehicleMembershipDetails 
WHERE IsActive=1 AND ISNULL(IsDeleted,0)=0
AND LocationId=@LocationId
--AND SUBSTRING(CAST(StartDate AS VARCHAR(10)),1,4)=@Date


DROP TABLE  IF EXISTS #NumberOfMembershipAccounts
SELECT 
    CAST(DATEPART(MONTH,StartDate)AS INT) Month,
	CAST(COUNT(*) AS DECIMAL) NumberOfMembershipAccounts
INTO 
	#NumberOfMembershipAccounts
FROM 
	#tblClientVehicleMembershipDetails 
GROUP BY DATEPART(MONTH,StartDate)

DROP TABLE  IF EXISTS #Vehicle
SELECT JobDate as Month,
COUNT(DISTINCT VehicleId) Vehilce
INTO 
	#Vehicle
from (
SELECT 
	DISTINCT DATEPART(MONTH,JobDate) JobDate,
	 VehicleId
FROM 
	#tblJob j
	join #tblClientVehicleMembershipDetails cvmd ON j.VehicleId = cvmd.ClientVehicleId
	and DATEPART(MONTH,JobDate) = DATEPART(MONTH,cvmd.StartDate)
	) A
WHERE ISNULL(VehicleId,0) != 0
GROUP BY JobDate

DROP TABLE  IF EXISTS #WashesCompletedCount
SELECT 
	DATEPART(MONTH,JobDate) Month,
	CAST(COUNT(*) AS DECIMAL)WashesCompletedCount 
INTO 
	#WashesCompletedCount
FROM 
	#tblJob j
	join #tblClientVehicleMembershipDetails cvmd ON j.VehicleId = cvmd.ClientVehicleId 
WHERE JobType =(SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')
AND JobStatus in (SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed' OR valuedesc = 'Paid')
GROUP BY DATEPART(MONTH,JobDate)


DROP TABLE IF EXISTS #CustSummaryResult

SELECT  
	Veh.Month,
	SUM (Noma.NumberOfMembershipAccounts) OVER (ORDER BY Veh.[Month]) AS RunningTotalNumberOfMembershipAccounts,
	ISNULL(Noma.NumberOfMembershipAccounts,0) NumberOfMembershipAccounts,
	ISNULL(Veh.Vehilce,0) VehicleCount,
	ISNULL(Wcc.WashesCompletedCount,0) WashesCompletedCount
into #CustSummaryResult
FROM 
	#Vehicle Veh
LEFT JOIN
	#NumberOfMembershipAccounts Noma
ON	Noma.Month=veh.Month
LEFT JOIN
	#WashesCompletedCount Wcc
ON	Wcc.Month=Veh.Month


Select 
	[Month],
	ISNULL(RunningTotalNumberOfMembershipAccounts,0) NumberOfMembershipAccounts,
	ISNULL(VehicleCount,0) VehicleCount,
	ISNULL(WashesCompletedCount,0) WashesCompletedCount,
CASE	WHEN ISNULL(VehicleCount,0)=0 THEN ISNULL(WashesCompletedCount,0) 
			ELSE CAST((ISNULL(WashesCompletedCount,0)/ISNULL(VehicleCount,0))AS decimal(9,2)) END AS AverageNumberOfWashesPerVehicle,
	CASE	WHEN ISNULL(RunningTotalNumberOfMembershipAccounts,0)=0 THEN ISNULL(WashesCompletedCount,0)
			ELSE CAST((ISNULL(WashesCompletedCount,0)/ISNULL(RunningTotalNumberOfMembershipAccounts,0)) AS decimal(9,2)) END AS TotalNumberOfWashesPerVehicle,
	CASE	WHEN ISNULL(RunningTotalNumberOfMembershipAccounts,0)=0 THEN 0
			ELSE CAST(((ISNULL(VehicleCount,0)/ISNULL(RunningTotalNumberOfMembershipAccounts,0)) * 100)AS decimal(9,2)) END AS PercentageOfVehicleThatTurnedUp
			from
			#CustSummaryResult


DROP TABLE  IF EXISTS #NumberOfMembershipAccounts
DROP TABLE  IF EXISTS #Vehicle
DROP TABLE  IF EXISTS #WashesCompletedCount


END