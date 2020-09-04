
CREATE PROC [StriveCarSalon].[uspGetVehicle] 
(@SearchName varchar(50) = null)
AS
BEGIN

SELECT
	cvl.VehicleId AS ClientVehicleId
	,cl.ClientId
	 ,cl.FirstName + ' '+cl.LastName as ClientName
	,cvl.VehicleNumber
	,cvl.VehicleMfr AS VehicleMakeId
	,cvMfr.valuedesc AS VehicleMake
	,cvl.VehicleModel AS VehicleModelId
	,cvmo.valuedesc AS ModelName
	,cvCo.valuedesc AS Color
	,cvl.VehicleColor AS ColorId
	,cvl.Upcharge
	,cvl.Barcode
FROM
strivecarsalon.tblclient cl
INNER JOIN strivecarsalon.tblClientVehicle cvl ON cl.ClientId = cvl.ClientId
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid

WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1 AND ISNULL(cvl.IsActive,1)=1 AND
ISNULL(cvl.IsDeleted,0)=0 AND
 ((@SearchName is null or cl.FirstName  like '%'+@SearchName+'%') OR
  (@SearchName is null or cl.LastName  like '%'+@SearchName+'%') OR
  (@SearchName is null or cvl.Barcode  like '%'+@SearchName+'%') OR
  (@SearchName is null or cvMfr.valuedesc  like '%'+@SearchName+'%') OR
  (@SearchName is null or cvmo.valuedesc  like '%'+@SearchName+'%') OR
  (@SearchName is null or cvCo.valuedesc  like '%'+@SearchName+'%') OR
  (@SearchName is null or cvl.VehicleNumber  like '%'+@SearchName+'%'))

END