CREATE PROCEDURE [StriveCarSalon].[uspGetVehicleDetailByClientId] 
(@ClientId int,@CurrentDate date = null)
AS

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
-- 08-Jun-2021, shalini - Added unk for make,mode
-- 08-Jun-2021, shalini - Added IsDiscount field
-- 31-Aug-2021, Zahir - Added IsDeleted condition for tblClientVehicleMembershipDetails

------------------------------------------------
-- =============================================
BEGIN

DECLARE @TodayDate date
DECLARE @Count int
DECLARE @IsMembership bit

set @TodayDate =(SELECT CAST(GETDATE() AS Date ) as CurrentDate)
set @Count =(select Count(1) from tblClientVehicleMembershipDetails 
                where --@TodayDate between StartDate and EndDate and
				ClientVehicleId in (select VehicleId from tblClientVehicle where ClientId=@ClientId and IsActive=1 and ISNULL(IsDeleted,0)=0)
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
	cl.Amount,
	cl.Gender,
	cl.BirthDate,
	cvl.VehicleId,
	cvl.VehicleNumber,
	--cvMfr.valuedesc AS VehicleMfr,
	--cvl.VehicleModel AS VehicleModelId,
	--cvMo.valuedesc AS VehicleModel,
	--cvl.VehicleMfr AS VehicleMakeId,
	cvl.VehicleColor as VehicleColorId,	
	cvCo.valuedesc AS VehicleColor,
	cvl.VehicleId,
	cvl.VehicleYear,
	cvl.Upcharge,
	cvl.Barcode,
	cvl.Notes,
	cvl.IsActive,
	cvl.MonthlyCharge,
	@IsMembership as IsMembership,
	cvmd.IsDiscount,
	tblm.MembershipName,	
	cvmd.DocumentId,
	make.MakeId as VehicleMakeId,
	IsNull(make.MakeValue,'Unk') as VehicleMfr,
	model.ModelId as VehicleModelId,
	IsNull(model.ModelValue,'Unk') as VehicleModel
FROM 
tblclient cl
INNER JOIN tblClientVehicle cvl ON cl.ClientId = cvl.ClientId
LEFT JOIN tblClientVehicleMembershipDetails cvmd ON cvl.VehicleId = cvmd.ClientVehicleId and cvmd.IsDeleted = 0
LEFT JOIN tblmembership tblm on cvmd.MembershipId = tblm.MembershipId and tblm.IsDeleted = 0
LEFT JOIN tblVehicleMake make on cvl.VehicleMfr = make.MakeId
LEFT JOIN tblvehicleModel model on cvl.VehicleModel= model.ModelId and make.MakeId = model.MakeId
--LEFT JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
--LEFT JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
LEFT JOIN GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid
WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1 AND ISNULL(cvl.IsActive,1)=1 AND
ISNULL(cvl.IsDeleted,0)=0 AND
cl.ClientId = @ClientId

END
