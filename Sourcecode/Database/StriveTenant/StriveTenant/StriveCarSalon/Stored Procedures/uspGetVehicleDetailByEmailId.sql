-- ================================================  
-- Author:  Juki B  
-- Create date: 17-03-2022  
-- Description: Retrieve Vehicle Detail by EmailId  
-- ================================================  
  
---------------------History--------------------  
-- =============================================  
-- 2022-03-17, Juki - Added   
  
------------------------------------------------  
-- =============================================  
CREATE PROCEDURE [StriveCarSalon].[uspGetVehicleDetailByEmailId]   
(@EmailId varchar(50))  
AS  

BEGIN  
  
DECLARE @Count int  
DECLARE @IsMembership bit  
DECLARE @ClientId int = (select top 1 ClientId from tblClientAddress where email = @EmailId)  
  
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
 --cvl.LocationId,  
 cl.FirstName,  
 --cl.MiddleName,  
 cl.LastName,  
 cl.Amount,  
 --cl.Gender,  
 --cl.BirthDate,  
 cvl.VehicleId,  
 cvl.VehicleNumber,   
 cvl.VehicleColor as VehicleColorId,   
 cvCo.valuedesc AS VehicleColor,  
 cvl.VehicleId,  
 cvl.VehicleYear,  
 cvl.Upcharge,  
 cvl.Barcode,  
 --cvl.Notes,  
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
AND IsNull(cvl.Barcode, '') = ''  
--cvl.Barcode IS NULL   
END  