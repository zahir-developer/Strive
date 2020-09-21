CREATE procedure [StriveCarSalon].[uspGetMembershipServiceByMembershipId]
(@MembershipId int)
AS
BEGIN

select 
MembershipId,
MembershipName
from [StriveCarSalon].[tblMembership] WITH(NOLOCK) WHERE MembershipId=@MembershipId AND ISNULL(IsDeleted,0)=0 AND IsActive=1

select 
MembershipServiceId,
MembershipId,
s.ServiceId,
s.ServiceType as ServiceTypeId
from [StriveCarSalon].[tblMembershipService] ms
LEFT JOIN StriveCarSalon.tblService s WITH(NOLOCK) on s.ServiceId = ms.ServiceId
WHERE MembershipId=@MembershipId AND ISNULL(ms.IsDeleted,0)=0 AND ms.IsActive=1

END
