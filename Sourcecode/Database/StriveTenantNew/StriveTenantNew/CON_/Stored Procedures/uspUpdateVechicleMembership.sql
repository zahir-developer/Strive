﻿
CREATE PROCEDURE [CON].[uspUpdateVechicleMembership] (
 @MembershipId int,
 @MembershipName varchar(50))
 AS
BEGIN
UPDATE [CON].[tblMembership]
   SET [MembershipName] = @MembershipName
   WHERE MembershipId = @MembershipId

END