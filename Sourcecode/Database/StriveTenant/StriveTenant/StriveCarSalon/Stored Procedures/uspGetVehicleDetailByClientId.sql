CREATE PROC [StriveCarSalon].[uspGetVehicleDetailByClientId]  -- 57,'2020-09-10'
(@ClientId int,@CurrentDate date = null)

AS
BEGIN

DECLARE @TodayDate date
DECLARE @Count int
DECLARE @IsMembership bit

set @TodayDate =(SELECT CAST( GETDATE() AS Date ) as CurrentDate)

set @Count =(select Count(1) from strivecarsalon.tblClientVehicleMembershipDetails 
                where @TodayDate between StartDate and EndDate ) --OR (@StartDate IS NULL AND @EndDate IS NULL))
IF(@Count = 0)
Begin
 SET @IsMembership = 'false';
End
ELSE
Begin
 SET @IsMembership = 'true';
End

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
	cvl.MonthlyCharge,
	@IsMembership as IsMembership
FROM 
strivecarsalon.tblclient cl
INNER JOIN strivecarsalon.tblClientVehicle cvl ON cl.ClientId = cvl.ClientId
INNER JOIN strivecarsalon.tblClientVehicleMembershipDetails cvmd ON cvl.VehicleId = cvmd.ClientVehicleId
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1 AND ISNULL(cvl.IsActive,1)=1 AND
ISNULL(cvl.IsDeleted,0)=0 AND
cl.ClientId = @ClientId

END
