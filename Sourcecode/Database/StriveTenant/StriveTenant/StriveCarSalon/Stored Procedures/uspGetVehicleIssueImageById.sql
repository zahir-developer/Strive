
-- =============================================
-- Author:		Zahir Hussain
-- Create date: 2022-02-04
-- Description:	Returns Vehicle Issue image details
-- Sample:	EXEC uspGetVehicleIssueImageById 5
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetVehicleIssueImageById] 
(@VehicleIssueImageId int)
AS
BEGIN

  select 
  vm.VehicleIssueImageId,
  vm.ImageName,
  vm.OriginalImageName,
  vm.ThumbnailFileName,
  vm.CreatedDate
  from tblVehicleIssueImage vm
    
  where vm.VehicleIssueImageId = @VehicleIssueImageId  and isnull(vm.IsDeleted,0)=0

END