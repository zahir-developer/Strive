
CREATE PROCEDURE [CON].[uspGetMembershipById]  
(@MembershipId int)
AS 
BEGIN

     select * from [CON].[tblMembership]  WHERE ISNULL(IsDeleted,0)=0 AND ISNULL(IsActive,1)=1 and
             (@MembershipId is null or MembershipId = @MembershipId) 

			 select * from [CON].[tblMembershipService]  where MembershipId = @MembershipId

END