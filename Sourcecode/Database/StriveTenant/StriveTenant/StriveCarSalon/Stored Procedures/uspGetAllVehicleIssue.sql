-- =============================================
-- Author:		Zahir Hussain
-- Create date: 2022-02-04
-- Description:	Returns list of Issues and Images
-- =============================================

CREATE PROCEDURE [StriveCarSalon].[uspGetAllVehicleIssue]
(@VehicleId int)
AS
BEGIN

Select VehicleIssueId, VehicleId, [Description], CreatedDate, CreatedBy from tblVehicleIssue where vehicleId = @VehicleId and IsDeleted = 0

Select 
vm.VehicleIssueImageId,
vI.VehicleIssueId,
vm.ImageName,
vm.OriginalImageName,
vm.ThumbnailFileName,
vm.CreatedDate
from tblVehicleIssue vI
inner join tblVehicleIssueImage vm on vi.VehicleIssueId = vm.VehicleIssueId
where vi.VehicleId = @VehicleId  and isnull(vi.IsDeleted,0)=0

END