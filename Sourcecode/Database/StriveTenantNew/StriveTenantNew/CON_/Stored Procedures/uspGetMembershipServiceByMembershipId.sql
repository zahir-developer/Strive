
-- =========================================================
-- Author:		Vineeth B
-- Create date: 26-08-2020
-- Description:	Get Membership and Its service details
-- =========================================================

-- =========================================================
------------------------History-----------------------------
--27-08-2020 - Zahir Hussain - Removed * and added columns, Added ServiceType and Id.
-- =========================================================

CREATE procedure [CON].[uspGetMembershipServiceByMembershipId]
(@MembershipId int)
AS
BEGIN

select 
MembershipId,
MembershipName
from [CON].[tblMembership] WITH(NOLOCK) WHERE MembershipId=@MembershipId AND ISNULL(IsDeleted,0)=0 AND IsActive=1

select 
MembershipServiceId,
MembershipId,
s.ServiceId,
s.ServiceType as ServiceTypeId
from [CON].[tblMembershipService] ms
LEFT JOIN StriveCarSalon.tblService s WITH(NOLOCK) on s.ServiceId = ms.ServiceId
WHERE MembershipId=@MembershipId AND ISNULL(ms.IsDeleted,0)=0 AND ms.IsActive=1

END