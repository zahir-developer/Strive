-- =============================================
-- Author:		Vineeth B
-- Create date: 26-08-2020
-- Description:	To get vehicle detail by clientid
-- =============================================

---------------------History--------------------
-- =============================================
-- 31-08-2020, Vineeth - Added monthly charge col
------------------------------------------------
-- =============================================
CREATE PROC [StriveCarSalon].[uspGetVehicleDetailByClientId] 
(@ClientId int)
AS
BEGIN

SELECT
    cl.ClientId,
	cvl.LocationId,
	cl.FirstName,
	cl.MiddleName,
	cl.LastName,
	cl.Gender,
	cl.BirthDate,
	cvl.VehicleId,
	cvl.VehicleNumber,
	cvMfr.valuedesc AS VehicleMfr,
	cvl.VehicleModelNo AS VehicleModelId,
	cvMo.valuedesc AS VehicleModel,
	cvCo.valuedesc AS VehicleColor,
	cvl.VehicleModelNo AS VehicleModelNo,
	cvl.VehicleYear,
	cvl.Upcharge,
	cvl.Barcode,
	cvl.Notes,
	cvl.IsActive,
	cvl.MonthlyCharge
FROM 
strivecarsalon.tblclient cl
INNER JOIN strivecarsalon.tblClientVehicle cvl ON cl.ClientId = cvl.ClientId
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1 AND ISNULL(cvl.IsActive,1)=1 AND
ISNULL(cvl.IsDeleted,0)=0 AND
cl.ClientId = @ClientId

END