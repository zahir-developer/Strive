
CREATE PROCEDURE [CON].[uspGetVihicleMembership]
(@MembershipId int =null)
As Begin
SELECT  tblm.MembershipName,
        tblcm.ClientVehicleId
      FROM [CON].[tblMembership]  tblm 
	       Inner Join [CON].[tblClientMembershipDetails] tblcm ON tblm.MembershipId = tblcm.MembershipId
		   inner join [CON].[tblClientVehicle] tbcv ON tblcm.ClientVehicleId = tbcv.VehicleId


WHERE ISNULL(tblm.IsDeleted,0)=0
AND
(@MembershipId is null or tblm.MembershipId = @MembershipId)


END