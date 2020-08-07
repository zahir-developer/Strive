CREATE PROCEDURE [StriveCarSalon].[uspUpdateVechicleMembership] (
 @MembershipId int,
 @MembershipName varchar(50))
 AS
BEGIN
UPDATE [StriveCarSalon].[tblMembership]
   SET [MembershipName] = @MembershipName
   WHERE MembershipId = @MembershipId

END