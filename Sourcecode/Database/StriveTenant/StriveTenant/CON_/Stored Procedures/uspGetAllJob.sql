

-- ====================================================
-- Author:		Vineeth B
-- Create date: 15-08-2020
-- Description:	Retrieves Job and Job item details
-- ====================================================

---------------------History---------------------------
-- ====================================================
-- 25-08-2020, Vineeth - changed jobtype to servicetype
-- 26-08-2020, Zahir Hussain -- 1. Change Join from INNER to LEFT, 2.Vehicle details taken from Job table instead of Client Vehicle table. 


-------------------------------------------------------
-- ====================================================

CREATE PROCEDURE [CON].[uspGetAllJob]
AS
BEGIN

Select 
distinct
tbj.JobId
,tbj.TicketNumber
,CONCAT(tblc.FirstName,' ',tblc.LastName) AS ClientName
,tblca.PhoneNumber
,CONCAT(cvMo.valuedesc,' , ',cvMfr.valuedesc,' , ',cvCo.valuedesc) AS VehicleName
,tbj.TimeIn
,tbj.EstimatedTimeOut
,tbls.ServiceName
,tbls.ServiceType,
tbj.IsDeleted
from 
StriveCarSalon.tblJob tbj 
INNER join StriveCarSalon.tblJobItem tblji on tbj.JobId = tblji.JobId
LEFT join StriveCarSalon.tblClient tblc on tbj.ClientId = tblc.ClientId
--LEFT join StriveCarSalon.tblClientVehicle tblclv on tbj.VehicleId = tblclv.VehicleId
LEFT join StriveCarSalon.tblClientAddress tblca on tbj.ClientId = tblca.ClientId
LEFT join StriveCarSalon.tblService tbls on tblji.ServiceId = tbls.ServiceId
LEFT join StriveCarSalon.GetTable('ServiceType') tblcv on tbls.ServiceType = tblcv.valueid and tblcv.valuedesc='Washes'
LEFT JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON tbj.Make = cvMfr.valueid
LEFT JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON tbj.Model = cvMo.valueid
LEFT JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON tbj.Color = cvCo.valueid
WHERE
tblcv.valuedesc='Washes'
AND isnull(tbj.IsDeleted,0)=0
AND isnull(tblca.IsDeleted,0)=0
AND isnull(tblc.IsDeleted,0)=0
AND isnull(tblji.IsDeleted,0)=0
AND isnull(tbls.IsDeleted,0)=0
ORDER BY tbj.JOBID
END
