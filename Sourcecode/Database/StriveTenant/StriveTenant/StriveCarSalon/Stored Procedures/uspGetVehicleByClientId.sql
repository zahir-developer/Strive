CREATE PROC [StriveCarSalon].[uspGetVehicleByClientId] 
(@ClientId int =null)
AS
BEGIN

SELECT
	cvl.VehicleId AS ClientVehicleId
	,cvl.VehicleNumber
	--,cvl.VehicleMfr AS VehicleMakeId
	--,cvMfr.valuedesc AS VehicleMake
	--,cvl.VehicleModel AS VehicleModelId
	--,cvmo.valuedesc AS VehicleModelName
	,cvCo.valuedesc AS VehicleColor
	,cvl.VehicleColor AS VehicleColorId
	,cvl.Upcharge
	,cvl.Barcode
	,make.MakeId as VehicleMakeId
	,make.MakeValue as VehicleMake
	,model.ModelId as VehicleModelId
	,model.ModelValue as VehicleModelName
FROM
strivecarsalon.tblClientVehicle cvl
Inner join tblVehicleMake make on cvl.VehicleMfr=make.MakeId
inner join tblvehicleModel model on cvl.VehicleModel= model.ModelId
--INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
--INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid

WHERE ISNULL(cvl.IsDeleted,0)=0 AND ISNULL(cvl.IsActive,1)=1 AND
(@ClientId is null or cvl.ClientId = @ClientId)

END
