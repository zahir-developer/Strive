-- =============================================
-- Author:		Zahir Hussain
-- Create date: 2022-02-04
-- Description:	Returns list of image url
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAllVehicleIssueImage]
(@VehicleIssueId int)
AS
BEGIN

Select 
vm.VehicleIssueImageId,
vm.ImageName,
vm.ThumbnailFileName
from tblVehicleIssueImage vm 
where vm.VehicleIssueId = @VehicleIssueId and isnull(vm.IsDeleted,0)=0

END