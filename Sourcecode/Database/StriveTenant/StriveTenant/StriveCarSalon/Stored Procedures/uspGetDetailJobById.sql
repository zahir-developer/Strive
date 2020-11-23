






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
--------------------------------------------------------
-- =====================================================

CREATE   PROC [StriveCarSalon].[uspGetDetailJobById] --375
(@JobId int)
AS
BEGIN
Select TOP 1
tbj.JobId
,tbljd.BayId
,tblclv.Barcode
,tbj.TicketNumber
,tbj.LocationId
,tbj.ClientId
,CONCAT(tblc.FirstName,' ',tblc.LastName) AS ClientName
,tbj.VehicleId
,tbj.Make
,tbj.Model
,tbj.Color
,tbj.JobType
,tbj.JobDate
,tbj.JobStatus
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbj.Notes
from 
StriveCarSalon.tblJob tbj with(nolock)
LEFT JOIN StriveCarSalon.tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
INNER JOIN StriveCarSalon.tblJobDetail tbljd on tbj.JobId = tbljd.JobId
INNER JOIN StriveCarSalon.tblClient tblc on tbj.ClientId = tblc.ClientId
INNER JOIN StriveCarSalon.tblJobItem tblji on tbj.JobId = tblji.JobId
INNER JOIN StriveCarSalon.GetTable('JobType') tbljt on tbljt.valueid = tbj.JobType
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
s.ServiceType as ServiceTypeId,
ISNULL(ct.valuedesc,'') CommissionType,
ISNULL(s.CommissionCost,0.00) CommissionCost,
s.ServiceName,
s.Cost
from StriveCarSalon.tblJobItem tblji with(nolock)
INNER JOIN StriveCarSalon.tblService s ON s.ServiceId = tblji.ServiceId
LEFT JOIN StriveCarSalon.GetTable('CommisionType') ct on ct.valueid = s.CommisionType
LEFT JOIN StriveCarSalon.tblJobServiceEmployee tblJSE ON tblji.JobItemId= tblJSE.JobItemId
WHERE tblji.JobId = @JobId
AND isnull(tblji.IsDeleted,0)=0
AND tblji.IsActive=1

Select
tbljse.JobServiceEmployeeId,
tbljse.JobItemId,
tbljse.ServiceId,
tbls.ServiceName,
tbls.Cost,
tbljse.EmployeeId,
CONCAT(tble.FirstName,' ',tble.LastName) AS EmployeeName
from StriveCarSalon.tblJobServiceEmployee tbljse with(nolock) 
INNER JOIN StriveCarSalon.tblJobItem tblji ON tbljse.JobItemId = tblji.JobItemId
INNER JOIN StriveCarSalon.tblService tbls ON(tbljse.ServiceId = tbls.ServiceId)
INNER JOIN StriveCarSalon.tblEmployee tble ON(tbljse.EmployeeId = tble.EmployeeId)
WHERE tblji.JobId =@JobId
AND isnull(tblji.IsDeleted,0)=0
AND tblji.IsActive=1
AND isnull(tbljse.IsDeleted,0)=0
AND tbljse.IsActive=1
END
GO

