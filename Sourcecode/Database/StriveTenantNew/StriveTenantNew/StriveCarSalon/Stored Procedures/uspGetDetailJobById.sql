
-- =============================================
-- Author:		Vineeth B
-- Create date: 31-08-2020
-- Description:	To get Detail against id
-- =============================================

CREATE PROC [StriveCarSalon].[uspGetDetailJobById] 
(@JobId int)
AS
BEGIN
Select 
tbj.JobId
,tbj.Barcode
,tbj.TicketNumber
,tbj.LocationId
,tbj.ClientId
,CONCAT(tblc.FirstName,' ',tblc.LastName) AS ClientName
,tblca.PhoneNumber
,tbj.VehicleId
,tbj.Make
,tbj.Model
,tbj.Color
,tbj.JobType
,tbj.JobDate
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbj.ActualTimeOut
,tbj.JobStatus
,tblji.ServiceId
,tbls.ServiceType as ServiceTypeId
,tbls.ServiceName
,tblji.Commission
,tblji.Price
,tblji.Quantity
from 
StriveCarSalon.tblJob tbj 
INNER JOIN StriveCarSalon.tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
LEFT JOIN StriveCarSalon.tblClientAddress tblca on tbj.ClientId = tblca.ClientId
LEFT JOIN StriveCarSalon.tblClient tblc on tbj.ClientId = tblc.ClientId
LEFT JOIN StriveCarSalon.tblJobItem tblji on tbj.JobId = tblji.JobId
LEFT JOIN StriveCarSalon.tblService tbls on tblji.ServiceId = tbls.ServiceId
LEFT JOIN StriveCarSalon.GetTable('ServiceType') tblcv on tbls.ServiceType = tblcv.valueid
WHERE tblcv.valuedesc='Details'
AND isnull(tbj.IsDeleted,0)=0
AND isnull(tblca.IsDeleted,0)=0
AND isnull(tblc.IsDeleted,0)=0
AND isnull(tblji.IsDeleted,0)=0
AND isnull(tbls.IsDeleted,0)=0
AND isnull(tblji.IsActive,1)=1
AND tbj.JobId = @JobId


Select 
JobItemId,
JobId,
tblji.ServiceId,
s.ServiceType as ServiceTypeId,
Commission,
Price,
Quantity,
ReviewNote
from StriveCarSalon.tblJobItem tblji
LEFT JOIN StriveCarSalon.tblService s on s.ServiceId = tblji.ServiceId
WHERE JobId = @JobId
AND isnull(tblji.IsDeleted,0)=0

END