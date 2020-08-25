

CREATE PROC [StriveCarSalon].[uspGetAllJob]
AS
BEGIN

Select 
tbj.JobId
,tbj.TicketNumber
,CONCAT(tblc.FirstName,' ',tblc.LastName) AS ClientName
,tblca.PhoneNumber
,CONCAT(cvMo.valuedesc,' , ',cvMfr.valuedesc,' , ',cvCo.valuedesc) AS VehicleName
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbls.ServiceName

from 
StriveCarSalon.tblJob tbj 
inner join StriveCarSalon.GetTable('ServiceType') tblcv on tbj.JobType = tblcv.valueid
inner join StriveCarSalon.tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON tblclv.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON tblclv.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON tblclv.VehicleColor = cvCo.valueid
inner join StriveCarSalon.tblClientAddress tblca on tbj.ClientId = tblca.ClientId
inner join StriveCarSalon.tblClient tblc on tbj.ClientId = tblc.ClientId
inner join StriveCarSalon.tblJobItem tblji on tbj.JobId = tblji.JobId
inner join StriveCarSalon.tblService tbls on tblji.ServiceId = tbls.ServiceId
WHERE
tblcv.valuedesc='Washes'
AND isnull(tblca.IsDeleted,0)=0
AND isnull(tblc.IsDeleted,0)=0
AND isnull(tblji.IsDeleted,0)=0
AND isnull(tbls.IsDeleted,0)=0
END