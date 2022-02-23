CREATE PROCEDURE [StriveCarSalon].[uspDeleteVehicleMembership] (@ClientMembershipId int)
AS
BEGIN

	update tblClientVehicleMembershipDetails set IsDeleted= 1 where ClientMembershipId = @ClientMembershipId

	update tblClientVehicleMembershipService set IsDeleted= 1 where ClientMembershipId = @ClientMembershipId

END