-- =============================================
-- Author:		Zahir Hussain M
-- Create date: 09-02-2022
-- Description:	Returns list of Detail Jobs assigned to given employee and jobDate
-- =============================================
-- EXEC uspGetEmployeeAssignedDetail 1138, '2022-02-14'
-------------------------History------------------------
-- =====================================================

-- =====================================================


CREATE PROCEDURE [StriveCarSalon].[uspGetEmployeeAssignedDetail] (@EmployeeId int, @JobDate DateTime)
AS
BEGIN

DECLARE @JobType INT  = (Select top 1 valueid from GetTable('JobType') where valuedesc = 'Detail')
DECLARE @AddService INT  = (Select top 1 valueid from GetTable('ServiceType') where valuedesc = 'Additional Services')
DECLARE @DetailService INT  = (Select top 1 valueid from GetTable('ServiceType') where valuedesc = 'Detail Package')

--Select * from GetTable('ServiceType')

DROP TABLE IF EXISTS #Jobs

Select DISTINCT ji.JobId, ji.JobItemId into #Jobs 
from tblJobServiceEmployee se
JOIN tblJobItem ji on se.JobItemId = ji.JobItemId
JOIN tblJob j on j.JobId = ji.JobId
WHERE  j.JobDate = @JobDate and j.JobType = @JobType and j.IsDeleted = 0 and ji.IsDeleted = 0 
and se.EmployeeId = @EmployeeId 
and se.IsDeleted = 0

DROP TABLE IF EXISTS #JobDetail

SELECT DISTINCT
tbj.JobId
,tbj.Barcode
,tbj.TicketNumber
,tbj.LocationId
,tbj.VehicleId
,ISNULL(tblvm.MakeValue, 'Unk') as VehicleMake
,ISNULL(tblv.ModelValue, 'Unk') as VehicleModel
,ISNULL(tblvc.CodeValue, 'Unk') as VehicleColor
,tbj.JobDate
,tbj.JobStatus
,tbj.TimeIn
,tbj.EstimatedTimeOut into #JobDetail
from #Jobs j
INNER JOIN tblJob tbj on j.JobId = tbj.JobId 
LEFT JOIN tblVehicleMake tblvm on tblvm.MakeId = tbj.Make
LEFT JOIN tblVehicleModel tblv on tblv.modelId = tbj.Model and tblvm.MakeId = tblv.MakeId
LEFT JOIN tblCodeValue tblvc on tblvc.id = tbj.Color

DROP TABLE IF EXISTS #JobItems

Select 
tblji.JobItemId,
tblji.JobId,
tblji.ServiceId,
s.ServiceType as ServiceTypeId,
s.ServiceName,
tblcv.CodeValue as ServiceType,
tblji.Price into #JobItems
from #Jobs j
JOIN tblJobItem tblji with(nolock) on j.JobItemId = tblji.JobItemId
INNER JOIN tblService s ON s.ServiceId = tblji.ServiceId
LEFT JOIN StriveCarSalon.tblcodevalue tblcv on s.ServiceType = tblcv.id
--LEFT JOIN tblJobServiceEmployee tblJSE ON tblji.JobItemId= tblJSE.JobItemId

DROP TABLE IF EXISTS #JobPrice

Select SUM(Price) as TotalPrice, JobId into #JobPrice from #JobItems 
GROUP BY JobId

DROP TABLE IF EXISTS #AdditionalJobItems

DROP TABLE IF EXISTS #DetailJobItems

Select JobId, JobItemId, ServiceName into #AdditionalJobItems from #JobItems where ServiceTypeId = @AddService

Select JobId, JobItemId, ServiceName into #DetailJobItems from #JobItems where ServiceTypeId = @DetailService

DROP TABLE IF EXISTS #AdditionalService

Select ISNULL(STUFF((SELECT Distinct ', ' + TRIM(ji.ServiceName) 
    FROM #AdditionalJobItems ji WHERE ji.JobId = ji2.JobId
    FOR XML PATH('')
	), 1, 2, ''),'None') AdditionalService, JobId into #AdditionalService from #JobItems ji2
	Group by JobId

	--Select * from #AdditionalService

	DROP TABLE IF EXISTS #DetailService
	
	Select STUFF((SELECT Distinct ', ' + TRIM(ji.ServiceName) 
    FROM #DetailJobItems ji WHERE ji.JobId = ji2.JobId 
    FOR XML PATH('')
	), 1, 2, '') DetailService, JobId into #DetailService from #JobItems ji2 
	Group by JobId

	--Select * from #DetailService

	Select DISTINCT jd.*, s.AdditionalService, d.DetailService, jp.TotalPrice
	from #JobDetail jd
	JOIN #AdditionalService s on s.jobId = jd.jobId
	JOIN #DetailService d on d.JobId = jd.JobId
	JOIN #JobPrice jp on jd.JobId = jp.JobId 
END