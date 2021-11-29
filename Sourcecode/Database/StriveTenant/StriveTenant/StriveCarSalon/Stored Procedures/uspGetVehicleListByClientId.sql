
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
,CONCAT(tblclv.BarCode,' ',cvMfr.MakeValue,' ',cvMo.ModelValue,' ',cvCo.valuedesc) AS VehicleDetails

from [tblClientVehicle] tblclv
INNER JOIN [StriveCarSalon].[tblClient] tblc ON tblc.ClientId = tblclv.ClientId 
LEFT JOIN tblVehicleMake cvMfr ON tblclv.VehicleMfr = cvMfr.MakeId
LEFT JOIN tblVehicleModel cvMo ON tblclv.VehicleModel = cvMo.ModelId and cvMfr.MakeId = cvMo.MakeId
INNER JOIN GetTable('VehicleColor') cvCo ON tblclv.VehicleColor = cvCo.valueid
WHERE
tblc.ClientId=@ClientId
AND isnull(tblclv.IsDeleted,0)=0
AND isnull(tblc.IsDeleted,0)=0
END
