--[StriveCarSalon].[uspGetVehicleImageById] 15
CREATE PROCEDURE [StriveCarSalon].[uspGetVehicleImageById] 
(@VehicleImageId int)
AS
BEGIN

  select 
  vm.VehicleImageId,
  vm.ImageName,
  vm.OriginalImageName,
  vm.ThumbnailFileName,
  vm.CreatedDate
   from tblVehicleImage vm
    
   where vm.VehicleImageId =@VehicleImageId  and isnull(vm.IsDeleted,0)=0

END