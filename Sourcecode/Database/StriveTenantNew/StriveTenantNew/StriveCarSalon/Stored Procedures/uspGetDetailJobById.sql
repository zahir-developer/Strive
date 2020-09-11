








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
--------------------------------------------------------
-- =====================================================
CREATE PROC [StriveCarSalon].[uspGetDetailJobById] 
(@JobId int)
AS
BEGIN
Select 
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
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tblji.ServiceId
,tbls.ServiceName
,tbj.Notes
from 
StriveCarSalon.tblJob tbj 
INNER JOIN StriveCarSalon.tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
INNER JOIN StriveCarSalon.tblJobDetail tbljd on tbj.JobId = tbljd.JobId
INNER JOIN StriveCarSalon.tblClient tblc on tbj.ClientId = tblc.ClientId
INNER JOIN StriveCarSalon.tblJobItem tblji on tbj.JobId = tblji.JobId
INNER JOIN StriveCarSalon.tblService tbls on tblji.ServiceId = tbls.ServiceId
INNER JOIN StriveCarSalon.GetTable('JobType') tbljt on tbljt.valueid = tbj.JobType
WHERE tbljt.valuedesc='Detail'
AND isnull(tbj.IsDeleted,0)=0 
AND isnull(tblc.IsDeleted,0)=0
AND isnull(tblji.IsDeleted,0)=0
AND isnull(tbls.IsDeleted,0)=0
AND isnull(tblji.IsActive,1)=1
AND tbj.JobId = @JobId

Select 
tblji.JobItemId,
tblji.JobId,
tblji.ServiceId,
s.ServiceType as ServiceTypeId,
s.ServiceName,
tblji.EmployeeId,
CONCAT(tble.FirstName,' ',tble.LastName) AS EmployeeName
from StriveCarSalon.tblJobItem tblji
INNER JOIN StriveCarSalon.tblService s on s.ServiceId = tblji.ServiceId
INNER JOIN StriveCarSalon.tblEmployee tble on tble.EmployeeId = tblji.EmployeeId
WHERE JobId = @JobId
AND isnull(tblji.IsDeleted,0)=0

END