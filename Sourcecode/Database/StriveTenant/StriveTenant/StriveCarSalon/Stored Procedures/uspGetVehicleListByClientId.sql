
-- =========================================================
-- Author:		Vineeth B
-- Create date: 24-08-2020
-- Description:	To get VehicleDetail for respective ClientId
-- =========================================================
create procedure [StriveCarSalon].[uspGetVehicleListByClientId] 
(@ClientId int)
AS
BEGIN
Select 
distinct 
tblel.VehicleId
,CONCAT(tblclv.BarCode,' ',cvMfr.valuedesc,' ',cvMo.valuedesc,' ',cvCo.valuedesc) AS VehicleDetails

from 
[StriveCarSalon].[tblEmployeeLiability] tblel
INNER JOIN [StriveCarSalon].[tblClientVehicle] tblclv ON tblel.VehicleId = tblclv.VehicleId 
INNER JOIN [StriveCarSalon].[tblClient] tblc ON tblc.ClientId = tblel.ClientId 
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON tblclv.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON tblclv.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON tblclv.VehicleColor = cvCo.valueid
WHERE
tblel.ClientId=@ClientId
AND isnull(tblclv.IsDeleted,0)=0
AND isnull(tblel.IsDeleted,0)=0
AND isnull(tblc.IsDeleted,0)=0
END
