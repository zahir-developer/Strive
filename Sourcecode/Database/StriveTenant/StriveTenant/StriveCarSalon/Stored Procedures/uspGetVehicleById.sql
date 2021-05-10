CREATE PROC [StriveCarSalon].[uspGetVehicleById] 
(@VehicleId int)
AS
BEGIN

SELECT
	cvl.VehicleId AS ClientVehicleId
	,cvl.ClientId
	,cvl.VehicleNumber
	--,cvl.VehicleMfr AS VehicleMakeId
	--,cvMfr.valuedesc AS VehicleMake
	--,cvl.VehicleModel AS VehicleModelId
	--,cvmo.valuedesc AS ModelName
	,cvCo.valuedesc AS Color
	,cvl.VehicleColor AS ColorId
	,cvl.Upcharge
	,cvl.Barcode
	,cvl.MonthlyCharge	
	,make.MakeId as VehicleMakeId
	,make.MakeValue as VehicleMake
	,model.ModelId as VehicleModelId
	,model.ModelValue as ModelName
FROM strivecarsalon.tblClientVehicle cvl 

--INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
--INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
LEFT join tblVehicleMake make on cvl.VehicleMfr=make.MakeId
LEFT join tblvehicleModel model on cvl.VehicleModel= model.ModelId and make.MakeId = model.MakeId
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
WHERE cvl.VehicleId in (@VehicleId) AND ISNULL(cvl.IsDeleted,0)=0

END
