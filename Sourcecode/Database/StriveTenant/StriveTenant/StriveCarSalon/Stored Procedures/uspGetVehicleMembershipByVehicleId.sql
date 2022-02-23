-- =========================================================
-- Author:		Vineeth B
-- Create date: 26-08-2020
-- Description:	Get Vehicle Membership Details for VehicleId 
-- EXEC [StriveCarSalon].[uspGetVehicleMembershipByVehicleId] 214494
-- =========================================================
--						History
-- =========================================================
-- Zahir - 01-09-2021 - Membership service retrieved if no ClientVehicleMembershipService available.
-- Zahir - 15-12-2021 - Added Total Price.

-- =========================================================

CREATE PROCEDURE [StriveCarSalon].[uspGetVehicleMembershipByVehicleId] 
(@VehicleId int)
AS
BEGIN

DECLARE @MembershipId INT;
DECLARE @ClientMembershipId INT;

select * from [tblClientVehicle] WITH(NOLOCK) WHERE VehicleId=@VehicleId AND ISNULL(IsDeleted,0)=0 AND IsActive=1

select
Top 1
ClientMembershipId,
ClientVehicleId,
LocationId,
MembershipId,
StartDate,
EndDate,
ISNULL(TotalPrice,0) TotalPrice,
CardNumber,
ExpiryDate,
ProfileId,
AccountId
from [tblClientVehicleMembershipDetails] WITH(NOLOCK) WHERE ClientVehicleId=@VehicleId AND ISNULL(IsDeleted,0)=0 AND IsActive=1


select top 1
@ClientMembershipId = ClientMembershipId,
@MembershipId=MembershipId from [tblClientVehicleMembershipDetails] WITH(NOLOCK) WHERE ClientVehicleId=@VehicleId AND ISNULL(IsDeleted,0)=0 AND IsActive=1

DROP TABLE IF EXISTS #ServiceIds

CREATE TABLE #ServiceIds(ServiceId INT, ClientVehicleMembershipServiceId INT)

INSERT INTO #ServiceIds
select vms.serviceId, vms.ClientVehicleMembershipServiceId from tblClientVehicleMembershipDetails vmd
INNER JOIN tblClientVehicleMembershipService vms on vms.ClientMembershipId = vmd.ClientMembershipId
WHERE vmd.ClientMembershipId = @ClientMembershipId 
AND ISNULL(vmd.IsDeleted,0)=0 AND vmd.IsActive=1 
AND ISNULL(vms.IsDeleted,0)=0 AND vms.IsActive=1

IF @ClientMembershipId = 0
BEGIN
INSERT INTO #ServiceIds
Select ms.ServiceId, 0 from tblMembership m 
INNER JOIN tblMembershipService ms on ms.membershipId = m.membershipId
WHERE m.MembershipId = @MembershipId 
AND ISNULL(m.IsDeleted,0)=0 AND m.IsActive=1 
AND ISNULL(ms.IsDeleted,0)=0 AND ms.IsActive=1
END

select 
@ClientMembershipId as ClientMembershipId,
s.ServiceId,
s.ServiceType as ServiceTypeId, 
cv.CodeValue as ServiceType,
s.Upcharges,
sIds.ClientVehicleMembershipServiceId
from #ServiceIds sIds
INNER JOIN tblService s on s.ServiceId = sIds.ServiceId
INNER JOIN tblCodeValue cv on cv.id = s.serviceType
END
