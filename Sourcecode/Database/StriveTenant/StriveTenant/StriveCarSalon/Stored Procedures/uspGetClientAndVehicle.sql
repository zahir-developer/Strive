CREATE PROCEDURE [StriveCarSalon].[uspGetClientAndVehicle]  
(@ClientId int)
AS 
BEGIN

SELECT 
  tblc.ClientId,
  tblc.FirstName+''+tblc.LastName as ClientName,
  tblc.Gender,
  tblc.MaritalStatus,
  tblc.BirthDate,
  tblc.CreatedDate,
  tblc.IsActive,
  tblc.Notes,
  tblc.RecNotes,
  tblc.Score,
  tblc.NoEmail,
  tblc.ClientType,
tblca.ClientAddressId,
tblca.ClientId as ClientRelatioshipId,
tblca.Address1,
tblca.Address2,
tblca.PhoneNumber,
tblca.PhoneNumber2,
tblca.Email,
tblca.City,
tblca.State,
tblca.Country,
tblca.Zip,
tblca.IsActive


FROM [tblClient] tblc 
inner join [tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId) 


WHERE ISNULL(tblc.IsDeleted,0)=0 AND ISNULL(tblc.IsActive,1)=1 and
(@ClientId is null or tblc.ClientId = @ClientId)  

select 
VehicleNumber,
VehicleMfr AS VehicleMakeId,
VehicleModel AS VehicleModelId,
VehicleColor AS ColorId,
Upcharge,
Barcode 

from  [tblClientVehicle] tclcv
--LEFT JOIN tblVehicleMake vm ON(tclcv.VehicleMfr = vm.MakeId)
--LEFT JOIN tblVehicleModel vmo ON(tclcv.VehicleModel = vmo.ModelId) and vm.MakeId = vmo.MakeId
INNER JOIN GetTable('VehicleColor') cvCo ON tclcv.VehicleColor = cvCo.valueid
 where (tclcv.IsDeleted = 0 OR tclcv.IsDeleted IS NULL) AND ISNULL(IsActive,1) = 1  AND tclcv.IsActive = 1
		 
END
