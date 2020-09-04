
CREATE PROCEDURE [CON].[uspGetClientAndVehicle]  
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


FROM [CON].[tblClient] tblc 
inner join [CON].[tblClientAddress] tblca ON(tblc.ClientId = tblca.ClientId) 


WHERE ISNULL(tblc.IsDeleted,0)=0 AND ISNULL(tblc.IsActive,1)=1 and
(@ClientId is null or tblc.ClientId = @ClientId)  

select 
VehicleNumber,
VehicleMfr AS VehicleMakeId,
VehicleModel AS VehicleModelId,
VehicleColor AS ColorId,
Upcharge,
Barcode 

from  [CON].[tblClientVehicle] tclcv
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON tclcv.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON tclcv.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON tclcv.VehicleColor = cvCo.valueid
 where (tclcv.IsDeleted = 0 OR tclcv.IsDeleted IS NULL) AND ISNULL(IsActive,1) = 1  AND tclcv.IsActive = 1
		 
END