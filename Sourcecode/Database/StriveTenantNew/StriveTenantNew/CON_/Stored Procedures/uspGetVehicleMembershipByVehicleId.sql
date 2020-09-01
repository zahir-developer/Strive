
-- =========================================================
-- Author:		Vineeth B
-- Create date: 26-08-2020
-- Description:	Get Vehicle Membership Details for VehicleId
-- =========================================================

CREATE procedure [CON].[uspGetVehicleMembershipByVehicleId] 
(@VehicleId int)
AS
BEGIN
select * from [CON].[tblClientVehicle] WITH(NOLOCK) WHERE VehicleId=@VehicleId AND ISNULL(IsDeleted,0)=0 AND IsActive=1
select * from [CON].[tblClientVehicleMembershipDetails] WITH(NOLOCK) WHERE ClientVehicleId=@VehicleId AND ISNULL(IsDeleted,0)=0 AND IsActive=1
select * from [CON].[tblClientVehicleMembershipService] WITH(NOLOCK) WHERE ClientMembershipId IN(select ClientMembershipId from [CON].[tblClientVehicleMembershipDetails] WHERE ClientVehicleId=@VehicleId AND ISNULL(IsDeleted,0)=0 AND IsActive=1)AND ISNULL(IsDeleted,0)=0 AND IsActive=1
END