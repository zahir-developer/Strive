
-- =========================================================
-- Author:		Vineeth B
-- Create date: 24-08-2020
-- Description:	To get VehicleDetail for respective ClientId
-- =========================================================
CREATE procedure [StriveCarSalon].[uspGetVehicleListByClientId] 
(@ClientId int)
AS
BEGIN
Select 
distinct 
tblclv.VehicleId
,CONCAT(tblclv.BarCode,' ',cvMfr.valuedesc,' ',cvMo.valuedesc,' ',cvCo.valuedesc) AS VehicleDetails

from [StriveCarSalon].[tblClientVehicle] tblclv
INNER JOIN [StriveCarSalon].[tblClient] tblc ON tblc.ClientId = tblclv.ClientId 
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON tblclv.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON tblclv.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON tblclv.VehicleColor = cvCo.valueid
WHERE
tblc.ClientId=@ClientId
AND isnull(tblclv.IsDeleted,0)=0
AND isnull(tblc.IsDeleted,0)=0
END
