
CREATE PROCEDURE [CON].[uspGetMembershipListByVehicleId]
(@VehicleId int)
As Begin
   Select tblm.MembershipId,
          tblm.MembershipName

		  from [CON].[tblClientVehicle] tblcv 
		  Inner join [CON].[tblClientVehicleMembershipDetails] tblcmd ON tblcv.VehicleId = tblcmd.ClientVehicleId
		  inner join [CON].[tblMembership] tblm ON tblcmd.MembershipId = tblm.MembershipId

		  WHERE ISNULL(tblcv.IsDeleted,0)=0 AND ISNULL(tblcv.IsActive,1)=1 and getdate() between tblcmd.StartDate and tblcmd.EndDate and
         (@VehicleId is null or tblcv.VehicleId = @VehicleId)  
		 
END
