-- =========================================================
-- Author:		Vineeth B
-- Create date: 26-08-2020
-- Description:	Get Membership and Its service details
-- =========================================================
 --[StriveCarSalon].[uspGetMembershipServiceByMembershipId] 23573 
-- =========================================================
------------------------History-----------------------------
--27-08-2020 - Zahir Hussain - Removed * and added columns, Added ServiceType and Id.
--31-08-2020 - Vineeth - Added Price and Notes column
--01-09-2020 - Zahir Hussain - Added IsActive/Status, Upcharges
--10-june-2021 -added discounted price
-- =========================================================

CREATE procedure [StriveCarSalon].[uspGetMembershipServiceByMembershipId]
(@MembershipId int)
AS
BEGIN

select 
MembershipId,
MembershipName,
Price,
Notes,
IsActive as Status,
CreatedDate as StartDate,
DiscountedPrice
from [tblMembership] WITH(NOLOCK) WHERE MembershipId=@MembershipId AND ISNULL(IsDeleted,0)=0 --AND IsActive=1

select 
MembershipServiceId,
MembershipId,
s.ServiceId,
s.ServiceType as ServiceTypeId,
s.Price,
st.valuedesc as ServiceType,
s.Upcharges

from [tblMembershipService] ms
LEFT JOIN tblService s WITH(NOLOCK) on s.ServiceId = ms.ServiceId
LEFT JOIN GetTable('ServiceType') st on s.ServiceType = st.valueid
WHERE MembershipId=@MembershipId AND ISNULL(ms.IsDeleted,0)=0 --AND ms.IsActive=1

END
