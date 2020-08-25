CREATE PROCEDURE [StriveCarSalon].[uspDeleteMembership] (@membershipId int)
AS
BEGIN

	update StriveCarSalon.tblMembership set IsDeleted= 1 where MembershipId = @membershipId

	update StriveCarSalon.tblMembershipService set IsDeleted= 1 where MembershipId = @membershipId

END