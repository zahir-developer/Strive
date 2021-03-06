

-- =========================================================
-- Author:		Zahir Hussain
-- Create date: 27-08-2020
-- Description:	Get Membership details for	Vehicle Id
-- =========================================================

CREATE procedure [CON].[uspGetMembershipServiceByVehicleId] 
(@VehicleId INT)
AS
BEGIN

DECLARE @MembershipId INT;

Select Top 1 @MembershipId=MembershipId from [CON].tblClientVehicleMembershipDetails where ClientVehicleId = @VehicleId

select 
MembershipId,
MembershipName
from [CON].[tblMembership] WITH(NOLOCK)

 WHERE MembershipId=@MembershipId AND ISNULL(IsDeleted,0)=0 AND IsActive=1

select 
MembershipServiceId,
MembershipId,
s.ServiceId,
s.ServiceType as ServiceTypeId,
s.Commision
from [CON].[tblMembershipService] ms
LEFT JOIN StriveCarSalon.tblService s WITH(NOLOCK) on s.ServiceId = ms.ServiceId
WHERE MembershipId=@MembershipId AND ISNULL(ms.IsDeleted,0)=0 AND ms.IsActive=1

END