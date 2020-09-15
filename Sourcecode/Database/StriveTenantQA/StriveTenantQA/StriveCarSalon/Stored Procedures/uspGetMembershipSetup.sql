CREATE PROCEDURE [StriveCarSalon].[uspGetMembershipSetup]
(@MembershipId int = null)
AS 
BEGIN
SELECT 
tblm.MembershipId,
tblm.MembershipName,
tblms.ServiceId,
tblm.LocationId,
tblcmd.ClientMembershipId,
tblcmd.ClientVehicleId,
tblcmd.StartDate,
tblcmd.EndDate,
tblcmd.Status,
tblcmd.Notes

FROM 
	[StriveCarSalon].[tblMembership] tblm 
INNER JOIN 
	[StriveCarSalon].[tblClientVehicleMembershipDetails] tblcmd
ON		tblm.MembershipId = tblcmd.MembershipId
AND		(@MembershipId IS NULL OR tblm.MembershipId=@MembershipId)
INNER JOIN
	[StriveCarSalon].[tblMembershipService] tblms
ON		tblms.MembershipId=tblm.MembershipId
WHERE ISNULL(tblm.IsDeleted,0)=0
END
