CREATE PROCEDURE [StriveCarSalon].[uspUpdateVechicleMembership] (
 @MembershipId int,
 @MembershipName varchar(50))
 AS
BEGIN
UPDATE [tblMembership]
   SET [MembershipName] = @MembershipName
   WHERE MembershipId = @MembershipId

END
