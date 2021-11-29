 --[StriveCarSalon].[uspGetVehicleMembershipByMembershipId] 25
CREATE PROCEDURE [StriveCarSalon].[uspGetVehicleMembershipByMembershipId]
(@MembershipId INT)
AS
-- =======================================================
-- Author:		shalini
-- Create date: 22-01-2020
-- Description:	To get if MembershipId 
-- =======================================================
BEGIN

SELECT 
cvmd.ClientMembershipId,
cvmd.ClientVehicleId,
cvmd.LocationId,
 cvmd.MembershipId 
FROM  tblClientVehicleMembershipDetails cvmd
WHERE cvmd.MembershipId =@MembershipId 
AND cvmd.IsActive = 1 AND ISNULL(cvmd.IsDeleted,0)=0

END