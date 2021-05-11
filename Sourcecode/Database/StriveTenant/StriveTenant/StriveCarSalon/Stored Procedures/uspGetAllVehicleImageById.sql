--[StriveCarSalon].[uspGetAllVehicleImageById] 49233
CREATE PROCEDURE [StriveCarSalon].[uspGetAllVehicleImageById] 
(@VehicleId int)
AS
BEGIN

  select 
  vm.VehicleImageId,
  vm.ImageName,
  vm.OriginalImageName,
  vm.ThumbnailFileName,
  vm.CreatedDate
   from tblClientVehicle tblc
    inner join tblVehicleImage vm on tblc.VehicleId = vm.VehicleId
   where tblc.VehicleId =@VehicleId  and isnull(tblc.IsDeleted,0)=0

END