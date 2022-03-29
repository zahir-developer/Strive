-- =========================================================
--						History
-- =========================================================
-- Zahir - 16-03-2022 - Added GetDate() for Updated date.

-- =========================================================

CREATE PROCEDURE [StriveCarSalon].[uspDeleteVehicleMembership] (@ClientMembershipId int)
AS
BEGIN

	update tblClientVehicleMembershipDetails set IsDeleted= 1, UpdatedDate = GETDATE() where ClientMembershipId = @ClientMembershipId

	update tblClientVehicleMembershipService set IsDeleted= 1, UpdatedDate = GETDATE() where ClientMembershipId = @ClientMembershipId

END
GO

