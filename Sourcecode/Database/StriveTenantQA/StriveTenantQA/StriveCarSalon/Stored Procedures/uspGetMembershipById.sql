
CREATE PROCEDURE [StriveCarSalon].[uspGetMembershipById]  
(@MembershipId int)
AS 
BEGIN

     select * from [StriveCarSalon].[tblMembership]  WHERE ISNULL(IsDeleted,0)=0 AND ISNULL(IsActive,1)=1 and
             (@MembershipId is null or MembershipId = @MembershipId) 

			 select * from [StriveCarSalon].[tblMembershipService]  where MembershipId = @MembershipId

END
