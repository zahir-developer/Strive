CREATE PROCEDURE [StriveCarSalon].[uspGetMembershipById]  
(@MembershipId int)
AS 
BEGIN

select * from [StriveCarSalon].[tblMembership]  WHERE ISNULL(IsDeleted,0)=0 AND ISNULL(IsActive,1)=1 and
             (@MembershipId is null or MembershipId = @MembershipId) 

select ms.* from [StriveCarSalon].[tblMembershipService] ms
JOIN tblMembership m on m.membershipId = ms.membershipId
JOIN tblService s on s.serviceId = ms.serviceId --and s.locationId = m.locationId
where ms.MembershipId = @MembershipId order by 1 desc

END
