




-- ================================================
-- Author:		Benny Johnson
-- Create date: 01-08-2020
-- Description:	Retrieve Vehicle Detail by ClientId
-- ================================================

---------------------History--------------------
-- =============================================
-- 2020-09-30, Vineeth - Added condition where 
--                      Membership exist or not 
--                      for any vehicle of 
--                      respective client,
--						Added distinct conditon

------------------------------------------------
-- =============================================
CREATE PROC [StriveCarSalon].[uspGetVehicleDetailByClientId] 
(@ClientId int,@CurrentDate date = null)

AS
BEGIN

DECLARE @TodayDate date
DECLARE @Count int
DECLARE @IsMembership bit

set @TodayDate =(SELECT CAST( GETDATE() AS Date ) as CurrentDate)
set @Count =(select Count(1) from strivecarsalon.tblClientVehicleMembershipDetails 
                where @TodayDate between StartDate and EndDate and ClientVehicleId 
				in(select VehicleId from tblClientVehicle where ClientId=@ClientId and IsActive=1 and ISNULL(IsDeleted,0)=0)
				and IsActive=1 and ISNULL(IsDeleted,0)=0) 
				
IF(@Count > 0)
 SET @IsMembership = 1;
ELSE
 SET @IsMembership = 0;

SELECT
    DISTINCT
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
LEFT JOIN strivecarsalon.tblClientVehicleMembershipDetails cvmd ON cvl.VehicleId = cvmd.ClientVehicleId
LEFT JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
LEFT JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
LEFT JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1 AND ISNULL(cvl.IsActive,1)=1 AND
ISNULL(cvl.IsDeleted,0)=0 AND
cl.ClientId = @ClientId

END
