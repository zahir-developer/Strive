
CREATE PROCEDURE [StriveCarSalon].[uspGetVihicleMembership]
(@MembershipId int =null)
As Begin
SELECT  tblm.MembershipName,
        tblcm.ClientVehicleId
      FROM [StriveCarSalon].[tblMembership]  tblm 
	       Inner Join [StriveCarSalon].[tblClientMembershipDetails] tblcm ON tblm.MembershipId = tblcm.MembershipId
		   inner join [StriveCarSalon].[tblClientVehicle] tbcv ON tblcm.ClientVehicleId = tbcv.VehicleId


WHERE ISNULL(tblm.IsDeleted,0)=0
AND
(@MembershipId is null or tblm.MembershipId = @MembershipId)


END 
