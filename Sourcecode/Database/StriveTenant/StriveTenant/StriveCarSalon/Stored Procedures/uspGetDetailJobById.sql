-- =============================================
-- Author:		Vineeth B
-- Create date: 31-08-2020
-- Description:	To get Detail against id
-- =============================================

---------------------History--------------------
-- =====================================================
-- 02-09-2020, Vineeth - Modified ServiceType to JobType
-- 04-09-2020, Vineeth - Added BayId    
-- 07-09-2020, Vineeth - Added barcode from clientvehicle
--						table and bayid from tbljobdetail  
-- 08-09-2020, Vineeth - Added Notes col        
-- 09-09-2020, Vineeth - Added Employee Details and 
--						 ServiceName
-- 10-09-2020, Vineeth - Added with(nolock) and used LJ
--					     Added IsActive and IsDetete 
-- 22-09-2020, Vineeth - Added tblJobServiceEmployee
-- 23-09-2020, Vineeth - Added cost property in JobItem
-- 28-09-2020, Zahir - Added JobStatus property
-- 28-09-2021, Zahir - Changed Client/ClientAddress INNER to LEFT JOIN
-- 30-09-2021, Zahir - Detail assigned Employee email ids taken.
-- 12-10-2021, Zahir - Added, JobStatus & JobType
-- 26-10-2021, Zahir - Ln: 112: Remove JobServiceEmployee from JobItem result set.
-- 20-01-2021, Zahir - Added serviceType in JobItem result set.

--------------------------------------------------------
-- EXEC [StriveCarSalon].[uspGetDetailJobById] 593280
-- =====================================================


CREATE PROCEDURE [StriveCarSalon].[uspGetDetailJobById] 
(@JobId int)
AS
BEGIN

DECLARE @EmployeeCount INT;

Select TOP 1
tbj.JobId
,tbljd.BayId
,tbj.Barcode
,tbj.JobId 
,tbj.TicketNumber
,tbj.LocationId
,tbj.ClientId
,CONCAT(ISNULL(tblc.FirstName, 'DRIVE'),' ',ISNULL(tblc.LastName, 'UP')) AS ClientName
,tbj.VehicleId
,tbj.Make
,tbj.Color
,tbj.Model
,ISNULL(tblvm.MakeValue, 'Unk') as VehicleMake
,ISNULL(tblv.ModelValue, 'Unk') as VehicleModel
,ISNULL(tblvc.valuedesc, 'Unk') as VehicleColor
,tbj.JobType
,tbj.JobDate
,tbj.JobStatus
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbj.Notes
,tblca.PhoneNumber
,tblca.Email
,ISNULL(tbj.JobPaymentId,0)
,ISNULL(ps.valuedesc,'NotPaid') AS Paymentstatus
,Case
when (ps.valuedesc != 'Success'OR tbljp.PaymentStatus IS NULL) then 'False'
when ps.valuedesc = 'Success' then 'True'
End AS IsPaid
from tblJob tbj with(nolock)
LEFT JOIN tblJobPayment tbljp  WITH(NOLOCK) ON tbj.JobPaymentId = tbljp.JobPaymentId AND tbljp.IsProcessed=1 AND ISNULL(tbljp.IsRollBack,0)=0 AND tbljp.IsActive = 1 AND ISNULL(tbljp.IsDeleted,0)=0 
LEFT JOIN GetTable('PaymentStatus') ps ON(tbljp.PaymentStatus = ps.valueid)
LEFT JOIN tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
INNER JOIN tblJobDetail tbljd on tbj.JobId = tbljd.JobId
LEFT JOIN tblClient tblc on tbj.ClientId = tblc.ClientId
LEFT JOIN tblClientAddress tblca on tblc.ClientId = tblca.ClientId
INNER JOIN tblJobItem tblji on tbj.JobId = tblji.JobId
INNER JOIN GetTable('JobType') tbljt on tbljt.valueid = tbj.JobType
LEFT JOIN tblVehicleMake tblvm on tblvm.MakeId = tbj.Make
LEFT JOIN tblVehicleModel tblv on tblv.modelId = tbj.Model and tblvm.MakeId = tblv.MakeId
LEFT JOIN GetTable('VehicleColor') tblvc on tblvc.valueid = tbj.Color
WHERE tbljt.valuedesc='Detail'
AND isnull(tbj.IsDeleted,0)=0 
AND isnull(tblji.IsDeleted,0)=0
AND isnull(tbljd.IsDeleted,0)=0
AND tbj.IsActive=1
AND tblji.IsActive=1
AND tbljd.IsActive=1
AND tbj.JobId = @JobId

Select 
tblji.JobItemId,
tblji.JobId,
tblji.ServiceId,
s.IsCeramic,
s.ServiceType as ServiceTypeId,
ISNULL(ct.valuedesc,'') CommissionType,
ISNULL(s.CommissionCost,0.00) CommissionCost,
s.ServiceName,
tblcv.CodeValue as ServiceType,
--s.Cost,
tblji.Price,
tblji.Price as Cost
from tblJobItem tblji with(nolock)
INNER JOIN tblService s ON s.ServiceId = tblji.ServiceId
LEFT JOIN GetTable('CommisionType') ct on ct.valueid = s.CommisionType
LEFT JOIN StriveCarSalon.tblcodevalue tblcv on s.ServiceType = tblcv.id
--LEFT JOIN tblJobServiceEmployee tblJSE ON tblji.JobItemId= tblJSE.JobItemId
WHERE tblji.JobId = @JobId
AND isnull(tblji.IsDeleted,0)=0
AND tblji.IsActive=1

DROP TABLE IF EXISTS #JobServiceEmployee

Select
tbljse.JobServiceEmployeeId,
tbljse.JobItemId,
tbljse.ServiceId,
tbls.ServiceName,
--tbls.Cost,
tblji.price as Cost,
tbljse.EmployeeId,
ISNULL(tbljse.CommissionAmount,'0.00')CommissionAmount,
CONCAT(tble.FirstName,' ',tble.LastName) AS EmployeeName
INTO #JobServiceEmployee
from tblJobServiceEmployee tbljse with(nolock) 
INNER JOIN tblJobItem tblji ON tbljse.JobItemId = tblji.JobItemId
INNER JOIN tblService tbls ON(tbljse.ServiceId = tbls.ServiceId)
INNER JOIN tblEmployee tble ON(tbljse.EmployeeId = tble.EmployeeId)

WHERE tblji.JobId =@JobId
AND isnull(tblji.IsDeleted,0)=0
AND tblji.IsActive=1
AND isnull(tbljse.IsDeleted,0)=0
AND tbljse.IsActive=1

Select * from #JobServiceEmployee

Select @EmployeeCount = COUNT(distinct employeeId) from #JobServiceEmployee


Select TOP 1 STUFF((SELECT Distinct ', ' + ea.Email  
    FROM #JobServiceEmployee e
JOIN tblEmployeeAddress ea on ea.EmployeeId = e.EmployeeId
where ea.Email is not null
    FOR XML PATH('')
	), 1, 2, '') Email
END
GO

