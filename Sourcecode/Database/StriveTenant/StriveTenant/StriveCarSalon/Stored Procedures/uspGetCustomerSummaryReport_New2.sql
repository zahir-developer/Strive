

CREATE PROCEDURE [StriveCarSalon].[uspGetCustomerSummaryReport_New2]
@LocationId INT, 
@Year VARCHAR(4)

AS

-- =================================================
-- Author:		Vineeth B
-- Create date: 03-10-2020
-- Description:	To get CustomerSummaryReport Details
-- Sample: 



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
DROP TABLE IF EXISTS #tblJob
SELECT j.TicketNumber, vehicle.VehicleId,JobDate,client.ClientId
INTO 
#tblJob
FROM tblJob j
INNER JOIN tblClientVehicleMembershipDetails md (NOLOCK) ON j.vehicleId = md.clientvehicleId 
	INNER JOIN tblClient client (NOLOCK) ON j.clientid = client.clientid 
	INNER JOIN tblClientVehicle vehicle (NOLOCK) ON j.vehicleId = vehicle.vehicleId 
	INNER JOIN tblJobPayment jp on jp.JobPaymentId = j.jobPaymentId
	INNER JOIN tblJobPaymentDetail jpd on jpd.jobPaymentId = jp.JobPaymentId
	LEFT JOIN tblcodevalue cv (NOLOCK) ON vehicle.VehicleColor = cv.id AND 
    cv.categoryId  = 30
		
	
WHERE JobType =(SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')

and (j.LocationID = @LocationID)
--and (REC.accamt > 0) 
AND ISNULL(j.IsDeleted,0)=0 AND md.IsDeleted = 0
--AND cv.codevalue = 'Account'

AND (md.isDeleted = 0)  
AND ({ fn Year(j.JobDate) } = @Year) AND md.StartDate <= CONVERT(VARCHAR(5), @Year) + '-12-31'
GROUP BY 
client.ClientId,
client.firstName, client.LastName, 
j.JobDate, 
vehicle.VehicleId, j.TicketNumber
,vehicle.vehicleModel, 
vehicle.VehicleColor, vehicle.VehicleMfr,
cv.codevalue 

--Select * from #tblJob

--Select CAST(DATEPART(MONTH,JobDate)AS INT) Month, count(distinct clientId) from #tblJob GROUP BY DATEPART(MONTH,JobDate) order by Month


DROP TABLE  IF EXISTS #tblClientVehicleMembershipDetails
SELECT 
    StartDate,
	ClientVehicleId
INTO 
	#tblClientVehicleMembershipDetails
FROM tblClientVehicleMembershipDetails md (NOLOCK) 
	INNER JOIN tblClientVehicle vehical (NOLOCK) ON md.ClientVehicleId = vehical.vehicleId 
	--INNER JOIN tblClient client (NOLOCK) ON REC.clientid = client.clientid 
AND md.LocationId=@LocationId
AND md.StartDate <= CONVERT(VARCHAR(5), @Year) + '-12-31'


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
	#tblJob)  A
WHERE ISNULL(VehicleId,0) != 0
GROUP BY JobDate

DROP TABLE  IF EXISTS #WashesCompletedCount
SELECT 
	DATEPART(MONTH,JobDate) Month,
	CAST(COUNT(j.ticketNumber) AS DECIMAL)WashesCompletedCount 
INTO 
	#WashesCompletedCount
FROM 
	#tblJob j

GROUP BY DATEPART(MONTH,JobDate)


SELECT  
	Veh.Month,
	ISNULL(Noma.NumberOfMembershipAccounts,0) NumberOfMembershipAccounts,
	ISNULL(Veh.Vehilce,0) VehicleCount,
	ISNULL(Wcc.WashesCompletedCount,0) WashesCompletedCount,
	CASE	WHEN ISNULL(Veh.Vehilce,0)=0 THEN ISNULL(wcc.WashesCompletedCount,0) 
			ELSE CAST((ISNULL(wcc.WashesCompletedCount,0)/ISNULL(Veh.Vehilce,0))AS decimal(9,2)) END AS AverageNumberOfWashesPerVehicle,
	CASE	WHEN ISNULL(noma.NumberOfMembershipAccounts,0)=0 THEN ISNULL(wcc.WashesCompletedCount,0)
			ELSE CAST((ISNULL(wcc.WashesCompletedCount,0)/ISNULL(noma.NumberOfMembershipAccounts,0)) AS decimal(9,2)) END AS TotalNumberOfWashesPerVehicle,
	CASE	WHEN ISNULL(noma.NumberOfMembershipAccounts,0)=0 THEN 0
			ELSE CAST(((ISNULL(Veh.Vehilce,0)/ISNULL(noma.NumberOfMembershipAccounts,0)) * 100)AS decimal(9,2)) END AS PercentageOfVehicleThatTurnedUp
FROM 
	#Vehicle Veh
LEFT JOIN
	#NumberOfMembershipAccounts Noma
ON	Noma.Month=veh.Month
LEFT JOIN
	#WashesCompletedCount Wcc
ON	Wcc.Month=Veh.Month

DROP TABLE  IF EXISTS #NumberOfMembershipAccounts
DROP TABLE  IF EXISTS #Vehicle
DROP TABLE  IF EXISTS #WashesCompletedCount

END