-- =========================================================
-- Author:		Vineeth B
-- Create date: 26-08-2020
-- Description:	Get Vehicle Membership Details for VehicleId
-- =========================================================

CREATE procedure [StriveCarSalon].[uspGetVehicleMembershipByVehicleId] 
(@VehicleId int)
AS
BEGIN

select * from [StriveCarSalon].[tblClientVehicle] WITH(NOLOCK) WHERE VehicleId=@VehicleId AND ISNULL(IsDeleted,0)=0 AND IsActive=1
select * from [StriveCarSalon].[tblClientVehicleMembershipDetails] WITH(NOLOCK) WHERE ClientVehicleId=@VehicleId AND ISNULL(IsDeleted,0)=0 AND IsActive=1

select 
ClientVehicleMembershipServiceId,
ClientMembershipId,
ms.ServiceId,
s.ServiceType as ServiceTypeId, 
s.Upcharges,
ms.IsActive,
ms.IsDeleted
from [StriveCarSalon].[tblClientVehicleMembershipService] ms
LEFT JOIN StriveCarSalon.tblService s WITH(NOLOCK) on s.ServiceId = ms.ServiceId
WHERE ClientMembershipId IN (select ClientMembershipId from [StriveCarSalon].[tblClientVehicleMembershipDetails] WHERE ClientVehicleId=@VehicleId AND ISNULL(IsDeleted,0)=0 AND IsActive=1) AND
ISNULL(ms.IsDeleted,0)=0 AND ms.IsActive=1 






END
