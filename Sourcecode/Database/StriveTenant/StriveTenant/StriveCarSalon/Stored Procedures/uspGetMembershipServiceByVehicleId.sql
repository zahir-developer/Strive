
-- =========================================================
-- Author:		Zahir Hussain
-- Create date: 27-08-2020
-- Description:	Get Membership details for	Vehicle Id
-- =========================================================

CREATE procedure [StriveCarSalon].[uspGetMembershipServiceByVehicleId] 
(@VehicleId INT)
AS
BEGIN

DECLARE @MembershipId INT;

Select Top 1 @MembershipId=MembershipId from tblClientVehicleMembershipDetails where ClientVehicleId = @VehicleId

select 
MembershipId,
MembershipName
from [tblMembership] WITH(NOLOCK)

 WHERE MembershipId=@MembershipId AND ISNULL(IsDeleted,0)=0 AND IsActive=1

select 
MembershipServiceId,
MembershipId,
s.ServiceId,
s.ServiceType as ServiceTypeId,
s.Commision,
s.Upcharges
from [tblMembershipService] ms
LEFT JOIN tblService s WITH(NOLOCK) on s.ServiceId = ms.ServiceId
WHERE MembershipId=@MembershipId AND ISNULL(ms.IsDeleted,0)=0 AND ms.IsActive=1

END
