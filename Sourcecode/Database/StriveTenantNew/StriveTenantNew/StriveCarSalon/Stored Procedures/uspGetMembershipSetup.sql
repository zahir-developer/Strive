




CREATE PROCEDURE [StriveCarSalon].[uspGetMembershipSetup]
(@MembershipId int = null)
AS 
BEGIN
SELECT 
tblm.MembershipId,
tblm.MembershipName,
tblm.ServiceId,
tblm.LocationId,
tblcmd.ClientMembershipId,
tblcmd.ClientVehicleId,
tblcmd.StartDate,
tblcmd.EndDate,
tblcmd.Status,
tblcmd.Notes

FROM [StriveCarSalon].[tblMembership] tblm inner join [StriveCarSalon].[tblClientMembershipDetails] tblcmd
		   ON(tblm.MembershipId = tblcmd.MembershipId)
		   AND
		   (@MembershipId IS NULL OR tblm.MembershipId=@MembershipId) 
           WHERE ISNULL(tblm.IsDeleted,0)=0
END