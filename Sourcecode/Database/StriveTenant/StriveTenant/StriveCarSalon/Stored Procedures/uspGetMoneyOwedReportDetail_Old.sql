
-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 2021-05-03
-- Description:	Retrives Money owned report details.
-- Sample: StriveCarSalon.uspGetMoneyOwedReportDetail 1, '2021-05'
-- =============================================
CREATE PROCEDURE [StriveCarSalon].[uspGetMoneyOwedReportDetail_Old]
@LocationId INT,
@Date VARCHAR(10)
AS
BEGIN

SET NOCOUNT ON;

--DECLARE @LocationId INT = 1

DECLARE @ColumnToPivot NVARCHAR(10) =  'LocationId'

--DECLARE @Date VARCHAR(10) = '2021-05'

DECLARE @JobStatusCompleted INT =(SELECT valueid FROM GetTable('JobStatus') WHERE valuedesc='Completed')

DECLARE @JobTypeWash INT =(SELECT valueid FROM GetTable('JobType') WHERE valuedesc='Wash')

DECLARE @PaymentStatusSuccess INT =(SELECT valueid FROM GetTable('PaymentStatus') WHERE valuedesc='Success')

DECLARE @WashServiceType INT =(SELECT valueid FROM GetTable('ServiceType') WHERE valuedesc='Wash Package')

DECLARE @PaymentTypeMembership INT =(SELECT valueid FROM GetTable('PaymentType') WHERE valuedesc='Membership')

DROP TABLE IF EXISTS #MembershipClients

Select c.ClientId, c.FirstName, c.LastName, cv.VehicleId, cm.ClientMembershipId INTO #MembershipClients 
from tblClient (nolock) c 
JOIN tblClientVehicle (nolock) cv on cv.ClientId = c.ClientId
JOIN tblClientVehicleMembershipDetails (nolock) cm on cm.ClientVehicleId = cv.VehicleId
WHERE cm.IsDeleted = 0 and cm.LocationId = @locationId --and c.ClientId in  (52100,47537)


DROP TABLE IF EXISTS #ClientJobCountByLocation

Select Count(j.jobId) JobCount, loc.LocationId, loc.LocationName, mc.ClientId into #ClientJobCountByLocation from tblJob j
JOIN tblJobPayment (nolock) jp on jp.JobPaymentId = j.JobPaymentId
JOIN #MembershipClients  mc on mc.ClientId = j.ClientId
JOIN tblLocation (nolock) loc on j.LocationId = loc.LocationId
WHERE 
jp.PaymentStatus = @PaymentStatusSuccess and 
j.JobType = @JobTypeWash and 
SUBSTRING(CAST(JobDate AS VARCHAR(10)),1,7) = @Date
Group by loc.LocationId, loc.LocationName, mc.ClientId

--SELECT * FROM (
--SELECT
--JobCount, LocationId, ClientId
--FROM #ClientJobCountByLocation
--) ClientSummary
--PIVOT (
--SUM(JobCount)
--FOR [LocationId]
--IN ([1],[2],[3],[4],[5],[6])
--) AS PivotTable

/*
DROP TABLE IF EXISTS #FinalClientJobCountByLocation

CREATE TABLE(

)

INSERT INTO #FinalClientJobCountByLocation
*/

DECLARE @LocIds NVARCHAR(100) = (Select STUFF((SELECT Distinct ', ' + CONVERT(VARCHAR(2),locationId ) 
    FROM #ClientJobCountByLocation
    FOR XML PATH('')
	), 1, 2, ''))

SET @LocIds = '[' + REPLACE(@LocIds,', ', '],[') + ']' 

EXEC DynamicPivotTableInSql N'LocationId', @LocIds 

Select ClientId, FirstName, LastName, s.ServiceId, s.Price as ServicePrice, md.TotalPrice as MembershipPrice, ms.ClientVehicleMembershipServiceId, s.ServiceType, cv.CodeValue ServiceTypeName
from #MembershipClients (nolock) mc
JOIN tblClientVehicleMembershipDetails (nolock) md on md.ClientMembershipId = mc.ClientMembershipId
JOIN tblClientVehicleMembershipService (nolock) ms on ms.ClientMembershipId = mc.ClientMembershipId
JOIN tblService (nolock) s on s.ServiceId = ms.ServiceId
JOIN tblCodeValue (nolock) cv on cv.id = s.ServiceType
WHERE s.ServiceType = @WashServiceType and ms.IsDeleted = 0


END